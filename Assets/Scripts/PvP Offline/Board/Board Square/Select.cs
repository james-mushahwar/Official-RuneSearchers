using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select : MonoBehaviour
{
    [SerializeField]
    private SelectHandler selectHandler;

    [Header("Selection frame settings")]
    [SerializeField]
    private GameObject selectionFrame;
    [SerializeField]
    private BoardSettings boardSettings;
    private IEnumerator currentSelectionFrameCycle;


    private void OnMouseDown()
    {
        selectHandler.BoardSquareClicked(gameObject, gameObject.GetComponent<BoardSquareContainer>().Unit);
    }

    public void SelectedBoardSquare()
    {
        selectionFrame.SetActive(true);

        if (currentSelectionFrameCycle == null)
            currentSelectionFrameCycle = SelectionFrameCycle();
        StartCoroutine(currentSelectionFrameCycle);
    }

    private IEnumerator SelectionFrameCycle()
    {
        Vector3 originalFrameScale = selectionFrame.transform.localScale;
        Vector3 targetFrameScale = selectionFrame.transform.localScale * 0.5f;

        while (true)
        {
            while (selectionFrame.transform.localScale.magnitude > targetFrameScale.magnitude)
            {
                selectionFrame.transform.localScale -= Vector3.one * Time.deltaTime * boardSettings.BoardSelectionFrameScalingSpeed;
                yield return null;
            }
            while (selectionFrame.transform.localScale.magnitude < originalFrameScale.magnitude)
            {
                selectionFrame.transform.localScale += Vector3.one * Time.deltaTime * boardSettings.BoardSelectionFrameScalingSpeed;
                yield return null;
            }
        }
    }

    public void DeselectBoardSquare()
    {
        selectionFrame.SetActive(false);

        if (currentSelectionFrameCycle != null)
            StopCoroutine(currentSelectionFrameCycle);
    }
}
