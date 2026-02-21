using System;
using System.Drawing;
using CritterSimulator.Game;
using CritterSimulator.CritterStorage.Premade;

namespace CritterSimulator.CritterStorage.Josh
{
    // Critter class extension for a critter called a 'EMBO'
    public class EMBO : Critter
    {
        private static readonly Random rand = new Random();

        // Private variables to measure the count of the instances and the previous string of the EMBO
        // and the different possible String values of the EMBO
        private int count;
        private string previousName;
        private readonly string[] EMBONames = { "E", "M", "B", "O" };
        private int EMBOIndex;

        // Constructor of the critter EMBO
        public EMBO()
        {
            this.count = 0;
            this.EMBOIndex = 0;
        }

        // Returns the color of the EMBO
        public override Color GetColor()
        {
            int currentColor = rand.Next(3);

            switch (currentColor)
            {
                case 0:
                    return Color.Blue;
                case 1:
                    return Color.Orange;
                default:
                    return Color.Black;
            }
        }

        // Returns the string value of EMBO
        public override string ToString()
        {
            this.count = this.count + 1;
            if ((this.count - 1) % 6 == 0)
            {
                if (this.EMBOIndex == 4)
                {
                    this.EMBOIndex = 0;
                }
                this.EMBOIndex = this.EMBOIndex + 1;
                return EMBONames[EMBOIndex - 1];
            }
            else
            {
                this.previousName = EMBONames[EMBOIndex - 1];
            }
            return this.previousName;
        }

        // Returns the move to be made by EMBO
        public override Action GetMove(ICritterInfo info)
        {
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
                return Action.Hop;
            }
        }
    }
}
