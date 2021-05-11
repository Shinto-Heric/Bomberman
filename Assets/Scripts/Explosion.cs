using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Explosion : MonoBehaviour
{
    // Start is called before the first frame update
    public int timeToExplode;
    public GameObject emptyCell;
    private GridHandler gridHandler;
    private CanvasHandler canvasHandler;
    private Enemy enemy;
    [SerializeField] AudioClip bombExplodeSound;
    [SerializeField][Range(0,1)] float soundVolume = 0.8f;
    void Start()
    {
        canvasHandler = FindObjectOfType<CanvasHandler>();
        gridHandler = FindObjectOfType<GridHandler>();
        enemy = FindObjectOfType<Enemy>();
        StartCoroutine(SelfDestruct());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(timeToExplode);
        if(gameObject != null)
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(bombExplodeSound, Camera.main.transform.position, soundVolume);
        if (collision.gameObject != null)
        {
            if (collision.gameObject.tag == "CanDestroy")
            {
                collision.gameObject.SendMessage("AddDestroyPoints");
                gridHandler.DestroyAndCreateEmpty(collision.gameObject.transform);
            }
            else if (collision.gameObject.tag == "Enemy")
            {

                collision.gameObject.SendMessage("AddKillPoints");
                Destroy(collision.gameObject);
            }
            else if (collision.gameObject.tag == "Player")
            {
                Destroy(collision.gameObject);
                canvasHandler.TriggerGameOver(false);
            }

        }
        
    }
   
}
