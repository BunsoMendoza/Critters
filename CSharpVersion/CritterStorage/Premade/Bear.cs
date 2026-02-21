using System.Drawing;
using CritterSimulator.Game;

namespace CritterSimulator.CritterStorage.Premade
{
    public class Bear : Critter
    {
        private bool polar = true;
        private bool isOdd = true;

        public Bear(bool polar)
        {
            this.polar = polar;
        }

        public override Action GetMove(ICritterInfo info)
        {
            if (info.GetFront() == Neighbor.Other)
            {
                return Action.Infect;
            }
            else if (info.GetFront() == Neighbor.Empty)
            {
                return Action.Hop;
            }
            else
            {
                return Action.Left;
            }
        }

        public override Color GetColor()
        {
            if (polar)
            {
                return Color.White;
            }
            else
            {
                return Color.Black;
            }
        }

        public override string ToString()
        {
            if (isOdd)
            {
                isOdd = false;
                return "/";
            }
            else
            {
                isOdd = true;
                return "\\";
            }
        }
    }
}
