using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update

    public float moveSpeed = 5f;
    public LayerMask layerName;
    public Transform movePos;
    float xMovePos = 1;
    float yMovePos = 1;
    int randVal;
    [SerializeField] float killPoints;
    CanvasHandler canvasHandler;
    void Start()
    {
        canvasHandler = FindObjectOfType<CanvasHandler>();
        StartCoroutine(MoveEnemy());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator MoveEnemy()
    {
        while(true)
        {
            randVal = UnityEngine.Random.Range(0, 100);
            if (randVal % 2 == 0)
            {

                if (randVal % 4 == 0)
                    xMovePos *= -1;
                FlipSprite();

                if (!Physics2D.OverlapCircle(gameObject.transform.position + new Vector3(xMovePos, 0f, 0f), 0.1f, layerName))
                    gameObject.transform.Translate(new Vector3(xMovePos, 0, 0));
                else
                    gameObject.transform.Translate(new Vector3(0, 0, 0));
            }
            else
            {

                if ((randVal + 1) % 4 == 0)
                    yMovePos *= -1;
                if (!Physics2D.OverlapCircle(gameObject.transform.position + new Vector3(0f, yMovePos, 0f), 0.1f, layerName))
                    gameObject.transform.Translate(new Vector3(0, yMovePos, 0));
                else
                    gameObject.transform.Translate(new Vector3(0, 0, 0));
            }
            yield return new WaitForSeconds(1);
    }

}

    private void FlipSprite()
    {
        if (xMovePos < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
    public void AddKillPoints()
    {
        canvasHandler.AddScore(killPoints);
    }
}
