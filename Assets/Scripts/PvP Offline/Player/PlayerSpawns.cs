using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Player Spawns", menuName = "Player/Player Spawns")]
public class PlayerSpawns : ScriptableObject
{
    [Header("Containers for player units")]
    [SerializeField]
    private UnitClassContainer searcherUnitObjects;
    [SerializeField]
    private UnitClassContainer redUnitObjects;
    [SerializeField]
    private UnitClassContainer yellowUnitObjects;
    [SerializeField]
    private UnitClassContainer blueUnitObjects;

    public UnitClassContainer SearcherUnitObjects => searcherUnitObjects;
    public UnitClassContainer RedUnitObjects => redUnitObjects;
    public UnitClassContainer YellowUnitObjects => yellowUnitObjects;
    public UnitClassContainer BlueUnitObjects => blueUnitObjects;

    private void OnEnable()
    {
        searcherUnitObjects = new UnitClassContainer();
        redUnitObjects = new UnitClassContainer();
        yellowUnitObjects = new UnitClassContainer();
        blueUnitObjects = new UnitClassContainer();
    }

    public GameObject GetSearcher(RuneClass runeclass)
    {
        if (runeclass == RuneClass.Red)
        {
            return searcherUnitObjects.Units[0].gameObject;
        }
        else if (runeclass == RuneClass.Yellow)
        {
            return searcherUnitObjects.Units[1].gameObject;
        }
        else
        {
            return searcherUnitObjects.Units[2].gameObject;
        }
    }

    public InplayUnit GetUnitToSummon(RuneClass runeClass, int summonCost)
    {
        if (runeClass == RuneClass.Red)
        {
            return redUnitObjects.GetInplayUnit(summonCost - 1);
        }
        else if (runeClass == RuneClass.Yellow)
        {
            return yellowUnitObjects.GetInplayUnit(summonCost - 1);
        }
        else
        {
            return blueUnitObjects.GetInplayUnit(summonCost - 1);
        }
    }

    public void ResetClassAbilities()
    {
        searcherUnitObjects.ResetUnitAbility();
        redUnitObjects.ResetUnitAbility();
        yellowUnitObjects.ResetUnitAbility();
        blueUnitObjects.ResetUnitAbility();
    }
}
