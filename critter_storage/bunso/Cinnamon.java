package critter_storage.bunso;
import critter_storage.premade.*;
import game.*;
import java.awt.*;
import java.util.Random;

public class Cinnamon extends Critter {


    public Action getMove(CritterInfo info) {

        Random rand = new Random();
        int tf = 2;
        int current = rand.nextInt(tf);
        boolean isRight = true;
        boolean hasBounced = false;

        switch (info.getFront()) {
            default:
                if (hasBounced && isRight) {
                    hasBounced = false;
                    return Action.RIGHT;
                } else if (hasBounced && !isRight) {
                    hasBounced = false;
                    return Action.LEFT;
                } else {
                    return Action.HOP;
                }

            case OTHER:
                return Action.INFECT;

            case SAME:
                return Action.RIGHT;

            case WALL:
                if (current == 0) {
                    isRight = false;
                    hasBounced = true;
                    return Action.LEFT;
                } else if (current == 1) {
                    isRight = true;
                    return Action.RIGHT;
                } else {
                    System.out.println("Error for LEFT OR RIGHT");
                    return Action.LEFT;
                }

        }

    }

    public Color getColor() {
        return Color.RED;
    }

    public String toString() {
        return "+";
    }

  

}