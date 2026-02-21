using System.Drawing;
using CritterSimulator.Game;
using CritterSimulator.CritterStorage.Premade;

namespace CritterSimulator.CritterStorage.Ryan
{
    public class Snake : Critter
    {
        private int direction = 0;
        private int step = 0;
        private readonly int AMP = 1;
        private bool alt = true;

        public Snake()
        {
        }

        public override Action GetMove(ICritterInfo info)
        {
            if (info.GetFront() == Neighbor.Other)
            {
                return Action.Infect;
            }

            if (info.GetFront() == Neighbor.Wall || info.GetFront() == Neighbor.Same)
            {
                return Action.Right;
            }
            else if (step == 0 && info.GetLeft() == Neighbor.Wall)
            {
                step++;
                return Action.Hop;
            }
            else if (step == 1 && info.GetLeft() == Neighbor.Wall)
            {
                step = 0;
                return Action.Right;
            }
            else
            {
                return Action.Hop;
            }
        }

        public override Color GetColor()
        {
            return Color.Red;
        }

        public override string ToString()
        {
            if (direction == 1)
            {
                return "I";
            }
            else if (direction == 2)
            {
                return "I";
            }
            else if (direction == 3)
            {
                return "I";
            }
            else
            {
                return "I";
            }
        }

        public void SetDirection(ICritterInfo info)
        {
            if (info.GetDirection() == Direction.North)
            {
                direction = 1;
            }
            else if (info.GetDirection() == Direction.East)
            {
                direction = 2;
            }
            else if (info.GetDirection() == Direction.South)
            {
                direction = 3;
            }
            else
            {
                direction = 4;
            }
        }
    }
}
