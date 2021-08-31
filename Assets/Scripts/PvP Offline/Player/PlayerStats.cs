using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Player Stats", menuName = "Player/Player Stats")]
public class PlayerStats : ScriptableObject
{
    [Header("Runes Collected")]
    [SerializeField]
    private RuneClassContainer redRunes;
    [SerializeField]
    private RuneClassContainer yellowRunes;
    [SerializeField]
    private RuneClassContainer blueRunes;

    [Header("In play stats")]
    [SerializeField]
    private int energyCollected;

    [Header("Player Config")]
    [SerializeField]
    private Occupant playerOccupant;

    public Occupant PlayerOccupant => playerOccupant;

    public RuneClassContainer RedRunes { get => redRunes; set => redRunes = value; }
    public RuneClassContainer YellowRunes { get => yellowRunes; set => yellowRunes = value; }
    public RuneClassContainer BlueRunes { get => blueRunes; set => blueRunes = value; }

    private void OnEnable()
    {
        redRunes = new RuneClassContainer();
        yellowRunes = new RuneClassContainer();
        blueRunes = new RuneClassContainer();
        energyCollected = 0;
    }

    public void SearcherPickedUpRunes(RuneClass searcherClass, RuneClass runeClass, int runeDrops)
    {
        if (searcherClass == runeClass)
        {
            Debug.Log("Same class, enable minion spawn"); 
            GeneratePlayerRunes(false, runeClass, runeDrops);
        }
        else
        {
            Debug.Log("Different class, runes are exhausted");
            GeneratePlayerRunes(true, runeClass, runeDrops);
        }
    }

    private void GeneratePlayerRunes(bool exhausted, RuneClass runeClass, int drops)
    {
        RuneClassContainer runeContainer = runeClass == RuneClass.Red ? redRunes : runeClass == RuneClass.Yellow ? yellowRunes : blueRunes;

        for (int i = 0; i < drops; i++)
        {
            runeContainer.AddRune(new SearcherRune(exhausted, runeClass));
        }
    }

    public void ExhaustRuneClassRunes(RuneClass runeClass, int cost)
    {
        if (runeClass == RuneClass.Red)
        {
            redRunes.ExhaustRunes(cost);
        }
        else if (runeClass == RuneClass.Yellow)
        {
            yellowRunes.ExhaustRunes(cost);
        }
        else
        {
            blueRunes.ExhaustRunes(cost);
        }
    }
}
