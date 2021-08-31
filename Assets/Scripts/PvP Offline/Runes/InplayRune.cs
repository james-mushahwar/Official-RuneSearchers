using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InplayRune : MonoBehaviour
{
    [Header("Rune settings")]
    [SerializeField]
    private RuneType runeType;

    [Header("Inplay rune stats")]
    [SerializeField]
    private RuneStats runeStats;

    public RuneType RuneType => runeType;
    public RuneStats RuneStats => runeStats;

    public void SetUpRuneType(RuneType newRuneType)
    {
        runeType = newRuneType;
    }

    public void SetupRuneStats(int hp, int drops)
    {
        runeStats = new RuneStats(hp, drops);
    }
}
