package critter_storage.premade;
import game.*;
import java.awt.*;
import java.util.Random;

public class Lion extends Critter{

    
    public String getName(){
        return "Lion";

    }

    public Action getMove(CritterInfo info) {
        if (info.getFront() == Neighbor.OTHER) {
            return Action.INFECT;
        } else if (info.getFront() == Neighbor.WALL || info.getRight() == Neighbor.WALL){
            return Action.LEFT;
        }else if(info.getFront() == Neighbor.SAME) {
            return Action.RIGHT;
        }else{
            return Action.HOP;
        }
    }
   
    public Color getColor() {
        Random rand = new Random();
        int numOfColors = 3;
        int currentColor = rand.nextInt(numOfColors);
    

      switch(currentColor){
            default:
            return  Color.WHITE;
            
            case 0:
            return  Color.RED;

            case 1: 
            return  Color.GREEN;

            case 2: 
            return  Color.BLUE;
        }
      
    }
        
        
    
    public String toString() {
        return "L";
        
    }
}