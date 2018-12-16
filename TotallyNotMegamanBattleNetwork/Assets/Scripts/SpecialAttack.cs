using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttack : ISpecial {

    

    List<damageTile> damageZone;
    int damage;
	public int Damage { get { return damage; } }
    int delaySpeed = 0;
    bool moves = true;
    int spawnDistance = 0;
    int width = 0;
    int length = 0;  
    int activeDamageTime = 2;

    public Vector2Int[] DamageZone
    { get
        {
            Vector2Int[] damPos = new Vector2Int[damageZone.Count];
            for (int i = 0; i < damageZone.Count; i++)
            {
                damPos[i] = damageZone[i].damagePos;
            }
            return damPos;
        }
    }

    private int Direction()
    {
        return caster == playerID.player1 ? 1 : -1;
    }

    public SpecialAttack(string spName, int spDamage)
    {
        damageZone = new List<damageTile>();
        name = spName;
        damage = spDamage;
    }
    public SpecialAttack(string spName,  int spDamage,int delay)
    {
        damageZone = new List<damageTile>();
        name = spName;
        damage = spDamage;
        delaySpeed = delay;
    }
    public SpecialAttack(string spName, int spDamage, int delay, int spSpawnDistance)
    {
        damageZone = new List<damageTile>();
        name = spName;
        damage = spDamage;
        delaySpeed = delay;
        spawnDistance = spSpawnDistance;
    }
    public SpecialAttack(string spName, int spDamage, int delay, int spSpawnDistance, int spWidth, int spLength)
    {
        damageZone = new List<damageTile>();
        name = spName;
        damage = spDamage;
        delaySpeed = delay;
        spawnDistance = spSpawnDistance;
        width = spWidth;
        length = spLength;
    }
    public SpecialAttack(string spName, int spDamage, int delay, int spSpawnDistance, int spWidth, int spLength, int spActiveDamageTime)
    {
        damageZone = new List<damageTile>();
        name = spName;
        damage = spDamage;
        delaySpeed = delay;
        spawnDistance = spSpawnDistance;
        width = spWidth;
        length = spLength;
        activeDamageTime = spActiveDamageTime;
        if (activeDamageTime < delay)
            moves = false;
    }

    class damageTile
    {
        public Vector2Int damagePos;
        public int remainingDuration;
        public damageTile(Vector2Int pos, int time)
        {
            damagePos = pos;
            remainingDuration = time;
        }

    }

    public bool CheckDamage(Vector2Int pos)
    {
        for (int i = 0; i < damageZone.Count; i++)
        {
            if (damageZone[i].damagePos == pos)
                return true;
        }
        return false;
    }
    public override void Call(Vector2Int pos, playerID spCaster)        
    {
        caster = spCaster;
        castPos = pos;
        generateStartingDamageZone();
        Clock.Instance.OnTick += SBehaviour;
        active = true;
    }
   

    private void generateStartingDamageZone()
    {
        Vector2Int spawn;
        for (int i = -width; i <= width; i++)
        {
            for (int j = -length; j <= length; j++)
            {
                spawn = new Vector2Int(castPos.x + spawnDistance * Direction() + length, castPos.y + width);
                if (spawn.x >= 0 && spawn.x <= 5 && spawn.y >= 0 && spawn.y <= 2)
                    damageZone.Add(new damageTile(spawn , activeDamageTime));
            }
        }
    }

    public override void SBehaviour()
    {
        for (int i = 0; i < damageZone.Count; i++)
        {
            if (moves)
            {
                if (damageZone[i].remainingDuration == activeDamageTime - delaySpeed)
                {
                    if(damageZone[i].damagePos.x + Direction()<=5 && damageZone[i].damagePos.x+Direction() >= 0)
                        damageZone.Add(new damageTile(new Vector2Int(damageZone[i].damagePos.x + Direction(), damageZone[i].damagePos.y), activeDamageTime));
                }
                    
            }
            damageZone[i].remainingDuration--;
        }
        damageZone.RemoveAll(tile => tile.remainingDuration <= 0);
        if (damageZone.Count == 0)
            End();
    }
}
