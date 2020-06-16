package critter_storage.bunso;
import game.*;
import java.awt.*;


public class Cartographer extends ReusableMethods {  

   
    public Action getMove(CritterInfo info){

        checkNeighbors(info);

        if(info.getFront() == enemy) {
            return Action.INFECT;
        }else if(isTouchingWall()){
            return wallCheck();
        }else if(info.getFront() == friend){
            touchedWall = false;
            hops = 0;
            return turnLeft;    
        }else{
            hops++;
            return hop;
            
        }

    }

 
    @Override
    public String toString(){
        return "∞";

    };

   
    public Color getColor(){
        return Color.RED;

    };


 
   }