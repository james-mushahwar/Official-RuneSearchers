using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    public FloatReference BotHP;
    public FloatReference BotAttack;
    public FloatReference BotMana;

    private void Start()
    {
        DebugStats();
    }

    private void DebugStats()
    {
        Debug.Log("My health is: " + BotHP.Value +
            " my attack is: " + BotAttack.Value +
            " my mana is: " + BotMana.Value);
    }

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            TakeDamage();
        }
    }

    private void TakeDamage()
    {
        BotHP.Value = 5;
        DebugStats();
    }


}
