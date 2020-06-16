// Critter class extension for a critter called a 'EMBO'
package critter_storage.josh;
import critter_storage.premade.*;
import game.*;
import java.awt.*;
import java.util.Random;

public class EMBO extends Critter {
	// private variables to measure the count of the instances and the previous string of the EMBO
	// and the different possible String values of the EMBO
	private int count;
	private String previousName;
	private final String[] EMBONames = {"E", "M", "B", "O"};
	private int EMBOIndex;
	
	// constructor of the critter EMBO
	public EMBO() {
		this.count = 0;
		this.EMBOIndex = 0;
	}

	public String getName(){
        return "EMBO";

    }
	
	// returns the color of the EMBO
	public Color getColor() {
        Random rand = new Random();
      int numOfColors = 3;
      int currentColor = rand.nextInt(numOfColors);

      switch(currentColor){
            default:
            return  Color.BLACK;
            
            case 0:
            return  Color.BLUE;

            case 1: 
            return  Color.ORANGE;

           

      }
      
    }
	
	// returns the string value of EMBO
	public String toString() {
		this.count = this.count + 1;
		if ((this.count - 1) % 6 == 0) {
			if (this.EMBOIndex == 4) {
				this.EMBOIndex = 0;
			}
			this.EMBOIndex = this.EMBOIndex + 1;
			return EMBONames[EMBOIndex - 1].toString();
		} else {
			this.previousName = EMBONames[EMBOIndex - 1];
		}
		return this.previousName;
	}
	
	// returns the move to be made by EMBO
	public Action getMove(CritterInfo info) {
		if (info.getFront() == Neighbor.OTHER) {
			return Action.INFECT;
		} else if (info.getFront() == Neighbor.WALL) {
			return Action.LEFT;			
		} else if (info.getFront() == Neighbor.SAME) {
			return Action.RIGHT;
		} else {
			return Action.HOP;
		}
	}
}