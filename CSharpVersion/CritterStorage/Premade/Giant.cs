using System.Drawing;
using CritterSimulator.Game;

namespace CritterSimulator.CritterStorage.Premade
{
    public class Giant : Critter
    {
        private int counter = 0;

        public override Action GetMove(ICritterInfo info)
        {
            if (info.GetFront() == Neighbor.Other)
            {
                return Action.Infect;
            }
            else if (info.GetFront() == Neighbor.Same || info.GetFront() == Neighbor.Wall)
            {
                return Action.Right;
            }
            else
            {
                return Action.Hop;
            }
        }

        public override Color GetColor()
        {
            return Color.Gray;
        }

        public override string ToString()
        {
            if (counter < 6)
            {
                counter++;
                return "fee";
            }
            else if (counter < 12)
            {
                counter++;
                return "fie";
            }
            else if (counter < 18)
            {
                counter++;
                return "foe";
            }
            else if (counter < 24)
            {
                counter++;
                return "fum";
            }
            else
            {
                counter = 0;
                return "fee";
            }
        }
    }
}
