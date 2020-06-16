package game;
import critter_storage.premade.*;
import critter_storage.bunso.*;
import critter_storage.ryan.*;
import critter_storage.josh.*;

// CSE 142 Homework 8 (Critters)
// Authors: Stuart Reges and Marty Stepp
//
// CritterMain provides the main method for a simple simulation program.  Alter
// the number of each critter added to the simulation if you want to experiment
// with different scenarios.  You can also alter the width and height passed to
// the CritterFrame constructor.

public class CritterMain {
    public static void main(String[] args) {
        CritterFrame frame = new CritterFrame(70, 50);

        // uncomment each of these lines as you complete these classes
        frame.add(30, Bear.class);
        //frame.add(30, Lion.class);
        //frame.add(30, ZigZag.class);
        //frame.add(30, Boon.class);
        frame.add(30, Cinnamon.class);
        //frame.add(30, Giant.class);
        frame.add(30, Atom.class);
        //frame.add(30, EMBO.class);

        frame.add(30, FlyTrap.class);
        frame.add(30, Food.class);

        frame.start();
    }
}
