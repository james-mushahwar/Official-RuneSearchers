using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Rune Appearance", menuName = "Rune/Rune Appearance")]
public class RuneAppearance : ScriptableObject
{
    [SerializeField]
    private Sprite runeInPlaySprite;
    [SerializeField]
    private RuneSize runeSize;

    public Sprite RuneInPlaySprite => runeInPlaySprite;
    public RuneSize RuneSize => runeSize;
}
