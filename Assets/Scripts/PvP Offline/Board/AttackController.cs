using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField]
    private GameObject graveyard;

    [Header ("Controllers")]
    [SerializeField]
    private ActionController actionController;

    public void AttackUnit(GameObject currentUnit, GameObject unitAttacked, GameObject boardSquare)
    {
        UnitStats currentUnitStats = currentUnit.GetComponent<InplayUnit>().CurrentUnitStats;
        UnitStats attackedUnitStats = unitAttacked.GetComponent<InplayUnit>().CurrentUnitStats;
        InplayUnit attackedInPlayUnit = unitAttacked.GetComponent<InplayUnit>();
        bool enragedAttack = IsEnragedAttack(currentUnit.GetComponent<InplayUnit>());

        int attackPoints = currentUnitStats.AttackPoints;
        attackedUnitStats.TakeDamage(attackPoints, attackedInPlayUnit, enragedAttack);
        if (enragedAttack)
        {
            currentUnit.GetComponent<InplayUnit>().CurrentUnitStats.UnitAbility.Deactivate(currentUnit.GetComponent<InplayUnit>());
        }
        currentUnit.GetComponent<InplayUnit>().UnitState.SetHasAttacked(true);

        if (attackedUnitStats.HealthPoints <= 0)
        {
            unitAttacked.GetComponent<InplayUnit>().UnitState.SetIsAlive(false);
            unitAttacked.transform.SetParent(graveyard.transform, false);
            actionController.RemoveUnitFromBoard(boardSquare);
            unitAttacked.SetActive(false);
        }
    }

    public void AttackRune(GameObject currentUnit, GameObject runeAttacked, GameObject boardSquare)
    {
        UnitStats currentUnitStats = currentUnit.GetComponent<InplayUnit>().CurrentUnitStats;
        HeldRunes currentUnitRunes = currentUnit.GetComponent<InplayUnit>().HeldRunes;
        RuneStats runeAttackedStats = runeAttacked.GetComponent<InplayRune>().RuneStats;

        runeAttackedStats.TakeDamage();

        if (runeAttackedStats.HitPoints <= 0)
        {
            PickupRunes(currentUnitRunes, currentUnitStats, runeAttackedStats, runeAttacked, currentUnit, boardSquare);
        }
    }

    private void PickupRunes(HeldRunes currentHeldRunes, UnitStats currentUnitStats, RuneStats runeStats, GameObject destroyedRune, GameObject currentUnit, GameObject boardSquare)
    {
        if (currentUnitStats.UnitType == UnitType.Minion)
        {
            if (currentHeldRunes.HasNoRunes())
            {
                currentHeldRunes.RuneCount = runeStats.RuneDrops;
                currentHeldRunes.RuneClass = destroyedRune.GetComponent<InplayRune>().RuneType.RuneClass;
            }
            else
            {
                DistributeRunesToSurroundingAllies(runeStats, destroyedRune);
            }
        }
        else if (currentUnitStats.UnitType == UnitType.Searcher)
        {
            UnitState currentUnitState = currentUnit.GetComponent<InplayUnit>().UnitState;
            Player currentPlayer = actionController.PlayerManager.GetPlayerOccupant(currentUnitState.UnitOccupant);
            RuneClass destroyedRuneClass = destroyedRune.GetComponent<InplayRune>().RuneType.RuneClass;

            currentPlayer.PlayerStats.SearcherPickedUpRunes(currentUnitStats.RuneClass, destroyedRuneClass, runeStats.RuneDrops);
            currentPlayer.SpawnHandler.UpdateSpawnTrayUnits();
        }

        actionController.RemoveRuneFromBoard(boardSquare);
        Destroy(destroyedRune);
    }

    private void DistributeRunesToSurroundingAllies(RuneStats runeStats, GameObject destroyedRune)
    {
        
    }

    private bool IsEnragedAttack(InplayUnit attacker)
    {
        if (attacker.CurrentUnitStats.UnitAbility is RedClassAbility && attacker.CurrentUnitStats.UnitAbility.isActive)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
