import java.awt.*;
import java.util.*;

public class Pheonix extends Shell {
   
   public boolean behaviorA = false;
   public boolean behaviorB = false;
   public boolean behaviorC = false;
   public boolean behaviorD = false; 
   public boolean hasBounced = false;
    
   public int stepsUntillPing = 350;//Ping is when they take one right turn after circling the border clockwise for x steps
   public int spinDuration = 30;//Adjust the duration of the initial spin
   public int bounceDuration = 150;//Adjusts how long the bounce and infect will last
   public int turn1 = 2;//Two turns L or R = 180 degree turn
   public int turn2 = 2;//Two turns L or R = 180 degree turn 
      
 
   public Pheonix() {
         
   }
   public Action getMove(CritterInfo info) {
      
      step++;
      setFields(info);//for use of Shell 
      
      //Checking conditions for swithcing on different behaviors
      //Start condition for behaviorA
      if (step < spinDuration && neighbor[left] != wall && neighbor[right] != wall && neighbor[back] != wall && neighbor[front] != wall) {
         behaviorA = true;
      }
      //Stop condition for behaviorA
      if (step >= spinDuration) {
         behaviorA = false;
      } 
      //Start condition for BehaviorB
      if ((step >= spinDuration && !behaviorC) || bounceDuration == 0) {
         behaviorB = true;
      }
      //Stop condition for BehaviorB 
      if (neighbor[front] == wall && !hasBounced) {
         behaviorB = false; 
      }
      //Start condition for BehaviorC the bounce 
      if (neighbor[front] == wall && !hasBounced  ) {
         hasBounced = true;
         behaviorC = true; 
      }
      //Stop condition for BehaviorC
      if (bounceDuration == 0) {
         behaviorC = false;
      }
      //Start condition for BehaviorD
      if (step % stepsUntillPing == 0) {
         behaviorD = true;
         behaviorB = false;
      } 
      //Stop condition for BehaviorD
      if (step % stepsUntillPing == 1) {
         behaviorD = false;
         behaviorB = true; 
      }
          
      //Executing activated behaviors.
      //Spin 'spin' times infecting and turning toward flanking others.      
      if (behaviorA) { 
         if (neighbor[front] == other) {
            return infect;
         } else if (neighbor[right] == other) {
            return R; 
         } else if (neighbor[left] == other) {
            return L;
         } else {
            return R;
         }
    
      //Basic bear behavior turning R with turning towards flanking others.
      } else if (behaviorB) { 
         if (neighbor[front] == other) {
            return infect;
         } else if (neighbor[right] == other) {
            return R;
         } else if (neighbor[left] == other) {
            return L;
         } else if (neighbor[front] == empty) {
            return hop;
         } else {
            return R;
         }
      //Bounce and wait for bounceDuration while infecting and turning towards flanking threat.
      } else if (behaviorC) {
         if (turn1 > 0) {
            turn1--;
            return R;
         } else if (turn1 == 0) {
            turn1 = -1;//to end first turn and hop
            return hop;
         } else if (turn2 > 0) {//second turn 
            turn2--;
            return R;
         } else if (bounceDuration > 0) {//infect for bounceDuration (can change to spin)
            bounceDuration--;                                                                                              
            if (threat[right]) {
               return R;
            } else if (threat[left]) {
               return L;
            //dont do this step if next to a wall (they get stuck)
            } else if (neighbor[left] != wall && neighbor[right] != wall && neighbor[back] != wall && neighbor[front] != wall) {
               return infect;
            } else {
               behaviorC = false; 
            }
         }
      //After 'ping' steps take one right turn to "ping" off the wall. 
      } else if (behaviorD) {
         return R;
         
      }
      behaviorB = true;
      return infect;
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
}