package critter_storage.ryan;
import game.*;
import critter_storage.premade.*;
import java.awt.*;
import java.util.*;

public class Snake extends Critter {
   private int direction = 0;
   private int step = 0;
   private final int AMP = 1; 
   private boolean alt = true;
   
   public Snake() {
      
   }
   
   public Action getMove(CritterInfo info) {
   
      if (info.getFront() == Neighbor.OTHER) {
         return Action.INFECT;
      }
         
//      if (info.leftThreat()) {
//         return Action.RIGHT;
//     } else if (info.rightThreat()) {
//         return Action.LEFT;
//      }
      
    
      if (info.getFront() == Neighbor.WALL || info.getFront() == Neighbor.SAME) {
         return Action.RIGHT;
      } else if (step == 0 && info.getLeft() == Neighbor.WALL) {
         step++;
         return Action.HOP; 
      } else if (step == 1 && info.getLeft() == Neighbor.WALL) {
         step=0;
         return Action.RIGHT;
      } else {
         return Action.HOP;
      }
       

      
   }
   
   public Color getColor() {
      return Color.RED; 
   }
   
   public String toString() {
      
      if (direction == 1) {
         return "I";
      } else if (direction == 2) {
         return "I";
      } else if (direction == 3) {
         return "I";
      } else {
         return "I";
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
 