using System;
using System.Collections.Generic;
using System.Drawing;
using CritterSimulator.Game;

namespace CritterSimulator.CritterStorage.Bunso
{
    public class ReusableMethods : Premade.Critter
    {
        private static readonly Random rand = new Random();
        public override string ToString()
        {
            return null;
        }

        public override Color GetColor()
        {
            return Color.Empty;
        }

        public override Action GetMove(ICritterInfo info)
        {
            return Action.Infect;
        }

        public bool touchedWall = false;
        public int hops = 0;
        public List<string> walls = new List<string>();
        public Dictionary<string, Neighbor> hmap = new Dictionary<string, Neighbor>();
        public Dictionary<string, bool> threats = new Dictionary<string, bool>();

        // To compare neighbor[] ie. (neighbor[back] == wall) etc.
        public Neighbor wall = Neighbor.Wall;
        public Neighbor empty = Neighbor.Empty;
        public Neighbor friend = Neighbor.Same;
        public Neighbor enemy = Neighbor.Other;

        // To compare direction ie. (direction == north) etc.
        public Direction north = Direction.North;
        public Direction east = Direction.East;
        public Direction south = Direction.South;
        public Direction west = Direction.West;

        public Direction currentDirection;

        // To return action ie. return hop;
        public Action hop = Action.Hop;
        public Action infect = Action.Infect;
        public Action turnLeft = Action.Left;
        public Action turnRight = Action.Right;

        // Set neighbor[] field.
        public void CheckNeighbors(ICritterInfo info)
        {
            currentDirection = info.GetDirection();
            hmap.Clear();
            walls.Clear();
            hmap["front"] = info.GetFront();
            hmap["right"] = info.GetRight();
            hmap["back"] = info.GetBack();
            hmap["left"] = info.GetLeft();

            foreach (var e in hmap)
            {
                if (e.Value == enemy)
                {
                    threats[e.Key] = true;
                }
            }
            foreach (var e in hmap)
            {
                if (e.Value == wall)
                {
                    walls.Add(e.Key);
                }
            }
        }

        public bool IsTouchingWall()
        {
            if (walls.Count == 0)
            {
                return false;
            }
            else
            {
                touchedWall = true;
                return true;
            }
        }

        // touching
        public Action WallCheck()
        {
            int wallCount = 0;

            if (walls.Count == 0)
            {
                Console.WriteLine("WallCheck() was called. But critter isn't touching wall.");
                return hop;
            }
            else
            {
                wallCount = walls.Count;
            }

            switch (wallCount)
            {
                case 1:
                    if (walls[0] == "front")
                    {
                        FacedDirection();
                        bool b = rand.NextDouble() < 0.5;
                        return b ? turnLeft : turnRight;
                    }
                    else if (walls[0] == "right")
                    {
                        return turnLeft;
                    }
                    else if (walls[0] == "left")
                    {
                        return turnRight;
                    }
                    else
                    {
                        return hop;
                    }

                case 2:
                    return TouchingTwoWalls(walls[0], walls[1]);

                default:
                    return hop;
            }
        }

        public Action TouchingTwoWalls(string wallOne, string wallTwo)
        {
            if ((wallOne == "front" || wallOne == "back") && wallTwo == "right")
            {
                return turnLeft;
            }
            else if ((wallOne == "front" || wallOne == "back") && wallTwo == "left")
            {
                return turnRight;
            }
            else
            {
                return infect;
            }
        }

        public Direction FacedDirection()
        {
            switch (currentDirection)
            {
                case Direction.North:
                    return Direction.South;
                case Direction.South:
                    return Direction.North;
                case Direction.East:
                    return Direction.West;
                case Direction.West:
                    return Direction.East;
                default:
                    Console.WriteLine("Direction was not set");
                    return currentDirection;
            }
        }
    }
}
