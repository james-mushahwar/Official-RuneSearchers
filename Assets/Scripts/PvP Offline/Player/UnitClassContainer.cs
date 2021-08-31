using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class UnitClassContainer
{
    [SerializeField]
    private InplayUnit[] units;

    public InplayUnit[] Units => units;

    public UnitClassContainer()
    {
        units = new InplayUnit[3];
    }

    public void SetUnitToArray(int index, InplayUnit unit)
    {
        units[index] = unit;
    }
    public InplayUnit GetLevelOneUnit()
    {
        return units[0];
    }
    public InplayUnit GetLevelTwoUnit()
    {
        return units[1];
    }
    public InplayUnit GetLevelThreeUnit()
    {
        return units[2];
    }
    public GameObject GetUnitByIndex(int index)
    {
        return units[index].gameObject;
    }
    public InplayUnit GetInplayUnit(int index)
    {
        return units[index];
    }

    public void ResetUnitAbility()
    {
        foreach (InplayUnit unit in units)
        {
            if (unit.gameObject.activeSelf)
                unit.CurrentUnitStats.UnitAbility.Reset(unit);
        }
    }
}
