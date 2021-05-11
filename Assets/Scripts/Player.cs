using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float moveSpeed = 5f;
    public LayerMask layerName;
    public Transform movePos;
    float xMovePos;
    float yMovePos;

    public GameObject bombPrefab;
    private GameObject bombReference;
    private CanvasHandler canvasHandler;
    // Start is called before the first frame update
    void Start()
    {
        canvasHandler = FindObjectOfType<CanvasHandler>();
        movePos.parent = null;
        bombReference = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            TriggerBomb();
        }
        MovePlayer();
    }

    private void TriggerBomb()
    {
        if(bombReference == null)
            bombReference = Instantiate(bombPrefab, movePos.position, Quaternion.identity) as GameObject;
    }

    private void MovePlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePos.position, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, movePos.position) <= 0.05f)
        {
            xMovePos = Input.GetAxisRaw("Horizontal");
            yMovePos = Input.GetAxisRaw("Vertical");
            if (Mathf.Abs(xMovePos) == 1f)
            {
                FlipSprite();

                if (!Physics2D.OverlapCircle(movePos.position + new Vector3(xMovePos, 0f, 0f), 0.1f, layerName))
                    movePos.position += new Vector3(xMovePos, 0f, 0f);
                else
                    movePos.position = transform.position;
            }
            else if (Mathf.Abs(yMovePos) == 1f)
            {
                if (!Physics2D.OverlapCircle(movePos.position + new Vector3(0f, yMovePos, 0f), 0.1f, layerName))
                    movePos.position += new Vector3(0f, yMovePos, 0f);
                else
                    movePos.position = transform.position;
            }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != null)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                Destroy(collision.gameObject);
                canvasHandler.TriggerGameOver(false);
                Destroy(gameObject);
            }
            else if (collision.gameObject.tag == "Finish")
            {
                canvasHandler.TriggerGameOver(true);
            }
        }
    }
}
