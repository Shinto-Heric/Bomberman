using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    // Start is called before the first frame update
    public int timeToExplode;
    private ExplosionHandler explosionHandler;
    void Start()
    {
        explosionHandler = FindObjectOfType<ExplosionHandler>();
        StartCoroutine(SelfDestruct());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(timeToExplode);
        explosionHandler.CreateExplosionAnim(gameObject.transform);
        Destroy(gameObject);
    }
}
