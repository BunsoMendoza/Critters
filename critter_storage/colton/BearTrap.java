
package critter_storage.colton;
import critter_storage.premade.*;
import game.*;
import java.awt.*;

import javax.swing.text.AttributeSet.ColorAttribute;

//import org.graalvm.compiler.nodes.NodeView.Default;


public class BearTrap extends Critter {

    Action myAction;
    boolean foundWall = false;
    int foundWallStage = 0;
    boolean protectingFriend = false;

    Color myColor;

    public String getCritterName(){
        return "BearTrap";

    }
    public Color getColor()
    {
        return myColor;
    }
    public String toString()
    {
        return "G";
    }

    public Action getMove(final CritterInfo info) {

        myAction = Action.INFECT;
        myColor = Color.BLACK;

        if (!foundWall) {
            myColor = Color.BLUE;
            findWall(info);
        }
        else if (foundWall){
            myColor = Color.RED;
            positionToWall(info);
        }

        return myAction;

    }

    private void findWall(final CritterInfo info)
    {
        switch (info.getFront()){

            case EMPTY:
            myAction = Action.HOP;

            case SAME:
            protectingFriend = true;
                break;

            case WALL:
            foundWall = true;
                break;

            default:
                break;
        }
    }

    private void positionToWall(final CritterInfo info){
        switch (foundWallStage){

            case 0:
            myAction = Action.RIGHT;
            break;

            case 1:
            myAction = Action.RIGHT;
            break;

            case 2:
            myAction = Action.HOP;
            break;

            case 3:
            myAction = Action.RIGHT;
            break;

            case 4:
            myAction = Action.RIGHT;
        }

        foundWallStage++;
    }

}


