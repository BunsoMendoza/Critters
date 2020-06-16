package critter_storage.bunso;

import java.awt.Color;
import java.util.HashMap;
import java.util.ArrayList;
import java.util.Map.Entry;

import critter_storage.premade.*;
import game.CritterInfo;

public class ReusableMethods extends Critter {

    public String toString() {
        return null;
    }

    public Color getColor() {
        return null;
    }

    public Action getMove(CritterInfo info) {
        return null;
    }
    public boolean touchedWall = false;
    public int hops = 0;
    public ArrayList<String> walls = new ArrayList<>();
    public HashMap<String, Neighbor> hmap = new HashMap<String, Neighbor>();
    public HashMap<String, Boolean> threats = new HashMap<String, Boolean>();
    // To compare neighbor[] ie. (neighbor[back] == wall) etc.
    public Neighbor wall = Neighbor.WALL;
    public Neighbor empty = Neighbor.EMPTY;
    public Neighbor friend = Neighbor.SAME;
    public Neighbor enemy = Neighbor.OTHER;

    // To compare direction ie. (direction == north) etc.
    public Direction north = Direction.NORTH;
    public Direction east = Direction.EAST;
    public Direction south = Direction.SOUTH;
    public Direction west = Direction.WEST;

    public Direction currentDirection;

    // To return action ie. return hop;
    public Action hop = Action.HOP;
    public Action infect = Action.INFECT;
    public Action turnLeft = Action.LEFT;
    public Action turnRight = Action.RIGHT;

    // Set neightbor[] field.
    public void checkNeighbors(CritterInfo info) {
        currentDirection = info.getDirection();
        hmap.clear();
        walls.clear();
        hmap.put("front", info.getFront());
        hmap.put("right", info.getRight());
        hmap.put("back", info.getBack());
        hmap.put("left", info.getLeft());

        for (Entry<String, Neighbor> e : hmap.entrySet()) {
            if (e.getValue() == enemy) {
                threats.put(e.getKey(), true);
            }
        }
        for (Entry<String, Neighbor> e : hmap.entrySet()) {     
            if (e.getValue() == wall) {
                walls.add(e.getKey());
            }
        }
        
    }

    public boolean isTouchingWall(){
        if(walls.isEmpty()){
            return false;
        }else{
            touchedWall = true;
            return true;
        }
    }

    //touching
    public Action wallCheck() { 
        
        int wallCount = 0;

        if(walls.isEmpty()){
            System.out.println("wallCheck() was called. But critter isnt touching wall.");
            return hop;
        }else{
            wallCount = walls.size();
        }

            switch(wallCount){
                
                case 1:
                    if(walls.get(0) == "front"){
                        facedDirection();
                        boolean b = Math.random() < 0.5;
                        return (b) ? turnLeft : turnRight;
                    }else if(walls.get(0) == "right"){
                        return turnLeft;
                    }else if(walls.get(0) == "left"){
                        return turnRight;
                    }else{-
                        return hop;
                    }
                   
                case 2:  
                    touchingTwoWalls(walls.get(0), walls.get(1));

                default:
                    return hop; 
            } 
            
    }

   public Action touchingTwoWalls(String wallOne, String wallTwo){
        if ((wallOne == "front"|| wallOne == "back") && wallTwo == "right"){
             return turnLeft;
        }else if((wallOne == "front"|| wallOne == "back") && wallTwo == "left"){
            return turnRight;
        }else{
            return infect;
        }

   }

   public Direction facedDirection(){
       switch(currentDirection){
            case NORTH:
                return Direction.SOUTH;
            case SOUTH:
                return Direction.NORTH;
            case EAST:
                return Direction.WEST;
            case WEST:
                return Direction.EAST;
            default:
                System.out.println("Direction was not set");
                return currentDirection;

       }
   }

}
    

