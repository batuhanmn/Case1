using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class GridElement : MonoBehaviour, IPointerClickHandler
{
    
    public int GridID;
    private bool isSelected;
    private TextMeshProUGUI gridText;
    public TextMeshProUGUI GridText
    {
        get { return gridText == null ? gridText = GetComponentInChildren<TMPro.TextMeshProUGUI>() : gridText; }
        set { gridText = value; }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (isSelected) return;
        isSelected = true;
        ChangeText("X");
        MatchController.Instance.Mark2Matris(GridID);
    }

    private void ChangeText(string text)
    {
        GridText.text = text;
    }

    public void MatchedGrid()
    {
        MatchController.Instance.Mark2Matris(GridID, 0, false);
        GridText.text = "";
        isSelected = false;
    }

}
