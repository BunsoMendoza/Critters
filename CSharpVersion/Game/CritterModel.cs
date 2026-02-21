using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using CritterSimulator.CritterStorage.Premade;

namespace CritterSimulator.Game
{
    // Class CritterModel keeps track of the state of the critter simulation.
    public class CritterModel
    {
        // The following constant indicates how often infect should fail for
        // critters who didn't hop on their previous move (0.0 means no advantage,
        // 1.0 means 100% advantage)
        public const double HopAdvantage = 0.2; // 20% advantage

        private int height;
        private int width;
        private Critter[,] grid;
        private Dictionary<Critter, PrivateData> info;
        private SortedDictionary<string, int> critterCount;
        private bool debugView;
        private int simulationCount;
        private static bool created;
        private Random random = new Random();

        public CritterModel(int width, int height)
        {
            // This prevents someone from trying to create their own copy of
            // the GUI components
            if (created)
                throw new InvalidOperationException("Only one world allowed");
            created = true;

            this.width = width;
            this.height = height;
            grid = new Critter[width, height];
            info = new Dictionary<Critter, PrivateData>();
            critterCount = new SortedDictionary<string, int>();
            this.debugView = false;
        }

        public IEnumerable<Critter> GetCritters()
        {
            return info.Keys;
        }

        public Point GetPoint(Critter c)
        {
            return info[c].Position;
        }

        public Color GetColor(Critter c)
        {
            return info[c].CritterColor;
        }

        public string GetString(Critter c)
        {
            return info[c].DisplayString;
        }

        public void Add(int number, Type critterType)
        {
            Critter.Direction[] directions = (Critter.Direction[])Enum.GetValues(typeof(Critter.Direction));
            if (info.Count + number > width * height)
                throw new InvalidOperationException("adding too many critters");
            for (int i = 0; i < number; i++)
            {
                Critter next;
                try
                {
                    next = MakeCritter(critterType);
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("ERROR: " + critterType + " does not have the appropriate constructor.");
                    Environment.Exit(1);
                    return;
                }
                catch (Exception)
                {
                    Console.WriteLine("ERROR: " + critterType + " threw an exception in its constructor.");
                    Environment.Exit(1);
                    return;
                }
                int x, y;
                do
                {
                    x = random.Next(width);
                    y = random.Next(height);
                } while (grid[x, y] != null);
                grid[x, y] = next;

                Critter.Direction d = directions[random.Next(directions.Length)];
                info[next] = new PrivateData(new Point(x, y), d);
            }
            string name = critterType.FullName;

            string displayName = GetCritterDisplayName(name);
            if (!critterCount.ContainsKey(displayName))
                critterCount[displayName] = number;
            else
                critterCount[displayName] = critterCount[displayName] + number;
        }

        private Critter MakeCritter(Type critterType)
        {
            if (critterType.FullName == "CritterSimulator.CritterStorage.Premade.Bear")
            {
                // flip a coin
                bool b = random.NextDouble() < 0.5;
                return (Critter)Activator.CreateInstance(critterType, b);
            }
            else
            {
                return (Critter)Activator.CreateInstance(critterType);
            }
        }

        public int Width => width;

        public int Height => height;

        public string GetAppearance(Critter c)
        {
            // Override specified ToString if debug flag is true
            if (!debugView)
                return info[c].DisplayString;
            else
            {
                PrivateData data = info[c];
                if (data.CritterDirection == Critter.Direction.North) return "^";
                else if (data.CritterDirection == Critter.Direction.South) return "v";
                else if (data.CritterDirection == Critter.Direction.East) return ">";
                else return "<";
            }
        }

        public void ToggleDebug()
        {
            this.debugView = !this.debugView;
        }

        private bool InBounds(int x, int y)
        {
            return (x >= 0 && x < width && y >= 0 && y < height);
        }

        private bool InBounds(Point p)
        {
            return InBounds(p.X, p.Y);
        }

        // Returns the result of rotating the given direction clockwise
        private Critter.Direction Rotate(Critter.Direction d)
        {
            if (d == Critter.Direction.North) return Critter.Direction.East;
            else if (d == Critter.Direction.South) return Critter.Direction.West;
            else if (d == Critter.Direction.East) return Critter.Direction.South;
            else return Critter.Direction.North;
        }

        private Point PointAt(Point p, Critter.Direction d)
        {
            if (d == Critter.Direction.North) return new Point(p.X, p.Y - 1);
            else if (d == Critter.Direction.South) return new Point(p.X, p.Y + 1);
            else if (d == Critter.Direction.East) return new Point(p.X + 1, p.Y);
            else return new Point(p.X - 1, p.Y);
        }

        private Info GetInfo(PrivateData data, Type original)
        {
            Critter.Neighbor[] neighbors = new Critter.Neighbor[4];
            Critter.Direction d = data.CritterDirection;
            bool[] neighborThreats = new bool[4];
            for (int i = 0; i < 4; i++)
            {
                neighbors[i] = GetStatus(PointAt(data.Position, d), original);
                if (neighbors[i] == Critter.Neighbor.Other)
                {
                    Point p = PointAt(data.Position, d);
                    PrivateData oldData = info[grid[p.X, p.Y]];
                    neighborThreats[i] = d == Rotate(Rotate(oldData.CritterDirection));
                }
                d = Rotate(d);
            }
            return new Info(neighbors, data.CritterDirection, neighborThreats);
        }

