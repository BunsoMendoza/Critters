package critter_storage.ryan;
import game.*;
import critter_storage.premade.*;
import java.awt.*;

public class Atom extends Critter {
   private int direction = 0;
   private int step = 0;
   private final int AMP = 15;//Change atom vibration amplitude 
   private boolean alt = true;
   
   public Atom() {
      
   }
   
   public Action getMove(CritterInfo info) {
      setDirection(info);
      
      if (info.getFront() == Neighbor.OTHER) {
         return Action.INFECT;
      }
      
      if (info.getFront() == Neighbor.WALL || info.getFront() == Neighbor.SAME || info.getRight() == Neighbor.WALL) {
         return Action.LEFT;
      }


      if (alt) {
         if (step % AMP == 0) {
            alt = false;   
         }
         
         step++;
          
         return Action.HOP;  
      } else {
         if (step % AMP == 0) {
            alt = true;           
         }
         step++;
         return Action.LEFT;
      }
      
   }
   
   public Color getColor() {
      return Color.RED; 
   }
   public String getCritterName(){
      return "atom";
   }
   public String toString() {
      
      if (direction == 1) {
         return ".";
      } else if (direction == 2) {
         return "o";
      } else if (direction == 3) {
         return ".";
      } else {
         return "o";
      }
   } 
   
   public void setDirection(CritterInfo info) {
      if (info.getDirection() == Direction.NORTH) {
         direction = 1;
      } else if (info.getDirection() == Direction.EAST) {
         direction = 2;
      } else if (info.getDirection() == Direction.SOUTH) {
         direction = 3;
      } else {
         direction = 4;
      }      
   } 
   
   
}
 