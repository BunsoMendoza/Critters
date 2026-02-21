using System.Drawing;
using CritterSimulator.Game;

namespace CritterSimulator.CritterStorage.Premade
{
    // This is the superclass of all of the Critter classes. Your class should
    // extend this class. The class provides several kinds of constants:
    //
    //    type Neighbor  : Wall, Empty, Same, Other
    //    type Action    : Hop, Left, Right, Infect
    //    type Direction : North, South, East, West
    //
    // Override the following methods to change the behavior of your Critter:
    //
    //     public Action GetMove(ICritterInfo info)
    //     public Color GetColor()
    //     public override string ToString()

    public abstract class Critter
    {
        public enum Neighbor
        {
            Wall, Empty, Same, Other
        }

        public enum Action
        {
            Hop, Left, Right, Infect
        }

        public enum Direction
        {
            North, South, East, West
        }

        public abstract new string ToString();
        public abstract Color GetColor();
        public abstract Action GetMove(ICritterInfo info);

        // This prevents critters from trying to redefine the definition of
        // object equality, which is important for the simulator to work properly.
        public sealed override bool Equals(object other)
        {
            return ReferenceEquals(this, other);
        }

        public sealed override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
