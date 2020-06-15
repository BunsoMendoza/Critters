
import java.awt.*;

public class Bear extends Critter { 
    boolean polar = true;
    boolean isOdd = true;

    public Bear (boolean polar) {
        this.polar = polar;
    }

    public Action getMove(CritterInfo info) {
        if (info.getFront() == Neighbor.OTHER) {
            return Action.INFECT;
        } else if (info.getFront() == Neighbor.EMPTY){
            return Action.HOP;
        }else{
            return Action.LEFT;
            
        }
    }
   
    public Color getColor() {
        
          if(polar){          
            return Color.WHITE;
          }else{
            return Color.BLACK;
          }       
    }
        
    public String toString() {
        if(isOdd){ 
            isOdd = false;
            return "/";
        }else{
            isOdd = true;
            return "\\";
        }
    }
}
