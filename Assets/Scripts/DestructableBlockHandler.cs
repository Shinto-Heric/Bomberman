using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableBlockHandler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float destroyPoints;
    CanvasHandler canvasHandler;
    void Start()
    {
        canvasHandler = FindObjectOfType<CanvasHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddDestroyPoints()
    {
        canvasHandler.AddScore(destroyPoints);
    }
}
