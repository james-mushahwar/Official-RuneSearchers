using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DestructableBehaviour
{

}

// taking damage
/* 
Red takes damage from blue, then enrage
Red takes damage from yellow, then nullify
Red takes damage from red, normal damage

Yellow takes damage from red, then fortify
Yellow takes damage from blue, then nullify
Yellow takes damage from Yellow, normal damage

Blue takes damage from yellow, then enhance
Blue takes damage from red, then nullify
Blue takes damage from blue, normal damage
 */