using System;
using System.Drawing;
using CritterSimulator.Game;
using CritterSimulator.CritterStorage.Premade;

namespace CritterSimulator.CritterStorage.Ryan
{
    public class Pheonix : Shell
    {
        public bool behaviorA = false;
        public bool behaviorB = false;
        public bool behaviorC = false;
        public bool behaviorD = false;
        public bool hasBounced = false;

        public int stepsUntilPing = 350;
        public int spinDuration = 30;
        public int bounceDuration = 150;
        public int turn1 = 2;
        public int turn2 = 2;

        public Pheonix()
        {
        }

        public override Critter.Action GetMove(ICritterInfo info)
        {
            step++;
            SetFields(info);

            // Start condition for behaviorA
            if (step < spinDuration && neighbor[left] != wall && neighbor[right] != wall && neighbor[back] != wall && neighbor[front] != wall)
            {
                behaviorA = true;
            }
            // Stop condition for behaviorA
            if (step >= spinDuration)
            {
                behaviorA = false;
            }
            // Start condition for BehaviorB
            if ((step >= spinDuration && !behaviorC) || bounceDuration == 0)
            {
                behaviorB = true;
            }
            // Stop condition for BehaviorB
            if (neighbor[front] == wall && !hasBounced)
            {
                behaviorB = false;
            }
            // Start condition for BehaviorC the bounce
            if (neighbor[front] == wall && !hasBounced)
            {
                hasBounced = true;
                behaviorC = true;
            }
            // Stop condition for BehaviorC
            if (bounceDuration == 0)
            {
                behaviorC = false;
            }
            // Start condition for BehaviorD
            if (step % stepsUntilPing == 0)
            {
                behaviorD = true;
                behaviorB = false;
            }
            // Stop condition for BehaviorD
            if (step % stepsUntilPing == 1)
            {
                behaviorD = false;
                behaviorB = true;
            }

            // Executing activated behaviors.
            if (behaviorA)
            {
                if (neighbor[front] == other)
                {
                    return infect;
                }
                else if (neighbor[right] == other)
                {
                    return R;
                }
                else if (neighbor[left] == other)
                {
                    return L;
                }
                else
                {
                    return R;
                }
            }
            else if (behaviorB)
            {
                if (neighbor[front] == other)
                {
                    return infect;
                }
                else if (neighbor[right] == other)
                {
                    return R;
                }
                else if (neighbor[left] == other)
                {
                    return L;
                }
                else if (neighbor[front] == empty)
                {
                    return hop;
                }
                else
                {
                    return R;
                }
            }
            else if (behaviorC)
            {
                if (turn1 > 0)
                {
                    turn1--;
                    return R;
                }
                else if (turn1 == 0)
                {
                    turn1 = -1;
                    return hop;
                }
                else if (turn2 > 0)
                {
                    turn2--;
                    return R;
                }
                else if (bounceDuration > 0)
                {
                    bounceDuration--;
                    if (threat[right])
                    {
                        return R;
                    }
                    else if (threat[left])
                    {
                        return L;
                    }
                    else if (neighbor[left] != wall && neighbor[right] != wall && neighbor[back] != wall && neighbor[front] != wall)
                    {
                        return infect;
                    }
                    else
                    {
                        behaviorC = false;
                    }
                }
            }
            else if (behaviorD)
            {
                return R;
            }

            behaviorB = true;
            return infect;
        }

        public override string ToString()
        {
            if (direction == north)
            {
                return "^";
            }
            else if (direction == east)
            {
                return ">";
            }
            else if (direction == south)
            {
                return "v";
            }
            else
            {
                return "<";
            }
        }
    }
}
