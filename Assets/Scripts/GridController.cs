using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public GameObject GridItem;
    public TMPro.TMP_InputField InputField;

    private void Start()
    {
        CreateGrid(3);
    }

    public void CreateGrid(int rowCount = 0)
    {
        ClearGrid();
        
        if (rowCount <= 0) rowCount = int.Parse(InputField.text);
        MatchController.Instance.CreateMatris(rowCount);
        MatchController.Instance.Grids.Clear();
        for (int i = 0; i < rowCount * rowCount; i++)
        {
            GameObject grid =  Instantiate(GridItem, transform);
            GridElement tmp_grid = grid.GetComponent<GridElement>();
            tmp_grid.GridID = i;
            MatchController.Instance.Grids.Add(i, tmp_grid);
        }
    }
    
    public void ClearGrid()
    {
        GridElement[] childs = GetComponentsInChildren<GridElement>();
        foreach (var item in childs)
        {
            Destroy(item.gameObject);
        }
    }
}
