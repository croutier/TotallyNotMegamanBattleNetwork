using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ISpecial {
    protected string name;
    public string Name { get { return name; } }
    public abstract void SBehaviour();
    protected Vector2Int castPos;
    protected playerID caster;
    public playerID Caster { get { return caster; } }
    protected bool active;
    public bool Active { get { return active; } }


    public virtual void Call(Vector2Int pos, playerID spCaster)
    {
        castPos = pos;
        caster = spCaster;
        Clock.Instance.OnTick += SBehaviour;
        active = true;
    }
    protected void End()
    {
        Clock.Instance.OnTick -= SBehaviour;
        active = false;
    }
}
