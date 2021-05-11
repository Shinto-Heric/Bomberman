using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayHandler : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Player;
    public GameObject Enemy;
    public GameObject Enemies;
    public GameObject portalCell;
    private GridHandler gridHandler;
    private GameObject[,] gridArray;
    public int enemyCount;
    private int rowCount;
    private int columnCount;
    private bool gameOver =  false;
    void Start()
    {
        InitGameplayHandler();
        CreatePlayerView();
        CreateEnemies();
    }

    private void InitGameplayHandler()
    {
        gridHandler = FindObjectOfType<GridHandler>();
        gridHandler.InitGridHandler();
        gridArray = gridHandler.GetGridArray();
        rowCount = gridArray.GetLength(0);
        columnCount = gridArray.GetLength(1);
    }

    private void CreatePlayerView()
    {
        Instantiate(Player, gridArray[1, columnCount - 2].transform.position, Quaternion.identity);
    }
    private void CreateEnemies()
    {

        int rowIndex, columnIndex;
        Transform trans;

        while (enemyCount > 0)
        {
            rowIndex = UnityEngine.Random.Range(0, rowCount);
            columnIndex = UnityEngine.Random.Range(0, columnCount);
            if (gridArray[rowIndex, columnIndex].tag == "Empty")
            {
                trans = gridArray[rowIndex, columnIndex].transform;
                Instantiate<GameObject>(Enemy, trans.position, Quaternion.identity).transform.parent = Enemies.transform;
                enemyCount--;
            }

        }
    }
    // Update is called once per frame
    void Update()
    {
        if(Enemies.transform.childCount == 0 && !gameOver)
        {
            gameOver = true;
            Instantiate<GameObject>(portalCell, gridArray[1, columnCount - 2].transform.position, Quaternion.identity);
        }
    }
}
