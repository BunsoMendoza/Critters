using System;
using System.Drawing;
using CritterSimulator.Game;
using CritterSimulator.CritterStorage.Premade;

namespace CritterSimulator.CritterStorage.Bunso
{
    public class Cinnamon : Critter
    {
        public override Action GetMove(ICritterInfo info)
        {
            Random rand = new Random();
            int current = rand.Next(2);
            bool isRight = true;
            bool hasBounced = false;

            switch (info.GetFront())
            {
                case Neighbor.Other:
                    return Action.Infect;

                case Neighbor.Same:
                    return Action.Right;

                case Neighbor.Wall:
                    if (current == 0)
                    {
                        isRight = false;
                        hasBounced = true;
                        return Action.Left;
                    }
                    else if (current == 1)
                    {
                        isRight = true;
                        return Action.Right;
                    }
                    else
                    {
                        Console.WriteLine("Error for LEFT OR RIGHT");
                        return Action.Left;
                    }

                default:
                    if (hasBounced && isRight)
                    {
                        hasBounced = false;
                        return Action.Right;
                    }
                    else if (hasBounced && !isRight)
                    {
                        hasBounced = false;
                        return Action.Left;
                    }
                    else
                    {
                        return Action.Hop;
                    }
            }
        }

        public override Color GetColor()
        {
            return Color.Red;
        }

        public override string ToString()
        {
            return "+";
        }
    }
}
