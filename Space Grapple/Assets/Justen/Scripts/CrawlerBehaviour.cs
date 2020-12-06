using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerBehaviour : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CheckGroundInFront()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.forward, Vector2.down, 2);

        if (hit.collider == null)
        {
            //transform.rotation += Quaternion.Euler(new Vector3(0, Mathf.Sign()
        }
    }
}
