using System.Drawing;
using CritterSimulator.Game;
using CritterSimulator.CritterStorage.Premade;

namespace CritterSimulator.CritterStorage.Ryan
{
    public class Atom : Critter
    {
        private int direction = 0;
        private int step = 0;
        private readonly int AMP = 15; // Change atom vibration amplitude
        private bool alt = true;

        public Atom()
        {
        }

        public override Action GetMove(ICritterInfo info)
        {
            SetDirection(info);

            if (info.GetFront() == Neighbor.Other)
            {
                return Action.Infect;
            }

            if (info.GetFront() == Neighbor.Wall || info.GetFront() == Neighbor.Same || info.GetRight() == Neighbor.Wall)
            {
                return Action.Left;
            }

            if (alt)
            {
                if (step % AMP == 0)
                {
                    alt = false;
                }

                step++;
                return Action.Hop;
            }
            else
            {
                if (step % AMP == 0)
                {
                    alt = true;
                }
                step++;
                return Action.Left;
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
                return ".";
            }
            else if (direction == 2)
            {
                return "o";
            }
            else if (direction == 3)
            {
                return ".";
            }
            else
            {
                return "o";
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
