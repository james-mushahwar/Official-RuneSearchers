using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeAbility
{
    public bool active { get; set; }
    public bool activated { get; set; }

    public bool CanActivate() 
    {
        return active == false && activated == false;
    }
    public void Activate()
    {
        active = activated = true;
    }
    public void Deactivate()
    {
        active = activated = false;
    }
    public void Expend()
    {
        active = false;
    }
}
