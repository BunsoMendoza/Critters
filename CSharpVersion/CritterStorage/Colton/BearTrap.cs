using System.Drawing;
using CritterSimulator.Game;
using CritterSimulator.CritterStorage.Premade;

namespace CritterSimulator.CritterStorage.Colton
{
    public class BearTrap : Critter
    {
        private Action myAction;
        private bool foundWall = false;
        private int foundWallStage = 0;
        private bool protectingFriend = false;

        private Color myColor;

        public override Color GetColor()
        {
            return myColor;
        }

        public override string ToString()
        {
            return "G";
        }

        public override Action GetMove(ICritterInfo info)
        {
            myAction = Action.Infect;
            myColor = Color.Black;

            if (!foundWall)
            {
                myColor = Color.Blue;
                FindWall(info);
            }
            else if (foundWall)
            {
                myColor = Color.Red;
                PositionToWall(info);
            }

            return myAction;
        }

        private void FindWall(ICritterInfo info)
        {
            switch (info.GetFront())
            {
                case Neighbor.Empty:
                    myAction = Action.Hop;
                    goto case Neighbor.Same;

                case Neighbor.Same:
                    protectingFriend = true;
                    break;

                case Neighbor.Wall:
                    foundWall = true;
                    break;

                default:
                    break;
            }
        }

        private void PositionToWall(ICritterInfo info)
        {
            switch (foundWallStage)
            {
                case 0:
                    myAction = Action.Right;
                    break;

                case 1:
                    myAction = Action.Right;
                    break;

                case 2:
                    myAction = Action.Hop;
                    break;

                case 3:
                    myAction = Action.Right;
                    break;

                case 4:
                    myAction = Action.Right;
                    break;
            }

            foundWallStage++;
        }
    }
}
