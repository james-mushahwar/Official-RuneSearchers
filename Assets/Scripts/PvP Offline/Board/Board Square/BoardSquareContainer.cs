using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSquareContainer : MonoBehaviour
{
    [Header("Board Square position data")]
    [SerializeField]
    private int rowIndex;
    [SerializeField]
    private int columnIndex;

    public int RowIndex
    {
        get { return rowIndex; }
        set { rowIndex = value; }
    }
    public int ColumnIndex
    {
        get { return columnIndex; }
        set { columnIndex = value; }
    }

    [Header("Board Square unit spawns")]
    [SerializeField]
    private GameObject unit = null;
    [SerializeField]
    private GameObject environmentUnit = null;
    [SerializeField]
    private GameObject boardRune;

    public GameObject Unit
    {
        get => unit;
        set => unit = value;
    }

    public GameObject EnvironmentUnit
    {
        get => environmentUnit;
        set => environmentUnit = value;
    }

    public GameObject BoardRune
    {
        get => boardRune;
        set => boardRune = value;
    }
}
