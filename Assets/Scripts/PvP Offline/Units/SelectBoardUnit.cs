using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBoardUnit : MonoBehaviour
{
    [SerializeField]
    private SelectHandler selectHandler;

    private void OnMouseDown()
    {
        Debug.Log("Unit selected");
        selectHandler.BoardSquareClicked(null, gameObject);
    }
}
