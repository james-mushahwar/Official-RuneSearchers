using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unit Appearance", menuName = "Unit/UnitAppearance")]
public class UnitAppearance : ScriptableObject
{
    [Header("Appearance")]
    [SerializeField]
    private Sprite unitSprite;

    [Header("Movement stats")]
    [SerializeField]
    private float movementSpeed;

    public Sprite UnitSprite => unitSprite;
    public float MovementSpeed => movementSpeed;
}
