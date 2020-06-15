import java.awt.*;

public class Boon extends Critter {
    private boolean attached = false;
    private int stepCounter = 1;
    private int turns = 0;
    private String direction = "Left";
    private int sumOfN = 0;
    private int N = 1;

    public Action getMove(CritterInfo info) {
        if (attached) {
            return attachedPivot(info);
        } else {
            return checkAndMove(info);
        }

    }

    public Color getColor() {
        return Color.PINK;
    }

    public String toString() {
        return "#";
    }

    private Action attachedPivot(CritterInfo info) {
        if (stepCounter % 200 == 0) {
            attached = false;
            // System.out.println("dettached");
            return Action.LEFT;
        }

        if (turns == 3 && direction == "Left") {
            turns = 0;
            direction = "Right";
        }
        if (turns == 3 && direction == "Right") {
            turns = 0;
            direction = "Left";
        }

        if (info.getFront() == Neighbor.OTHER) {
            stepCounter++;
            return Action.INFECT;
        } else if (direction == "Left") {
            stepCounter++;
            turns++;
            return Action.LEFT;
        } else {
            stepCounter++;
            turns++;
            return Action.RIGHT;
        }

    }

    private Action checkAndMove(CritterInfo info) {

        if (N > 20) {
            N = 1;
            sumOfN = 0;
        }

        switch(info.getFront()){
            default:
            return Action.INFECT; 

            case OTHER:
            return Action.INFECT;

            case SAME:
            return Action.RIGHT;

            case WALL:
            attached = true;
            System.out.println("attached");
            turns++;
            return Action.LEFT;

            case EMPTY:
            if (sumOfN == 0 || N % sumOfN != 0) {
                N++;
                sumOfN += N;
                return Action.HOP;
            } else if (N % sumOfN == 0 && sumOfN % 2 == 0) {
                N++;
                sumOfN += N;
                return Action.LEFT;
            } else if (N % sumOfN == 0) {
                N++;
                sumOfN += N;
                return Action.RIGHT;
            } else {
                N++;
                sumOfN += N;
                return Action.INFECT;
            }

        }
        
       
    }

}