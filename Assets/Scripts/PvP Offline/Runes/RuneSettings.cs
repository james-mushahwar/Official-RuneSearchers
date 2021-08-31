using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RuneSettings : ScriptableObject
{
    [Header("General Rune settings")]
    [SerializeField]
    private int maxRunesInPlay;

    [Header("Rune Stats")]
    [SerializeField]
    [Tooltip("X = HP, Y = Drops")]
    Vector2Int smallRuneStats;
    [SerializeField]
    [Tooltip("X = HP, Y = Drops")]
    Vector2Int mediumRuneStats;
    [SerializeField]
    [Tooltip("X = HP, Y = Drops")]
    Vector2Int largeRuneStats;

    [Header("Rune spawn appearance settings")]
    [SerializeField]
    private Color redRuneColour;
    [SerializeField]
    private Color yellowRuneColour;
    [SerializeField]
    private Color blueRuneColour;

    [Header("Rune Hover speed")]
    [SerializeField]
    private float runeHoverSpeed;

    public Vector2Int SmallRuneStats => smallRuneStats;
    public Vector2Int MediumRuneStats => mediumRuneStats;
    public Vector2Int LargeRuneStats => largeRuneStats;
    public int MaxRunesInPlay => maxRunesInPlay;
    public Color RedRuneColour => redRuneColour;
    public Color YellowRuneColour => yellowRuneColour;
    public Color BlueRuneColour => blueRuneColour;
}
