using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SudokuSolver2 : MonoBehaviour
{

    [SerializeField] private TMP_Text rightLabel;
    [SerializeField] private TMP_Text leftLabel;
    private int[,] generatedGrid;
    private int[,] solvedGrid;
    [SerializeField] private int numToRemove;
    private int numSolutions=0; 

    private void Start()
    {
        generatedGrid = ReadInPuzzle(Generator());
        

        if (!isValid(generatedGrid))
        {
            Debug.Log("Invalid sudoku");
        }
        else if (FindFirstSolution(generatedGrid))
        {
            Debug.Log("First solution Found");
            PrintGrid(solvedGrid, leftLabel);

            //remove

           RemoveCells();

        }
        else
        {
            //this will never run 
            Debug.Log("No solution");
        }

    }



    private void RemoveCells()
    {
        int cellsRemoved = 0;
        while(cellsRemoved< numToRemove)
        {
            int randX = UnityEngine.Random.Range(0, 9);
            int randY = UnityEngine.Random.Range(0, 9);
            int previousValue = generatedGrid[randX, randY];
            if(generatedGrid[randX, randY]!= 0)
            {
                generatedGrid[randX, randY] = 0;
                cellsRemoved++;
            }
            
            /*if(FindOneSolution(generatedGrid))
            {
                cellsRemoved++;
                Debug.Log("Cells removed: " + cellsRemoved);
            }
            else
            {
                //return it to previoud + let while loop choose a new cell
                generatedGrid[randX, randY] = previousValue;
                
            }   */
        }
        Debug.Log("While loop over");
        PrintGrid(generatedGrid, rightLabel);
    }

    private String Generator()
    {
        string generatedStr = "";
        int[] replaceCells = new int[9] { 0, 12, 24, 28, 40, 52, 56, 68, 80 };
        int[] generatedInts = new int[81];
        for (int i = 0; i < 81; i++)
        {
            generatedInts[i] = 0;
        }
        //replace at index with the same random number
        int randomInt = UnityEngine.Random.Range(0, 10);

        for (int i = 0; i < replaceCells.Length; i++)
        {
            generatedInts[replaceCells[i]] = randomInt;
        }

        Debug.Log(generatedInts);

        //convert to string 
        for (int i = 0; i < generatedInts.Length; i++)
        {
            generatedStr += generatedInts[i].ToString();
        }
        return generatedStr;
    }

    
    private int[,] ReadInPuzzle(string input)
    {
        generatedGrid = new int[9, 9];
        int cell = 0;
        for (int row = 0; row < 9; row++)
        {
            for (int column = 0; column < 9; column++)
            {
                generatedGrid[row,column] = int.Parse(input[cell].ToString());
                cell++;
            }
        }
        return generatedGrid; 

    }

    private void PrintGrid(int[,] gridToPrint, TMP_Text printTo)
    {
        string gridString = "";
        for (int row = 0; row < 9; row++)
        {
            for (int column = 0; column < 9; column++)
            {
                gridString += gridToPrint[row, column] + " ";
            }
            gridString += "\n";
        }
        printTo.text = gridString;
    }

    private bool isValid(int cellRow, int cellColumn, int[,] grid)
    {
        for(int cycleColumn = 0 ; cycleColumn < 9; cycleColumn++)
        {
            //if its in a different column in the SAME ROW - row checker 
            if(cellColumn != cycleColumn && grid[cellRow, cycleColumn] == grid[cellRow, cellColumn])
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
        for (int startRow = (cellRow/3)*3; startRow<(cellRow/3)*3+3; startRow++)
        {
            //vertical cycle within each block :       ^ start column   
            for (int startColumn = (cellColumn/3)*3; startColumn < (cellColumn/3)*3+3; startColumn++)
        {
                //if two different cells within a block have the same number 
                if(startRow!= cellRow && startColumn != cellColumn && grid[startRow, startColumn]== grid[cellRow, cellColumn])
                {
                    return false; 
                }
        }
    }
        return true;
    }

    //overloaded isValid (check wholeGrid)

    private bool isValid(int[,] grid)
    {
        for (int row = 0; row < 9; row++)
        {
            for(int column = 0; column < 9; column++)
            {
                if (grid[row, column] !=0 && !isValid(row, column, grid))
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
        for(int row = 0;row < 9; row++)
        {
            for( int column = 0;column < 9; column++)
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
    private bool FindOneSolution(int[,] grid)
    {
        int[,] freeCellList = GetFreeCellList(grid);
        int k = 0;
        bool hasOneSolution = false;

        while (!hasOneSolution)
        {
            //check k value before assigning free cells
            if (k<0 || k>= freeCellList.GetLength(0))
            {
                return false;
            }
            int freeCellRow = freeCellList[k,0];
            int freeCellColumn = freeCellList[k,1];

            if (grid[freeCellRow,freeCellColumn] == 0)
            {
                grid[freeCellRow,freeCellColumn] = 1;
            }

            if (isValid(freeCellRow, freeCellColumn, grid))
            {
                //all free cells used up
                if (k + 1 == freeCellList.GetLength(0))
                {
                    numSolutions++;
                    Debug.Log("Num solutions: "+ numSolutions);
                    if (numSolutions>1)
                    {
                        return false;
                    }
                    //go back to check if theres other solutions too
                    grid[freeCellRow, freeCellColumn] = 0;
                    k--;
                }
                else
                {
                    //fill next cell
                    k++;
                }
            }
            else if (grid[freeCellRow, freeCellColumn] < 9)
            {
                grid[freeCellRow, freeCellColumn]++;
            }
            else
            {
                //keep backtracking to each prev cell
                while (grid[freeCellRow, freeCellColumn] == 9)
                {
                    grid[freeCellRow, freeCellColumn] = 0;
                    if (k == 0)
                    {
                        return false;
                    }
                    k--; //go to previous free cell + change it 
                         //check k value before assigning free cells
                    if (k >= 0 && k < freeCellList.GetLength(0))
                    {
                        freeCellRow = freeCellList[k, 0];
                        freeCellColumn = freeCellList[k, 1];
                    }
                   
                }
                //once no longer nine: check new values for freed cells 
                grid[freeCellRow, freeCellColumn] += 1; 
            }
        }
        return true; 
    }

    private bool FindFirstSolution(int[,] grid)
    {
        int[,] freeCellList = GetFreeCellList(grid);
        int k = 0;
        bool found = false;

        while (!found)
        {
            int freeCellRow = freeCellList[k, 0];
            int freeCellColumn = freeCellList[k, 1];

            if (grid[freeCellRow, freeCellColumn] == 0)
            {
                grid[freeCellRow, freeCellColumn] = 1;
            }

            if (isValid(freeCellRow, freeCellColumn, grid))
            {
                //all free cells used up
                if (k + 1 == freeCellList.GetLength(0))
                {
                    found = true;
                    //cast as int [,] not object
                    solvedGrid = (int[,])grid.Clone();
                }
                else
                {
                    //fill next cell
                    k++;
                }
            }
            else if (grid[freeCellRow, freeCellColumn] < 9)
            {
                grid[freeCellRow, freeCellColumn]++;
            }
            else
            {
                //keep backtracking to each prev cell
                while (grid[freeCellRow, freeCellColumn] == 9)
                {
                    grid[freeCellRow, freeCellColumn] = 0;
                    if (k == 0)
                    {
                        return false;
                    }
                    k--; //go to previous free cell + change it 
                    freeCellRow = freeCellList[k, 0];
                    freeCellColumn = freeCellList[k, 1];
                }
                //once no longer nine: check new values for freed cells 
                grid[freeCellRow, freeCellColumn] += 1;
            }
        }
        return true;
    }
}

