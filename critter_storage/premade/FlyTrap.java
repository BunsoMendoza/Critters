package critter_storage.premade;
import game.*;
import java.awt.*;
// This defines a simple class of critters that infect whenever they can and
// otherwise just spin around, looking for critters to infect.  This simple
// strategy turns out to be surpisingly successful.



public class FlyTrap extends Critter {
    
    public Action getMove(CritterInfo info) {
        if (info.getFront() == Neighbor.OTHER) {
            return Action.INFECT;
        } else {
            return Action.LEFT;
        }
    }

    public Color getColor() {
        return Color.RED;
    }
    public String getCritterName(){
        return "FlyTrap";
    }

    public String toString() {
        return "T";
    }
}