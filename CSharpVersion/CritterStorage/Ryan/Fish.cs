using System;
using System.Drawing;
using CritterSimulator.Game;
using CritterSimulator.CritterStorage.Premade;

namespace CritterSimulator.CritterStorage.Ryan
{
    // This critter is testing the inheritance from Shell
    public class Fish : Shell
    {
        public Fish()
        {
        }

        public override Critter.Action GetMove(ICritterInfo info)
        {
            // Must call this method for Shell to work
            SetFields(info);

            PrintFields(); // For debugging

            if (neighbor[front] == empty)
            {
                return Critter.Action.Hop;
            }
            else if (neighbor[front] == other)
            {
                return Critter.Action.Infect;
            }
            else
            {
                return Critter.Action.Right;
            }
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

        public override Color GetColor()
        {
            if (threat[front])
            {
                return Color.Red;
            }
            else if (threat[right])
            {
                return Color.Blue;
            }
            else if (threat[back])
            {
                return Color.Yellow;
            }
            else if (threat[left])
            {
                return Color.Green;
            }
            else
            {
                return Color.Black;
            }
        }
    }
}
