package critter_storage.ryan;
import game.*;



public class Bat extends Shell {

   boolean runBounce = false;
   boolean runHop = false;
   boolean runSpin = false;    
   
   //number of spins after hoop
   int spin = 100;
   
   //constructor 
   public Bat() {
   
   }
   
   public Action getMove(CritterInfo info) {
      
      setFields(info); 
      
      //Infect always if possible 
      if (neighbor[front] == other) {
         return infect;
      }
      
      //Turn two
      //If we hit a wall for the first time turn once more
      if (runBounce && !runHop) {
         runHop = true;
         return R; 
      }
      
      if (runHop && !runSpin) {
         runSpin = true;
         return hop;
      } 
      
      while (spin > 0 && runSpin) {
         spin--;
         return L;
      }
      
      //If you meet your kin flip a coin L or R 
      if (neighbor[front] == same) {
         boolean b = Math.random() < 0.5;
         
         if (b) {
            return R;
         } else {
            return L;
         }
          
      }
      
      //after bouncing always hop if empty 
      if (neighbor[front] == empty) {
         return hop; 
      }
      
      //if a wall is in front and we havnt bounced, run bounce
      if (neighbor[front] == wall || !runBounce) {
         runBounce = true;
         return R;   
      }
      
      //if a wall is in front and we have bounced turn right. 
      if (neighbor[front] == wall) {
         return L;
      }
      
      System.out.println("ERROR end of getMove decision tree");
      return infect;       
      
      
     
         
   }
   
   //Critter display string
   public String toString() {
      return "X";
   }
   
}