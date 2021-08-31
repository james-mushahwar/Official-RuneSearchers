using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(fileName = "New Rune Size", menuName = "Rune/Rune Size")]
public class RuneSize : ScriptableObject
{
    [SerializeField]
    private RuneSizeClass size;

    public RuneSizeClass Size => size;
}
