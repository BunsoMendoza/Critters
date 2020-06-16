package critter_storage.ryan;
import game.*;
import critter_storage.premade.*;
import java.awt.*;
import java.util.*;

public class Rihno extends Shell {

   private final int SPIN = 20;
   private final int PAUSE_FOR = 150;
   private final int PAUSE_EVERY = 400;
   
   private int turn1 = 2;
   private int turn2 = 2;
   private int pauseStep = 0;
   
   private boolean doPause = false;


   
   public Rihno() {
   
   }
   public Action getMove(CritterInfo info) {
      setFields(info);
      
      //Spin around infecting for the first SPIN steps
      if (step <= SPIN && !hasWall) {
         if (neighbor[front] == other) {
            return infect;
         } else if (neighbor[right] == other) {
            return R;
         } else {
            return L;
         }  
      }
      //When it finds its first wall turn on bounce 
      if (neighbor[front] == wall && !seenFirstWall) {
         seenFirstWall = true;
         doPause = true;
      }
      
      //Turn 180deg hop once turn 180deg infect 'pause' times. 
      if (doPause) {
         if (neighbor[front] == other) {
            return infect;
         } else if (turn1 > 0) {
            turn1--;
            return R;
         } else if (turn1 == 0) {
            turn1--;
            return hop;
         } else if (turn2 > 0) {
            turn2--;
            return R;
         } else if (pauseStep <= PAUSE_FOR && !hasWall && !(neighbor[front] == same && neighbor[back] == same)) { //Skip step if next to a wall.
            if (threat[right]) {
               return R;
            } else if (threat[left]) {
               return L;
            } else {
               pauseStep++;
               return infect;
            }    
         } else {
            doPause = false;
            turn1 = 2;
            turn2 = 2;
            pauseStep = 0; 
            return infect; 
         }
      }
      
      //After PAUSE_EVERY steps turn right once to ping off wall then run bounce again at next wall hit. 
      if (step % PAUSE_EVERY == 0) {
         seenFirstWall = false;
         return R;
         
      } 
     
      //Default behavior, like bear but turning right instead of left.
      if (neighbor[front] == other) {
         return infect; 
      } else if (neighbor[front] == empty) {
         return hop;
      } else {
         return R; 
      }
      
      
      
         
   }
}