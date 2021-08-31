using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighlightTrayUnit : MonoBehaviour
{
    private Image TrayImage { get; set; }
    private Color currentColour { get; set; }
    [SerializeField]
    private TrayUnitData trayUnitData;

    private void Start()
    {
        TrayImage = gameObject.GetComponent<Image>();
        currentColour = gameObject.GetComponent<Image>().color;
        UpdateInteractable();
    }

    public void Highlight()
    {
        TrayImage.color += new Color(0.1f, 0.1f, 0.1f);
    }

    public void RemoveHighlight()
    {
        TrayImage.color -= new Color(0.1f, 0.1f, 0.1f);
    }

    public void UpdateInteractable()
    {
        if (trayUnitData.IsPlayable)
        {
            gameObject.GetComponent<Image>().color = currentColour;
        }
        else
        {
            gameObject.GetComponent<Image>().color = currentColour * 0.5f;
        }
    }
}
