package critter_storage.premade;
import game.*;
import java.awt.*;
public class Giant extends Critter{
   
    int counter = 0;
    
    public String getName(){
        return "Giant";

    }

    public Action getMove(CritterInfo info) {
        if(info.getFront() == Neighbor.OTHER){
            return Action.INFECT;
        }else if(info.getFront() == Neighbor.SAME || info.getFront() == Neighbor.WALL){
            return Action.RIGHT;
        }else{
            return Action.HOP;
        }
        
    }

    public Color getColor() {
        return Color.GRAY;
    }

    public String toString() {
        if(counter < 6){
            counter++;
            return "fee";
        }else if(counter < 12){
            counter++;
            return "fie";
        }else if( counter < 18){
            counter++;
            return "foe";
        }else if(counter < 24){
            counter++;
            return "fum";
        }else{
            counter = 0;
            return "fee";
        }

    }

        
        
    
}
