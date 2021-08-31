using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class RuneClassContainer
{
    [SerializeField]
    public List<SearcherRune> runes = new List<SearcherRune>();

    public List<SearcherRune> Runes => runes;

    public void AddRune(SearcherRune rune)
    {
        runes.Add(rune);
    }
    public void Remove(SearcherRune rune)
    {
        runes.Remove(rune);
    }
    public void Remove()
    {
        runes.RemoveAt(runes.Count - 1);
    }
    public bool HasNoRunes()
    {
        return runes.Count == 0;
    }

    public bool HasEqualOrMoreActiveRunes(int amount)
    {
        int runeCount = 0;
        foreach (SearcherRune rune in runes)
        {
            if (!rune.IsExhausted)
            {
                runeCount++;
                if (runeCount >= amount)
                {
                    Debug.Log("We have enough active runes!!!");
                    return true;
                }
                    
            }
        }
        return false;
    }

    public void ExhaustRunes(int exhaustCount)
    {
        Debug.Log("Runes length = " + runes.Count);
        Debug.Log("exhaustCount = " + exhaustCount);
        int startingIndex = GetLastActiveRune();

        for (int i = startingIndex; i > startingIndex - exhaustCount; i--)
        {
            if (!runes[i].IsExhausted)
            {
                runes[i].Exhaust();
            }
        }
    }

    private int GetLastActiveRune()
    {
        for (int i = runes.Count - 1; i > 0; i--)
        {
            if (!runes[i].IsExhausted)
            {
                return i;
            }
        }
        return 0;
    }
}
