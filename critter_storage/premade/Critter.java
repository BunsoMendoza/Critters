package critter_storage.premade;
import game.*;
// This is the superclass of all of the Critter classes.  Your class should
// extend this class.  The class provides several kinds of constants:
//
//    type Neighbor  : WALL, EMPTY, SAME, OTHER
//    type Action    : HOP, LEFT, RIGHT, INFECT
//    type Direction : NORTH, SOUTH, EAST, WEST
//
// Override the following methods to change the behavior of your Critter:
//
//     public Action getMove(CritterInfo info)
//     public Color getColor()
//     public String toString()
//
// The CritterInfo object passed to the getMove method has the following
// available methods:
//
//     public Neighbor getFront();            neighbor in front of you
//     public Neighbor getBack();             neighbor in back of you
//     public Neighbor getLeft();             neighbor to your left
//     public Neighbor getRight();            neighbor to your right
//     public Direction getDirection();       direction you are facing
//     public boolean frontThreat();          threatening critter in front?
//     public boolean backThreat();           threatening critter in back?
//     public boolean leftThreat();           threatening critter to the left?
//     public boolean rightThreat();          threatening critter to the right?

import java.awt.*;

public abstract class Critter{
    //methods that need to be overriden
    //critter name displayed on gui for tracking { return "Critter";}
    public abstract String getName(); 
    //for player character usualy one character {return "A";}
    public abstract String toString();
    //change color of character {return Color.PINK;}
    public abstract Color getColor();
    //handle movement 
    public abstract Action getMove(CritterInfo info);
     
    public static enum Neighbor {
        WALL, EMPTY, SAME, OTHER
    };

    public static enum Action {
        HOP, LEFT, RIGHT, INFECT
    };

    public static enum Direction {
        NORTH, SOUTH, EAST, WEST
    };
    // This prevents critters from trying to redefine the definition of
    // object equality, which is important for the simulator to work properly.
    public final boolean equals(Object other) {
        return this == other;
    }
}