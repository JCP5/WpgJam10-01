using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
   [SerializeField] float grapplePower = 10.0f;
    private DistanceJoint2D joint;
    [SerializeField] private Collider2D myColliderBABY;
    [SerializeField] Transform aimer;
    public GameObject quad;
    private Vector3 grapplePoint;
    void GrappleItBoy(Collider2D otherCollider, Vector3 point)
    {
        //Aww yeah
        if (joint != null)
        {
            Destroy(joint);
        }
        grapplePoint = point;
        Instantiate(quad, grapplePoint, Quaternion.identity);
        joint = gameObject.AddComponent<DistanceJoint2D>();
            joint.connectedBody = otherCollider.attachedRigidbody;
            joint.connectedAnchor = otherCollider.transform.InverseTransformPoint(point);
        joint.enableCollision = true;
        
    }

    void ShootGrapple()
    {
        
        myColliderBABY.enabled = false;
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -Camera.main.transform.position.z;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3 checkPos = transform.position;
        mousePos.z = 0;
        checkPos.z = 0;
        Vector3 dir = (mousePos - checkPos).normalized;
        Debug.Log(dir);
        aimer.transform.position = transform.position + dir * 2;
        RaycastHit2D hit = (Physics2D.Raycast((Vector2)transform.position, (Vector2)dir, Mathf.Infinity));
        if (hit.collider!=null)
        {
            GrappleItBoy(hit.collider, hit.point);
        }
        myColliderBABY.enabled = true;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShootGrapple();
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (joint != null)
            {
                Destroy(joint);
            }
        }
        if (joint != null)
        {
            Vector3 dir = (grapplePoint - transform.position);
            dir.z = 0;
            dir.Normalize();
            aimer.transform.position = transform.position + ((Vector3)dir * 2);
        }
    }
}