        private Critter.Neighbor GetStatus(Point p, Type original)
        {
            if (!InBounds(p))
                return Critter.Neighbor.Wall;
            else if (grid[p.X, p.Y] == null)
                return Critter.Neighbor.Empty;
            else if (grid[p.X, p.Y].GetType() == original)
                return Critter.Neighbor.Same;
            else
                return Critter.Neighbor.Other;
        }

        public void Update()
        {
            simulationCount++;
            List<Critter> list = new List<Critter>(info.Keys);
            // Shuffle
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                Critter temp = list[k];
                list[k] = list[n];
                list[n] = temp;
            }

            // This keeps track of critters that are locked and cannot be
            // infected this turn.
            HashSet<Critter> locked = new HashSet<Critter>();

            for (int i = 0; i < list.Count; i++)
            {
                Critter next = list[i];
                if (!info.ContainsKey(next))
                {
                    // Happens when creature was infected earlier in this round
                    continue;
                }
                PrivateData data = info[next];
                // Clear any prior setting for having hopped in the past
                bool hadHopped = data.JustHopped;
                data.JustHopped = false;
                Point p = data.Position;
                Point p2 = PointAt(p, data.CritterDirection);

                // Try to perform the critter's action
                Critter.Action move = next.GetMove(GetInfo(data, next.GetType()));
                if (move == Critter.Action.Left)
                    data.CritterDirection = Rotate(Rotate(Rotate(data.CritterDirection)));
                else if (move == Critter.Action.Right)
                    data.CritterDirection = Rotate(data.CritterDirection);
                else if (move == Critter.Action.Hop)
                {
                    if (InBounds(p2) && grid[p2.X, p2.Y] == null)
                    {
                        grid[p2.X, p2.Y] = grid[p.X, p.Y];
                        grid[p.X, p.Y] = null;
                        data.Position = p2;
                        locked.Add(next);
                        data.JustHopped = true;
                    }
                }
                else if (move == Critter.Action.Infect)
                {
                    if (InBounds(p2) && grid[p2.X, p2.Y] != null
                        && grid[p2.X, p2.Y].GetType() != next.GetType()
                        && !locked.Contains(grid[p2.X, p2.Y])
                        && (hadHopped || random.NextDouble() >= HopAdvantage))
                    {
                        Critter otherCritter = grid[p2.X, p2.Y];
                        // Remember the old critter's private data
                        PrivateData oldData = info[otherCritter];
                        // Then remove that old critter
                        string c1 = otherCritter.GetType().FullName;
                        critterCount[GetCritterDisplayName(c1)] = critterCount[GetCritterDisplayName(c1)] - 1;
                        string c2 = next.GetType().FullName;
                        critterCount[GetCritterDisplayName(c2)] = critterCount[GetCritterDisplayName(c2)] + 1;
                        info.Remove(otherCritter);
                        // And add a new one to the grid
                        try
                        {
                            grid[p2.X, p2.Y] = MakeCritter(next.GetType());
                            locked.Add(grid[p2.X, p2.Y]);
                        }
                        catch (Exception e)
                        {
                            throw new InvalidOperationException(e.ToString());
                        }
                        // And add to the map
                        info[grid[p2.X, p2.Y]] = oldData;
                        // But it's new, so it didn't just hop
                        oldData.JustHopped = false;
                    }
                }
            }
            UpdateColorString();
        }

        // Calling this method causes each critter to update the stored color and
        // text for ToString; should be called each time update is performed and
        // once before the simulation begins
        public void UpdateColorString()
        {
            foreach (Critter next in info.Keys)
            {
                info[next].CritterColor = next.GetColor();
                info[next].DisplayString = next.ToString();
            }
        }

        public IEnumerable<KeyValuePair<string, int>> GetCounts()
        {
            return critterCount;
        }

        public int SimulationCount => simulationCount;

        private class PrivateData
        {
            public Point Position { get; set; }
            public Critter.Direction CritterDirection { get; set; }
            public Color CritterColor { get; set; }
            public string DisplayString { get; set; }
            public bool JustHopped { get; set; }

            public PrivateData(Point p, Critter.Direction d)
            {
                this.Position = p;
                this.CritterDirection = d;
            }

            public override string ToString()
            {
                return Position + " " + CritterDirection;
            }
        }

        private string GetCritterDisplayName(string name)
        {
            string[] tokens = name.Split('.');
            return tokens[tokens.Length - 1];
        }

        // An object used to query a critter's state (neighbors, direction)
        private class Info : ICritterInfo
        {
            private Critter.Neighbor[] neighbors;
            private Critter.Direction direction;
            private bool[] neighborThreats;

            public Info(Critter.Neighbor[] neighbors, Critter.Direction d,
                        bool[] neighborThreats)
            {
                this.neighbors = neighbors;
                this.direction = d;
                this.neighborThreats = neighborThreats;
            }

            public Critter.Neighbor GetFront()
            {
                return neighbors[0];
            }

            public Critter.Neighbor GetBack()
            {
                return neighbors[2];
            }

            public Critter.Neighbor GetLeft()
            {
                return neighbors[3];
            }

            public Critter.Neighbor GetRight()
            {
                return neighbors[1];
            }

            public Critter.Direction GetDirection()
            {
                return direction;
            }

            public bool FrontThreat()
            {
                return neighborThreats[0];
            }

            public bool BackThreat()
            {
                return neighborThreats[2];
            }

            public bool LeftThreat()
            {
                return neighborThreats[3];
            }

            public bool RightThreat()
            {
                return neighborThreats[1];
            }
        }
    }
}
