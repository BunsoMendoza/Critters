package critter_storage.ryan;
import game.*;
import critter_storage.premade.*;
import java.awt.*;
import java.util.*;
//This class is an empty shell that MIGHT make designing new critters a little
//easier and MAYBE a bit more intuitive. There is probably a better way of doing this and it may be a total 
//waste of time, we'll see. 

//Be sure to call 'setFields()' in the getAction() method of the of the subclass to set the fields.
//You can also add 'printFields(') for debugging.


public class Shell extends Critter {
   
   public Color getColor() {return null;}
   //step counter should line up with simulation step counter.
   public int step = 0;
   
   //Is true if critter is touching a wall on any side.
   public boolean hasWall = false;
   
   //Is true after critter has seen its first wall. 
   public boolean seenFirstWall = false;  
   
   //Field that stores the 4 current neighbor constants for this critter - ie. Neighbor.WALL, ... Neighbor.OTHER.   
   public Neighbor[] neighbor = new Neighbor[4];
   
   //Field that stores the 4 current threat booleans - ie. false, false, true, false.
   public boolean[] threat = new boolean[4]; // ie. threat[
   
   //Field that stores this critters current direction - ie. Direction.NORTH. 
   public Direction direction = null;
   
   //To compare neighbor[] ie. (neighbor[back] == wall) etc. 
   public Neighbor wall = Neighbor.WALL;
   public Neighbor empty = Neighbor.EMPTY;
   public Neighbor same = Neighbor.SAME;
   public Neighbor other = Neighbor.OTHER;
   
   //To compare direction ie. (direction == north) etc.
   public Direction north = Direction.NORTH;
   public Direction east = Direction.EAST;
   public Direction south = Direction.SOUTH;
   public Direction west = Direction.WEST;
   
   //To return action ie. return hop;
   public Action hop = Action.HOP;
   public Action infect = Action.INFECT;
   public Action L = Action.LEFT;
   public Action R = Action.RIGHT;
   
   //To ask about neightbor[] 
   public int front = 0; // ie. neighbor[0] = neighbor[front] etc.
   public int right = 1; 
   public int back = 2; 
   public int left = 3; 
   
   //Constructor 
   public Shell() {
      
   }
   
   //Normally overidden, left for debugging
   public Action getMove(CritterInfo info) {
      //Set fields.
      //setFields(info);
      //System.out.println(Arrays.toString(neighbor));
      //System.out.println(Arrays.toString(threat));
      //System.out.println(direction);

      //getMove start.
      return Action.INFECT;  
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
   //Set neightbor[] field.
   public void setNeighbors(CritterInfo info) {
      neighbor[0] = info.getFront();
      neighbor[1] = info.getRight();
      neighbor[2] = info.getBack();
      neighbor[3] = info.getLeft();   
   }
   //Set threat[] field.
   public void setThreat(CritterInfo info) {
      threat[0] = info.frontThreat();
      threat[1] = info.rightThreat();
      threat[2] = info.backThreat();
      threat[3] = info.leftThreat();      
   }
   //Set direction field.
   public void setDirection(CritterInfo info) {
      direction = info.getDirection(); 
   }
   //Set all fields
   public void setFields(CritterInfo info) {
      step++;
      setNeighbors(info);
      setThreat(info);
      setDirection(info);
      hasWall();    
   }
   
   //Print fields to console; 
   public void printFields() {
      System.out.println(Arrays.toString(neighbor));
      System.out.println(Arrays.toString(threat));
      System.out.println(direction);
   }
   //Updates 'hasWall' field to true if critter is next to a wall on any side 
   public void hasWall() {
      if (neighbor[left] == wall || neighbor[right] == wall || neighbor[back] == wall || neighbor[front] == wall) {
         hasWall = true;
      } else {
         hasWall = false; 
      }
   }

   
 
} 