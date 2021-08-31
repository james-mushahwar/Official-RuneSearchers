using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    [SerializeField]
    private BoardSettings boardSettings;
    [SerializeField]
    private SelectHandler selectHandler;
    private SpriteRenderer SpriteRenderer { get; set; }

    private void Start()
    {
        SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void OnMouseEnter()
    {
        if (SpriteRenderer.color != boardSettings.BoardSquareColour)
            return;
        if (selectHandler.CurrentBoardSquare != gameObject)
            SpriteRenderer.color += new Color(0.1f, 0.1f, 0.1f);
    }

    private void OnMouseExit()
    {
        if (SpriteRenderer.color != boardSettings.BoardSquareColour + new Color(0.1f, 0.1f, 0.1f))
            return;
        if (selectHandler.CurrentBoardSquare != gameObject)
            SpriteRenderer.color -= new Color(0.1f, 0.1f, 0.1f);
    }

    public void RemoveHighlighting()
    {
        if (SpriteRenderer.color != boardSettings.BoardSquareColour)
            SpriteRenderer.color = boardSettings.BoardSquareColour;
    }

    public void ApplyHighlighting(Color newColour)
    {
        if (SpriteRenderer.color != boardSettings.BoardSquareColour)
            return;
        SpriteRenderer.color = newColour;
    }
}
