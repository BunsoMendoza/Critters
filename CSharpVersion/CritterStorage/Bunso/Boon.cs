using System;
using System.Drawing;
using CritterSimulator.Game;
using CritterSimulator.CritterStorage.Premade;

namespace CritterSimulator.CritterStorage.Bunso
{
    public class Boon : Critter
    {
        private bool attached = false;
        private int stepCounter = 1;
        private int turns = 0;
        private string direction = "Left";
        private int sumOfN = 0;
        private int N = 1;

        public override Action GetMove(ICritterInfo info)
        {
            if (attached)
            {
                return AttachedPivot(info);
            }
            else
            {
                return CheckAndMove(info);
            }
        }

        public override Color GetColor()
        {
            return Color.Pink;
        }

        public override string ToString()
        {
            return "#";
        }

        private Action AttachedPivot(ICritterInfo info)
        {
            if (stepCounter % 200 == 0)
            {
                attached = false;
                return Action.Left;
            }

            if (turns == 3 && direction == "Left")
            {
                turns = 0;
                direction = "Right";
            }
            if (turns == 3 && direction == "Right")
            {
                turns = 0;
                direction = "Left";
            }

            if (info.GetFront() == Neighbor.Other)
            {
                stepCounter++;
                return Action.Infect;
            }
            else if (direction == "Left")
            {
                stepCounter++;
                turns++;
                return Action.Left;
            }
            else
            {
                stepCounter++;
                turns++;
                return Action.Right;
            }
        }

        private Action CheckAndMove(ICritterInfo info)
        {
            if (N > 20)
            {
                N = 1;
                sumOfN = 0;
            }

            switch (info.GetFront())
            {
                case Neighbor.Other:
                    return Action.Infect;

                case Neighbor.Same:
                    return Action.Right;

                case Neighbor.Wall:
                    attached = true;
                    Console.WriteLine("attached");
                    turns++;
                    return Action.Left;

                case Neighbor.Empty:
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

                default:
                    return Action.Infect;
            }
        }
    }
}
