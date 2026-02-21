using CritterSimulator.CritterStorage.Premade;

namespace CritterSimulator.Game
{
    // The ICritterInfo interface defines a set of methods for querying the
    // state of a critter simulation. You should not alter this file.
    public interface ICritterInfo
    {
        Critter.Neighbor GetFront();
        Critter.Neighbor GetBack();
        Critter.Neighbor GetLeft();
        Critter.Neighbor GetRight();
        Critter.Direction GetDirection();
        bool FrontThreat();
        bool BackThreat();
        bool LeftThreat();
        bool RightThreat();
    }
}
