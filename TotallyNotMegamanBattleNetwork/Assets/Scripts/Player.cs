using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum playerID
{
    player1,
    player2
}

public class Player {

    int initialHp = 100;
    int invTicks = 4;
    int currentInvTicks = 0;
    bool alive;
    public bool Alive { get { return alive; } }
    int currentHp;
    bool invulnerable = false;
    public int CurrentHp { get { return currentHp; } }
    playerID myID;
    public bool shoots;
    public playerID GetID { get { return myID; } }
    Vector2Int currentPos;
    public Vector2Int GetPos { get { return currentPos; } }
    List<ISpecial> currentSpecials;    

    public Player(Vector2Int spawnPos, playerID iD)
    {
        myID = iD;
        currentHp = initialHp;
        alive = true;
        currentPos = spawnPos;
        currentSpecials = new List<ISpecial>();
    }    

    public ISpecial GetSpecial()
    {
        ISpecial special = currentSpecials[0];
        currentSpecials.RemoveAt(0);
        return special;
    }
    public void AddSpecial (ISpecial special)
    {
        currentSpecials.Add(special);
    }
    public bool HasSpecial()
    {
        return currentSpecials.Count > 0;
    }
    public void ReciveDamage(int damage)
    {
        if (!invulnerable && alive)
        {
            currentHp -= damage;
            invulnerable = true;
            if (currentHp <= 0)
            {
                alive = false;//RIP
                currentHp = 0;
            }
        }       
    }
    public void ReduceInvTime()
    {
        if (invulnerable)
        {
            currentInvTicks++;
            if (currentInvTicks > invTicks)
            {
                invulnerable = false;
                currentInvTicks = 0;
            }
        }
    }

    public bool TryToMove(Vector2Int mov)
    {
        
        if ((currentPos + mov).x < 3 + ((int)myID * 3) && (currentPos + mov).x >= 0 + ((int)myID * 3) && (currentPos + mov).y < 3 && (currentPos + mov).y >= 0)
        {
            currentPos += mov;
            return true;
        }
               
        return false;
    }
}
