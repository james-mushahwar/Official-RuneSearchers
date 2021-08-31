using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public void MoveUnit(GameObject moveUnit, GameObject destinationSquare)
    {
        UnitState moveUnitState = moveUnit.GetComponent<InplayUnit>().UnitState;
        UnitStats moveUnitStats = moveUnit.GetComponent<InplayUnit>().CurrentUnitStats;

        moveUnit.GetComponent<Movement>().Move(destinationSquare);
        if (moveUnitStats.UnitAbility is BlueClassAbility && moveUnitStats.UnitAbility.isActive)
        {
            moveUnitStats.UnitAbility.Deactivate(moveUnit.GetComponent<InplayUnit>());
        }
        else
        {
            moveUnitState.SetHasMoved(true);
        }
    }
}
