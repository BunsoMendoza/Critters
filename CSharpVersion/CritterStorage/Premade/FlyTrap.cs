using System.Drawing;
using CritterSimulator.Game;

namespace CritterSimulator.CritterStorage.Premade
{
    // This defines a simple class of critters that infect whenever they can and
    // otherwise just spin around, looking for critters to infect. This simple
    // strategy turns out to be surprisingly successful.
    public class FlyTrap : Critter
    {
        public override Action GetMove(ICritterInfo info)
        {
            if (info.GetFront() == Neighbor.Other)
            {
                return Action.Infect;
            }
            else
            {
                return Action.Left;
            }
        }

        public override Color GetColor()
        {
            return Color.Red;
        }

        public override string ToString()
        {
            return "T";
        }
    }
}
