package critter_storage.bunso;
import critter_storage.premade.*;
import game.*;
import java.awt.*;


public class ZigZag extends Critter {
    int sumOfN = 0;
    int N = 1;
    
    public String getName(){
        return "ZigZag";

    }

    public Action getMove(CritterInfo info) {
        if(N > 20){
            N = 1;
            sumOfN = 0;
        }

        if (info.getFront() == Neighbor.OTHER) {
            return Action.INFECT;
        } else if (info.getFront() == Neighbor.WALL) {
            return Action.LEFT;
        } else if (info.getFront() == Neighbor.SAME) {
            return Action.RIGHT;
        } else {

            if (sumOfN == 0 || N % sumOfN != 0) {
                N++;
                sumOfN += N;
                return Action.HOP;   
            }else if(N % sumOfN == 0 && sumOfN % 2 == 0){
                N++;
                sumOfN += N;
                return Action.LEFT;
            }else if(N % sumOfN == 0){
                N++;
                sumOfN += N;
                return Action.RIGHT;
            }else{
                N++;
                sumOfN += N;
                return Action.INFECT;
            }
        }

    }

    public Color getColor() {
        return Color.ORANGE;
    }

    public String toString() {
        return "Z";
    }
}