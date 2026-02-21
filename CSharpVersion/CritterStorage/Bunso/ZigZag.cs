using System.Drawing;
using CritterSimulator.Game;
using CritterSimulator.CritterStorage.Premade;

namespace CritterSimulator.CritterStorage.Bunso
{
    public class ZigZag : Critter
    {
        private int sumOfN = 0;
        private int N = 1;

        public override Action GetMove(ICritterInfo info)
        {
            if (N > 20)
            {
                N = 1;
                sumOfN = 0;
            }

            if (info.GetFront() == Neighbor.Other)
            {
                return Action.Infect;
            }
            else if (info.GetFront() == Neighbor.Wall)
            {
                return Action.Left;
            }
            else if (info.GetFront() == Neighbor.Same)
            {
                return Action.Right;
            }
            else
            {
                if (sumOfN == 0 || N % sumOfN != 0)
                {
                    N++;
                    sumOfN += N;
                    return Action.Hop;
                }
                else if (N % sumOfN == 0 && sumOfN % 2 == 0)
                {
                    N++;
                    sumOfN += N;
                    return Action.Left;
                }
                else if (N % sumOfN == 0)
                {
                    N++;
                    sumOfN += N;
                    return Action.Right;
                }
                else
                {
                    N++;
                    sumOfN += N;
                    return Action.Infect;
                }
            }
        }

        public override Color GetColor()
        {
            return Color.Orange;
        }

        public override string ToString()
        {
            return "Z";
        }
    }
}
