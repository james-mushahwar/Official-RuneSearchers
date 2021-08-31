using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ClassAbility
{
    [SerializeField]
    internal bool isActive = false;
    [SerializeField]
    internal bool previouslyActivated = false;

    public virtual void Activate(InplayUnit unit)
    {
        Debug.Log("Class ability activated!");
        isActive = true;
    }
    public virtual void Deactivate(InplayUnit unit)
    {
        isActive = false;
        previouslyActivated = true;
    }
    public virtual void Deactivate()
    {
        isActive = false;
        previouslyActivated = true;
    }
    public virtual void Reset()
    {
        isActive = false;
        previouslyActivated = false;
    }
    public virtual void Reset(InplayUnit unit)
    {
        isActive = false;
        previouslyActivated = false;
    }
    public bool AbilityCanBeActivated()
    {
        return !isActive && !previouslyActivated;
    }
    public virtual int TakeDefenceDamage(int damage)
    {
        return damage;
    }
}

[Serializable]
public class RedClassAbility : ClassAbility
{
    [SerializeField]
    private int attackBuff;

    public RedClassAbility(int buff)
    {
        attackBuff = buff;
    }

    public override void Activate(InplayUnit unit)
    {
        base.Activate(unit);
        unit.CurrentUnitStats.AttackPoints = attackBuff;
    }
    public override void Deactivate(InplayUnit unit)
    {
        base.Deactivate(unit);
        unit.CurrentUnitStats.AttackPoints = -attackBuff;
    }
    public override void Reset(InplayUnit unit)
    {
        if (isActive)
        {
            unit.CurrentUnitStats.AttackPoints = -attackBuff;
        }
        base.Reset();
    }
}

[Serializable]
public class YellowClassAbility : ClassAbility
{
    [SerializeField]
    private int defenceBuff;
    [SerializeField]
    private int currentDefence;

    public YellowClassAbility(int buff)
    {
        defenceBuff = buff;
        currentDefence = buff;
    }

    public int CurrentDefence { get => currentDefence; set => currentDefence = value; }

    public override void Activate(InplayUnit unit)
    {
        base.Activate(unit);
        currentDefence = defenceBuff;
    }
    public override void Deactivate()
    {
        base.Deactivate();
    }
    public override int TakeDefenceDamage(int damage)
    {
        if (isActive && currentDefence > 0)
        {
            currentDefence -= damage;
            return 0;
        }
        else
        {
            return damage;
        }
    }
    public override void Reset(InplayUnit unit)
    {
        base.Reset();
    }
}

[Serializable]
public class BlueClassAbility : ClassAbility
{
    [SerializeField]
    private int movementRange;

    public BlueClassAbility(int buff)
    {
        movementRange = buff;
    }

    public override void Activate(InplayUnit unit)
    {
        base.Activate(unit);
        unit.CurrentUnitStats.MovementRange = movementRange;
    }
    public override void Deactivate(InplayUnit unit)
    {
        base.Deactivate(unit);
        unit.CurrentUnitStats.MovementRange = -movementRange;
    }
    public override void Reset(InplayUnit unit)
    {
        if (isActive)
        {
            unit.CurrentUnitStats.MovementRange = -movementRange;
        }
        base.Reset();
    }
}
