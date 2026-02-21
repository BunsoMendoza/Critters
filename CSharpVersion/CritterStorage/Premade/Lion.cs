using System;
using System.Drawing;
using CritterSimulator.Game;

namespace CritterSimulator.CritterStorage.Premade
{
    public class Lion : Critter
    {
        public override Action GetMove(ICritterInfo info)
        {
            if (info.GetFront() == Neighbor.Other)
            {
                return Action.Infect;
            }
            else if (info.GetFront() == Neighbor.Wall || info.GetRight() == Neighbor.Wall)
            {
                return Action.Left;
            }
            else if (info.GetFront() == Neighbor.Same)
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
            Random rand = new Random();
            int currentColor = rand.Next(3);

            switch (currentColor)
            {
                case 0:
                    return Color.Red;
                case 1:
                    return Color.Green;
                case 2:
                    return Color.Blue;
                default:
                    return Color.White;
            }
        }

        public override string ToString()
        {
            return "L";
        }
    }
}
