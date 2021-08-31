using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SelectHandler : ScriptableObject
{
    [SerializeField]
    private GameObject currentUnitSelected;
    [SerializeField]
    private GameObject currentBoardSquare;
    [SerializeField]
    private GameObject newUnitSelected;
    [SerializeField]
    private GameObject newBoardSquare;

    [Header("Events for type of selection")]
    [SerializeField]
    private GameEvent SelectBoardSquareEvent;

    public GameObject CurrentUnitSelected
    {
        get { return currentUnitSelected; }
        set { currentUnitSelected = value; }
    }

    public GameObject CurrentBoardSquare
    {
        get { return currentBoardSquare; }
    }

    public GameObject NewUnitSelected
    {
        get { return newUnitSelected; }
        set { newUnitSelected = value; }
    }

    public GameObject NewBoardSquare
    {
        get { return newBoardSquare; }
        set { newBoardSquare = value; }
    }

    private void OnEnable()
    {
        ResetSO();
    }

    private void ResetSO()
    {
        currentUnitSelected = null;
        currentBoardSquare = null;
        newUnitSelected = null;
        newBoardSquare = null;
    }

    public void BoardSquareClicked(GameObject newBoardSquare, GameObject newUnit)
    {
        NewUnitSelected = newUnit;
        NewBoardSquare = newBoardSquare;
        SelectBoardSquareEvent.Raise();
    }

    public void SelectBoardSquare(GameObject newBoardSquare, GameObject newUnit)
    {
        Debug.Log("Select board square");
        if (currentBoardSquare != null)
        {
            currentBoardSquare.GetComponent<Highlight>().RemoveHighlighting();
            currentBoardSquare.GetComponent<Select>().DeselectBoardSquare();
        }

        currentBoardSquare = newBoardSquare;
        currentUnitSelected = newUnit;
        NewUnitSelected = null;
        NewBoardSquare = null;
        currentBoardSquare.GetComponent<Select>().SelectedBoardSquare();
    }

    public void UnselectCurrentBoardSquare()
    {
        if (currentBoardSquare != null)
        {
            currentBoardSquare.GetComponent<Select>().DeselectBoardSquare();
            currentBoardSquare.GetComponent<Highlight>().RemoveHighlighting();
        }
        currentBoardSquare = null;
        currentUnitSelected = null;
    }

    public void ExternalUnselect()
    {
        if (currentBoardSquare != null)
        {
            currentBoardSquare.GetComponent<Highlight>().RemoveHighlighting();
            UnselectCurrentBoardSquare();
        }    
    }

    public void ClearAllSelections()
    {
        if (currentBoardSquare != null)
        {
            currentBoardSquare.GetComponent<Select>().DeselectBoardSquare();
            currentBoardSquare.GetComponent<Highlight>().RemoveHighlighting();
        }
        ResetSO();
    }
}
