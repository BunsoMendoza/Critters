using System.Drawing;
using CritterSimulator.Game;
using CritterSimulator.CritterStorage.Premade;

namespace CritterSimulator.CritterStorage.Bunso
{
    public class Cartographer : ReusableMethods
    {
        public override Critter.Action GetMove(ICritterInfo info)
        {
            CheckNeighbors(info);

            if (info.GetFront() == enemy)
            {
                return Critter.Action.Infect;
            }
            else if (IsTouchingWall())
            {
                return WallCheck();
            }
            else if (info.GetFront() == friend)
            {
                touchedWall = false;
                hops = 0;
                return turnLeft;
            }
            else
            {
                hops++;
                return hop;
            }
        }

        public override string ToString()
        {
            return "\u221E"; // ∞
        }

        public override Color GetColor()
        {
            return Color.Red;
        }
    }
}
