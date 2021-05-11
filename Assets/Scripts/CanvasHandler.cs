using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasHandler : MonoBehaviour
{
    // Start is called before the first frame update

    public Button restartButton;
    public Image imageBG;
    public Text gameOverStatus;
    public Text scoreTest;
    [SerializeField] AudioClip winSound;
    [SerializeField] AudioClip lossSound;
    [SerializeField] [Range(0, 1)] float soundVolume = 0.8f;
    float score;
    void Start()
    {
        score = 0;
        restartButton.gameObject.SetActive(false);
        imageBG.gameObject.SetActive(false);
        gameOverStatus.gameObject.SetActive(false);
        scoreTest.gameObject.SetActive(true);
        scoreTest.text = "Score : " + score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerGameOver(bool isWon)
    {
        if(isWon)
        {
            gameOverStatus.text = "You Won!!!\nScore : " + score.ToString();
            AudioSource.PlayClipAtPoint(winSound, Camera.main.transform.position, soundVolume);

        }
        else
        {
            AudioSource.PlayClipAtPoint(lossSound, Camera.main.transform.position, soundVolume);
            gameOverStatus.text = "You Died!!!\nScore : " + score.ToString();
        }
        restartButton.gameObject.SetActive(true);
        imageBG.gameObject.SetActive(true);
        gameOverStatus.gameObject.SetActive(true);
        scoreTest.gameObject.SetActive(false);
    }


    public void AddScore(float points)
    {
        score += points;
        scoreTest.text = "Score : " + score.ToString();
    }
}
