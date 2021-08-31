using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player GUI", menuName = "Player/Player GUI")]
public class PlayerGUI : ScriptableObject
{
    [SerializeField]
    private GameObject spawnTray;

    public GameObject SpawnTray { get => spawnTray; set => spawnTray = value; }

    public void HidePlayerGUI()
    {
        spawnTray.SetActive(false);
    }

    public void ShowPlayerGUI()
    {
        spawnTray.SetActive(true);
    }
}
