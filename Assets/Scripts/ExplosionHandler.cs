using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject explosionAnim;
    public GameObject explosionParent;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CreateExplosionAnim(Transform bombPos)
    {
        

        for (var col = bombPos.position.x - 1; col <= bombPos.position.x + 1; col++)
        {
            for (var row = bombPos.position.y - 1; row <= bombPos.position.y + 1; row++)
            {
                (Instantiate(explosionAnim, new Vector3(col, row, 0f), Quaternion.identity)).transform.parent = explosionParent.transform;
            }
        }
    }
}
