using System;
using System.Drawing;
using System.Linq;
using CritterSimulator.Game;

namespace CritterSimulator.CritterStorage.Ryan
{
    // This class is an empty shell that MIGHT make designing new critters a little
    // easier and MAYBE a bit more intuitive.
    // Be sure to call 'SetFields()' in the GetMove() method of the subclass to set the fields.
    // You can also add 'PrintFields()' for debugging.
    public class Shell : Premade.Critter
    {
        public override Color GetColor() { return Color.Empty; }

        // Step counter should line up with simulation step counter.
        public int step = 0;

        // Is true if critter is touching a wall on any side.
        public bool hasWall = false;

        // Is true after critter has seen its first wall.
        public bool seenFirstWall = false;

        // Field that stores the 4 current neighbor constants for this critter.
        public Neighbor[] neighbor = new Neighbor[4];

        // Field that stores the 4 current threat booleans.
        public bool[] threat = new bool[4];

        // Field that stores this critter's current direction.
        public Direction direction = Direction.North;

        // To compare neighbor[] ie. (neighbor[back] == wall) etc.
        public Neighbor wall = Neighbor.Wall;
        public Neighbor empty = Neighbor.Empty;
        public Neighbor same = Neighbor.Same;
        public Neighbor other = Neighbor.Other;

        // To compare direction ie. (direction == north) etc.
        public Direction north = Direction.North;
        public Direction east = Direction.East;
        public Direction south = Direction.South;
        public Direction west = Direction.West;

        // To return action ie. return hop;
        public Action hop = Action.Hop;
        public Action infect = Action.Infect;
        public Action L = Action.Left;
        public Action R = Action.Right;

        // To ask about neighbor[]
        public int front = 0;
        public int right = 1;
        public int back = 2;
        public int left = 3;

        public Shell()
        {
        }

        // Normally overridden, left for debugging
        public override Action GetMove(ICritterInfo info)
        {
            return Action.Infect;
        }

        public override string ToString()
        {
            if (direction == north)
            {
                return "^";
            }
            else if (direction == east)
            {
                return ">";
            }
            else if (direction == south)
            {
                return "v";
            }
            else
            {
                return "<";
            }
        }

        // Set neighbor[] field.
        public void SetNeighbors(ICritterInfo info)
        {
            neighbor[0] = info.GetFront();
            neighbor[1] = info.GetRight();
            neighbor[2] = info.GetBack();
            neighbor[3] = info.GetLeft();
        }

        // Set threat[] field.
        public void SetThreat(ICritterInfo info)
        {
            threat[0] = info.FrontThreat();
            threat[1] = info.RightThreat();
            threat[2] = info.BackThreat();
            threat[3] = info.LeftThreat();
        }

        // Set direction field.
        public void SetDirection(ICritterInfo info)
        {
            direction = info.GetDirection();
        }

        // Set all fields
        public void SetFields(ICritterInfo info)
        {
            step++;
            SetNeighbors(info);
            SetThreat(info);
            SetDirection(info);
            HasWallCheck();
        }

        // Print fields to console
        public void PrintFields()
        {
            Console.WriteLine(string.Join(", ", neighbor.Select(n => n.ToString()).ToArray()));
            Console.WriteLine(string.Join(", ", threat));
            Console.WriteLine(direction);
        }

        // Updates 'hasWall' field to true if critter is next to a wall on any side
        public void HasWallCheck()
        {
            if (neighbor[left] == wall || neighbor[right] == wall || neighbor[back] == wall || neighbor[front] == wall)
            {
                hasWall = true;
            }
            else
            {
                hasWall = false;
            }
        }
    }
}
