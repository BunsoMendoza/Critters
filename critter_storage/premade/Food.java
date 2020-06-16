package critter_storage.premade;
import game.*;
import java.awt.*;

public class Food extends Critter {
    public Action getMove(CritterInfo info) {
        return Action.INFECT;
    }

    public Color getColor() {
        return Color.GREEN;
    }

    public String toString() {
        return "F";
    }
    public String getCritterName(){
        return "Food";

    }

}
