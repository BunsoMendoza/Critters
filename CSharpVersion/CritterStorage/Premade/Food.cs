using System.Drawing;
using CritterSimulator.Game;

namespace CritterSimulator.CritterStorage.Premade
{
    public class Food : Critter
    {
        public override Action GetMove(ICritterInfo info)
        {
            return Action.Infect;
        }

        public override Color GetColor()
        {
            return Color.Green;
        }

        public override string ToString()
        {
            return "F";
        }
    }
}
