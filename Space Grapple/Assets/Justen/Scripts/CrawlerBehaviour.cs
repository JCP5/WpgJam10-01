using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerBehaviour : MonoBehaviour
{
    Rigidbody2D rb;
    public float moveSpeed = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.right * moveSpeed;
    }

    private void FixedUpdate()
    {
        CheckGroundInFront();
    }

    void CheckGroundInFront()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + transform.right, Vector2.down, 2);
        Debug.DrawLine(transform.position + transform.right, transform.position + transform.right + Vector3.down);
        if (hit.collider == null)
        {
            Debug.Log("Hello");

            if (transform.rotation.eulerAngles.y == 0)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }
            else
            {
                transform.rotation = Quaternion.Euler(Vector3.zero);
            }
        }
    }
}
