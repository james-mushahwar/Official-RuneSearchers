using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTrayUnit : MonoBehaviour
{
    [SerializeField]
    private SpawnHandler spawnHandler;
    [SerializeField]
    private TrayUnitData trayUnitData;

    public void ClickTrayUnit()
    {
        if (!trayUnitData.IsPlayable)
            return;
        spawnHandler.SelectUnitToSpawn(trayUnitData.Unit);
    }
}
