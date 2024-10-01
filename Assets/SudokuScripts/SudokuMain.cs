using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SudokuMain : MonoBehaviour
{
    [SerializeField] private GameObject staticCellPrefab;
    [SerializeField] private GameObject inputCellPrefab;
    [SerializeField] private Transform gridParent;
    [SerializeField] private TMP_Text difficultyLabel;
    [SerializeField] private TMP_Text timeLabel;
    [SerializeField] private TMP_Text coinsEarnedLabel;

    private GameObject[] allCellsArr = new GameObject[81];
    private int[,] gridArray;
    private string sudokusPath;
    private int size=0;
    [HideInInspector] public string solvedStr;
    private int[,] solvedGrid;
    private string randSudokuDiff;

    public GameObject[] GetAllCellsArr()
    {
        return allCellsArr;
    }
    public string GetSolvedStr()
    {
        Debug.Log(solvedStr);
        return solvedStr;
    }
    void Start()
    {
        sudokusPath = Application.dataPath + "/sudokus.txt";
        ReadInPuzzle();
        GenerateGridUI();
        difficultyLabel.text = "Difficulty: " +randSudokuDiff;
        GetSolution();
        Debug.Log(solvedStr);
    }

    void Update()
    {
        timeLabel.text = string.Format("{0:00}:{1:00}", Math.Round(Time.time, 1)/60, Math.Round(Time.time, 1),3600);
        string userAnswer = GetUserAnswer();
        if (userAnswer.Equals(solvedStr))
        {
            GameOver();
        }
    }

    private string GetUserAnswer()
    {
        string returnString = "";
        int i = 0;
        foreach (GameObject gm in allCellsArr)
        {

            if (gm.name.Equals("InputCellPrefab(Clone)"))
            {

                returnString += allCellsArr[i].GetComponent<TMP_InputField>().text;

            }
            else

            {
                returnString += allCellsArr[i].GetComponentInChildren<TMP_Text>().text;

            }
            i++;
        }
        return returnString;
        }

    private void GameOver()
    {
        int coinsEarned=0;
        foreach (GameObject gm in allCellsArr)
        {
            if (gm.name.Equals("InputCellPrefab(Clone"))
            {
                gm.GetComponent<TMP_InputField>().interactable = false;
            }
            if (difficultyLabel.Equals("Evil")) coinsEarned = 4000;
            else if (difficultyLabel.Equals("Hard")) coinsEarned = 2500;
            else if (difficultyLabel.Equals("Medium")) coinsEarned = 1000;
            else coinsEarned = 750;
            coinsEarnedLabel.text = coinsEarned.ToString();
            UserManager.Instance.Coins += coinsEarned;
        }
    }
    public void NewGame()
    {
        SceneManagerScript sms = new SceneManagerScript();
        sms.LoadScene("Sudoku");
    }
        //reads in puzzle to the gridArray int[,]
        private void ReadInPuzzle()
    {
        int size = 0;
        if (!File.Exists(sudokusPath))
        {
            Debug.Log("File not found. Cannot read in random sudoku.");
        }
        else
        {
            string[] sudokuArr = new string[10];
            string currentLine = "";
            using (StreamReader fileReader = new StreamReader(sudokusPath))
            {
                while ((currentLine = fileReader.ReadLine()) != null)
                {
                    sudokuArr[size] = currentLine;
                    size++;
                }
            }
            int randInt = UnityEngine.Random.Range(0, sudokuArr.Length);
            randSudokuDiff = sudokuArr[randInt].Split("#")[0];
            string randSudokuStr = sudokuArr[randInt].Split("#")[1];

            gridArray = new int[9, 9];
            int cell = 0;
            for (int row = 0; row < 9; row++)
            {
                for (int column = 0; column < 9; column++)
                {
                    gridArray[row, column] = int.Parse(randSudokuStr[cell].ToString());
                    cell++;
                }
            }
            

        }
    }

    //populates the ui gridArray 
    private void GenerateGridUI()
    {
        for (int row = 0; row < 9; row++)
        {
            for (int col = 0; col < 9; col++)
            {
                if (gridArray[row, col] != 0)
                {
                    GameObject staticCell = Instantiate(staticCellPrefab, gridParent);
                    allCellsArr[size] = staticCell;
                    size++;
                    staticCell.GetComponentInChildren<TMP_Text>().text = gridArray[row, col].ToString();
                }
                else
                {
                    GameObject inputCell = Instantiate(inputCellPrefab, gridParent);
                    allCellsArr[size] = inputCell;
                    size++;
                    inputCell.GetComponent<TMP_InputField>().text = "";
                }

            }
        }
    }
    public void RevealSolution()
    {
        int i= 0;
        foreach (GameObject gm in allCellsArr)
        {

            if (gm.name.Equals("InputCellPrefab(Clone)"))
            {

                allCellsArr[i].GetComponent<TMP_InputField>().text = solvedStr[i].ToString();
                
            }
            else
            //it is just a static cell
            {
                allCellsArr[i].GetComponentInChildren<TMP_Text>().text = solvedStr[i].ToString();
                
            }
            i++;
        }
        GameOver();
        
    }
    private string ConvertGridToString(int[,] gridToConvert)
    {
        string gridString = "";
        for (int row = 0; row < 9; row++)
        {
            for (int column = 0; column < 9; column++)
            {
                gridString += gridToConvert[row, column];
            }
        }
        return gridString;
    }

    //keep as bool to terminate while when no solution 
    private bool GetSolution()
    {
        int[,] freeCellList = GetFreeCellList(gridArray);
        int k = 0;
        bool found = false;

        while (!found)
        {
            int freeCellRow = freeCellList[k, 0];
            int freeCellColumn = freeCellList[k, 1];

            if (gridArray[freeCellRow, freeCellColumn] == 0)
            {
                gridArray[freeCellRow, freeCellColumn] = 1;
            }

            if (isValid(freeCellRow, freeCellColumn, gridArray))
            {
                //all free cells used up
                if (k + 1 == freeCellList.GetLength(0))
                {
                    found = true;
                    //cast as int [,] not object
                    solvedGrid = (int[,])gridArray.Clone();
                    solvedStr = ConvertGridToString(solvedGrid);
                }
                else
                {
                    //fill next cell
                    k++;
                }
            }
            else if (gridArray[freeCellRow, freeCellColumn] < 9)
            {
                gridArray[freeCellRow, freeCellColumn]++;
            }
            else
            {
                //keep backtracking to each prev cell
                while (gridArray[freeCellRow, freeCellColumn] == 9)
                {
                    gridArray[freeCellRow, freeCellColumn] = 0;
                    if (k == 0)
                    {
                        return false;
                    }
                    k--; //go to previous free cell + change it 
                    freeCellRow = freeCellList[k, 0];
                    freeCellColumn = freeCellList[k, 1];
                }
                //once no longer nine: check new values for freed cells 
                gridArray[freeCellRow, freeCellColumn] += 1;
            }
        }
        return true;
    }

    //overloaded isValid (check wholeGrid)
    private bool isValid(int[,] grid)
    {
        for (int row = 0; row < 9; row++)
        {
            for (int column = 0; column < 9; column++)
            {
                if (grid[row, column] != 0 && !isValid(row, column, grid))
                {
                    return false;
                }
            }
        }
        return true;
    }

    private int[,] GetFreeCellList(int[,] grid)
    {
        int numFreeCells = 0;
        for (int row = 0; row < 9; row++)
        {
            for (int column = 0; column < 9; column++)
            {
                if (grid[row, column] == 0)
                {
                    numFreeCells++;
                }
            }
        }

        //2 columns 
        int[,] freeCellList = new int[numFreeCells, 2];
        int count = 0;
        for (int row = 0; row < 9; row++)
        {
            for (int column = 0; column < 9; column++)
            {
                if (grid[row, column] == 0)
                {
                    freeCellList[count, 0] = row;
                    freeCellList[count, 1] = column;
                    count++;
                }
            }
        }
        return freeCellList;
    }
    private bool isValid(int cellRow, int cellColumn, int[,] grid)
    {
        for (int cycleColumn = 0; cycleColumn < 9; cycleColumn++)
        {
            //if its in a different column in the SAME ROW - row checker 
            if (cellColumn != cycleColumn && grid[cellRow, cycleColumn] == grid[cellRow, cellColumn])
            {
                return false;
            }
        }

        for (int cycleRow = 0; cycleRow < 9; cycleRow++)
        {
            //column checker 
            if (cellRow != cycleRow && grid[cycleRow, cellColumn] == grid[cellRow, cellColumn])
            {
                return false;
            }
        }

        //horizontal cycle withinn block         ^  start row
        for (int startRow = (cellRow / 3) * 3; startRow < (cellRow / 3) * 3 + 3; startRow++)
        {
            //vertical cycle within each block :       ^ start column   
            for (int startColumn = (cellColumn / 3) * 3; startColumn < (cellColumn / 3) * 3 + 3; startColumn++)
            {
                //if two different cells within a block have the same number 
                if (startRow != cellRow && startColumn != cellColumn && grid[startRow, startColumn] == grid[cellRow, cellColumn])
                {
                    return false;
                }
            }
        }
        return true;
    }

}
