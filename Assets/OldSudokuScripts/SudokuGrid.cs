using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SudokuGrid : MonoBehaviour
{
    [SerializeField] private int sudokuColumns;
    [SerializeField] private int sudokuRows;
    
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private float squareOffset;
    [SerializeField] private Vector2 startPosition;
    [SerializeField] private float squareScale;

    //makes a list of all the prefabs (each cell) 
    private List<GameObject> cellList = new List<GameObject>();

    void Start()
    {
        CreateGrid();
        SetGridNumber();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateGrid()
    {
        //instantiate the cells 
        SpawnGridSquares();
        //adjust the positions of each cell
        SetSquaresPosition();
    }

    private void SpawnGridSquares()
    {
        //adds 9 columns for 1 row, adds 9 rows
        for (int row = 0; row < sudokuRows; row++)
        {
            for (int column = 0; column < sudokuColumns; column++)
            {
                cellList.Add(Instantiate(cellPrefab) as GameObject);
                
                // count - 1 bc most recently spawned cell 
                cellList[cellList.Count-1].transform.parent = this.transform; //spawns ON the generatedGrid on the canvas 
                
                cellList[cellList.Count-1].transform.localScale = new Vector3 (squareScale, squareScale, squareScale); //scales the squares (sizing)  
            }
        }
    }

    private void SetSquaresPosition()
    {
        // use the first element's rect settings as a reference 
        var squareRect = cellList[0].GetComponent<RectTransform>();
        Vector2 offsetBetweenSquares = new Vector2();
        //size of the cell horizontally/ vertically + the offset 
        offsetBetweenSquares.x = squareRect.rect.width * squareRect.transform.localScale.x + squareOffset;
        offsetBetweenSquares.y = squareRect.rect.height * squareRect.transform.localScale.y + squareOffset;

        int columnNumber = 0;
        int rowNumber = 0;

        foreach (GameObject cell in cellList) { 
            //if columnNumber + 1 = (10) > 9 then the row is full
            //starts from 0 so positions 9 columns  
            if(columnNumber+1> sudokuColumns)
            {
                //next row 
                rowNumber++;
                columnNumber = 0;
            }
            var posXOffset = offsetBetweenSquares.x * columnNumber;
            var posYOffset = offsetBetweenSquares.y * rowNumber;
            cell.GetComponent<RectTransform>().anchoredPosition = new Vector2 (startPosition.x + posXOffset, startPosition.y - posYOffset);
            //next column 
            columnNumber++;
        }
    }

    private void SetGridNumber()
    {
        foreach (var cell in cellList)
        {
            //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            cell.GetComponent<CellScript>().SetNumber(Random.Range(0, 10));
        }
    }
}
