//This critter is testing the inheratence from Shell.jav
package critter_storage.ryan;
import game.*;
import critter_storage.premade.*;
import java.awt.*;
import java.util.*;

public class Fish extends Shell {
   
   public Fish() {
   
   }
   public String getName(){
      return "Fish";

  }
   
   public Action getMove(CritterInfo info) {
      
      //HERE
      setFields(info);//Must call this method for Shell to work; 
      //HERE
      
      printFields();//For debugging; 
     
      
      if (neighbor[front] == empty) {
      
         return Action.HOP; 
         
      } else if (neighbor[front] == other) {
      
         return Action.INFECT;
         
      } else {
         return Action.RIGHT;
      }       
      
   } 
   
   public String toString() {
      if (direction == north) {
         return "^";
      } else if (direction == east) {
         return ">";
      } else if (direction == south) {
         return "v";
      } else { //direction == west
         return "<";
      }
   }
   
   public Color getColor() {
      if (threat[front]) {
         return Color.RED;
      } else if (threat[right]) {
         return Color.BLUE;
      } else if (threat[back]) {
         return Color.YELLOW;
      } else if (threat[left]) {
         return Color.GREEN;
      } else {
         return Color.BLACK;
      } 
      
   }
}