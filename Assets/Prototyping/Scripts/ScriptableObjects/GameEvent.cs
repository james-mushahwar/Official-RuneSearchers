using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEvent : ScriptableObject
{
    List<GameEventListener> GameEventListeners = new List<GameEventListener>();

    public void RegisterListener(GameEventListener listener)
    {
        GameEventListeners.Add(listener);
    }

    public void UnregisterListener(GameEventListener listener)
    {
        GameEventListeners.Remove(listener);
    }

    public void Raise()
    {
        for (int i = GameEventListeners.Count - 1; i >= 0; i--)
        {
            GameEventListeners[i].OnEventRaised();
        }
    }
}
