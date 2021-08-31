using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BoardSettings : ScriptableObject
{
    [Header("Board rows and columns")]
    [SerializeField]
    private int boardRows;
    [SerializeField]
    private int boardColumns;

    [Header("Board Appearance")]
    [SerializeField]
    private float boardXRotation;
    [SerializeField]
    private Color boardSquareColour;
    [SerializeField]
    private float boardSelectionFrameScalingSpeed;

    [Header("Camera Position")]
    [SerializeField]
    private Vector3 cameraPosition;

    public int BoardRows => boardRows;
    public int BoardColumns => boardColumns;
    public float BoardXRotation => boardXRotation;
    public Color BoardSquareColour => boardSquareColour;
    public float BoardSelectionFrameScalingSpeed => boardSelectionFrameScalingSpeed;
    public Vector3 CameraPosition => cameraPosition;
}
