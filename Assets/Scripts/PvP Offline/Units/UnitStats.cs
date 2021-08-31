using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(fileName = "New Unit Stats", menuName = "Unit/UnitStats")]
public class UnitStats : ScriptableObject
{
    [Header("Unit Details")]
    [SerializeField]
    private string unitName;
    [SerializeField]
    private UnitType unitType;
    [SerializeField]
    private RuneClass runeClass;

    [Header("Unit battle stats")]
    [SerializeField]
    private int summonCost;
    [SerializeField]
    private int attackPoints;
    [SerializeField]
    private int healthPoints;
    [SerializeField]
    private int movementRange;
    [SerializeField]
    private int classStatBuff;

    [Header("Unit Class ability")]
    [SerializeField]
    private ClassAbility unitAbility;

    public UnitType UnitType => unitType;
    public RuneClass RuneClass => runeClass;

    public string UnitName => unitName;
    public int SummonCost => summonCost;
    public int HealthPoints => healthPoints;
    public int MovementRange
    {
        get => movementRange;
        set => movementRange += value;
    }
    public int AttackPoints
    {
        get => attackPoints;
        set => attackPoints += value;
    }
    public ClassAbility UnitAbility { get => unitAbility; set => unitAbility = value; }
    public int ClassStatBuff { get => classStatBuff; set => classStatBuff = value; }

    public void CopyStats(UnitStats copyStats)
    {
        unitName = copyStats.unitName;
        unitType = copyStats.unitType;
        runeClass = copyStats.runeClass;
        summonCost = copyStats.summonCost;
        attackPoints = copyStats.attackPoints;
        healthPoints = copyStats.healthPoints;
        movementRange = copyStats.movementRange;
        classStatBuff = copyStats.classStatBuff;
    }

    public ClassAbility GenerateUnitClassAbility()
    {
        Debug.Log("Generating new Unit CLass Ability");
        if (runeClass == RuneClass.Red)
        {
            return new RedClassAbility(classStatBuff);
        }
        else if (runeClass == RuneClass.Yellow)
        {
            return new YellowClassAbility(classStatBuff);
        }
        else
        {
            return new BlueClassAbility(classStatBuff);
        }
    }

    public void TakeDamage(int attack, InplayUnit unit, bool isARetaliate)
    {
        int finalDamage;
        if (unitAbility is YellowClassAbility)
        {
            Debug.Log("Ability type is yellow");
            finalDamage = unitAbility.TakeDefenceDamage(attack);
            healthPoints -= finalDamage;
            if (finalDamage == 0 && unitAbility.isActive)
                unitAbility.Deactivate();
        }
        else
        {
            healthPoints -= attack;
        }
        if (healthPoints > 0 && unitAbility.AbilityCanBeActivated() && !isARetaliate)
        {
            unitAbility.Activate(unit);
        }
    }
}
