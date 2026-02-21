using System;
using System.Drawing;
using CritterSimulator.Game;
using CritterSimulator.CritterStorage.Premade;

namespace CritterSimulator.CritterStorage.Ryan
{
    public class Bat : Shell
    {
        private bool runBounce = false;
        private bool runHop = false;
        private bool runSpin = false;

        // Number of spins after hop
        private int spin = 100;

        public Bat()
        {
        }

        public override Critter.Action GetMove(ICritterInfo info)
        {
            SetFields(info);

            // Infect always if possible
            if (neighbor[front] == other)
            {
                return infect;
            }

            // Turn two
            // If we hit a wall for the first time turn once more
            if (runBounce && !runHop)
            {
                runHop = true;
                return R;
            }

            if (runHop && !runSpin)
            {
                runSpin = true;
                return hop;
            }

            if (spin > 0 && runSpin)
            {
                spin--;
                return L;
            }

            // If you meet your kin flip a coin L or R
            if (neighbor[front] == same)
            {
                bool b = new Random().NextDouble() < 0.5;
                if (b)
                {
                    return R;
                }
                else
                {
                    return L;
                }
            }

            // After bouncing always hop if empty
            if (neighbor[front] == empty)
            {
                return hop;
            }

            // If a wall is in front and we haven't bounced, run bounce
            if (neighbor[front] == wall || !runBounce)
            {
                runBounce = true;
                return R;
            }

            // If a wall is in front and we have bounced turn right.
            if (neighbor[front] == wall)
            {
                return L;
            }

            Console.WriteLine("ERROR end of getMove decision tree");
            return infect;
        }

        // Critter display string
        public override string ToString()
        {
            return "X";
        }
    }
}
