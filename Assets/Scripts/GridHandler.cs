using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public int columnCount;
    public int rowCount;
    private float cellDimen = 32f;
    public GameObject emptyCell;
    public GameObject destructableCell;
    public GameObject inDestructableCell;
    public GameObject reservedCell;
    public GameObject[,] gridArray;
    public GameObject emptyCells;
    public GameObject inDestructableCells;
    public GameObject destructableCells;
    public int destructableCellsCount;
    private int randFactor;
    void Start()
    {

    }

    public void InitGridHandler()
    {
        gridArray = new GameObject[rowCount, columnCount];
        randFactor = UnityEngine.Random.Range(2, 4);
        CreateGameArea();
        CreateIndestructableBlocks();
        CreateReservedBlocks();
        CreateDestructibleBlocks();
    }

    private void CreateDestructibleBlocks()
    {
        int rowIndex, columnIndex;
        Transform trans;

        while (destructableCellsCount > 0)
        {
            rowIndex = UnityEngine.Random.Range(0, rowCount);
            columnIndex = UnityEngine.Random.Range(0, columnCount);
            if (gridArray[rowIndex, columnIndex].tag == "Empty")
            {
                trans = gridArray[rowIndex, columnIndex].transform;
                Destroy(gridArray[rowIndex, columnIndex]);
                gridArray[rowIndex, columnIndex] = Instantiate<GameObject>(destructableCell, trans.position, Quaternion.identity) as GameObject;
                gridArray[rowIndex, columnIndex].transform.parent = destructableCells.transform;
                destructableCellsCount--;
            }
            
        }
    }

    private void CreateReservedBlocks()
    {
        Transform trans;

        for (int rowIndex = 1; rowIndex < 4; rowIndex++)
        {
            for (int colIndex = columnCount - 4; colIndex < columnCount - 1; colIndex++)
            {
                if (gridArray[rowIndex, colIndex].tag == "Empty")
                {
                    trans = gridArray[rowIndex, colIndex].transform;
                    Destroy(gridArray[rowIndex, colIndex]);
                    gridArray[rowIndex, colIndex] = Instantiate<GameObject>(reservedCell, trans.position, Quaternion.identity) as GameObject;
                    gridArray[rowIndex, colIndex].transform.parent = emptyCells.transform;
                }
            }
        }
    }

    private void CreateIndestructableBlocks()
    {
        Transform trans;
        bool createCell;
        for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
        {
            for (int colIndex = 0; colIndex < columnCount; colIndex++)
            {
                createCell = false;
                if(gridArray[rowIndex, colIndex].tag == "Empty")
                {
                    if (colIndex == 0 || colIndex == columnCount - 1 || rowIndex == 0 || rowIndex == rowCount - 1)
                    {
                        createCell = true;
                    }
                    else if(rowIndex % randFactor == 0 && colIndex % randFactor != 0)
                    {
                        createCell = true;
                    }
                    
                    if(createCell)
                    {
                        trans = gridArray[rowIndex, colIndex].transform;
                        Destroy(gridArray[rowIndex, colIndex]);
                        gridArray[rowIndex, colIndex] = Instantiate<GameObject>(inDestructableCell, trans.position, Quaternion.identity) as GameObject;
                        gridArray[rowIndex, colIndex].transform.parent = inDestructableCells.transform;
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void CreateGameArea()
    {

        int row = rowCount;
        int column = columnCount;
        int centerRow, centerCol;
        float tempX = 0; ;
        float tempY = 0;

        for (int rowIndex = 0; rowIndex < row; rowIndex++)
        {
            for (int colIndex = 0; colIndex < column; colIndex++)
            {
                gridArray[rowIndex,colIndex] = Instantiate<GameObject>(emptyCell, new Vector3(rowIndex, colIndex, 0), Quaternion.identity) as GameObject;
                gridArray[rowIndex, colIndex].transform.parent = emptyCells.transform;
            }
        }

        if (row % 2 == 0)
        {
            centerRow = row / 2 - 1;
        }
        else
        {
            centerRow = row / 2;
        }
        if (column % 2 == 0)
        {
            centerCol = column / 2 - 1;
        }
        else
        {
            centerCol = column / 2;
        }
        if (row % 2 == 0 && column % 2 == 0)
        {
            tempX = gridArray[centerRow, centerCol].transform.position.x;
            tempY = gridArray[centerRow, centerCol].transform.position.y;
            tempX = tempX + 0.5f;
            tempY = tempY + 0.5f;
        }
        else if (row % 2 == 0 && column % 2 != 0)
        {
            tempX = gridArray[centerRow, centerCol].transform.position.x;
            tempY = gridArray[centerRow, centerCol].transform.position.y;
            tempX = tempX + 0.5f;
            tempY = tempY + 0f;
        }
        else if (row % 2 != 0 && column % 2 == 0)
        {
            tempX = gridArray[centerRow, centerCol].transform.position.x;
            tempY = gridArray[centerRow, centerCol].transform.position.y;
            tempX = tempX + 0f;
            tempY = tempY + 0.5f;
        }
        else if (row % 2 != 0 && column % 2 != 0)
        {
            tempX = gridArray[centerRow, centerCol].transform.position.x;
            tempY = gridArray[centerRow, centerCol].transform.position.y;
            tempX = tempX + 0f;
            tempY = tempY + 0f;
        }
        for (int rowIndex = 0; rowIndex < row; rowIndex++)
        {
            for (int colIndex = 0; colIndex < column; colIndex++)
            {
                gridArray[rowIndex, colIndex].gameObject.transform.position = new Vector3((gridArray[rowIndex, colIndex].transform.position.x - tempX), (gridArray[rowIndex, colIndex].transform.position.y - tempY));
            }
        }

    }

    public void DestroyAndCreateEmpty(Transform transPos)
    {
        int rowIndex, colIndex;
        Transform trans;
        for ( rowIndex = 0;rowIndex<rowCount;rowIndex++)
        {
            for( colIndex =0; colIndex<columnCount;colIndex++)
            {
                if(gridArray[rowIndex, colIndex].transform.tag == "CanDestroy")
                {
                    if (gridArray[rowIndex, colIndex].transform == transPos)
                    {
                        trans = gridArray[rowIndex, colIndex].transform;
                        Destroy(gridArray[rowIndex, colIndex]);
                        gridArray[rowIndex, colIndex] = Instantiate<GameObject>(emptyCell, trans.position, Quaternion.identity) as GameObject;
                        gridArray[rowIndex, colIndex].transform.parent = emptyCells.transform;
                    }
                }
            }
        }
    }

    public GameObject[,] GetGridArray()
    {
        return gridArray;
    }
}
