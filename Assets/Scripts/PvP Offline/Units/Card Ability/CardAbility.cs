using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class CardAbility
{
    public virtual void Setup() { }
    public virtual void Activate() { }
    public virtual void Deactivate() { }
}

[Serializable]
public class AttackCardAbility : CardAbility
{
    public override void Setup()
    {

    }
}
