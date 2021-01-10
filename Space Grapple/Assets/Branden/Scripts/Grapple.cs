using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Grapple : MonoBehaviour
{
   [SerializeField] float grapplePower = 10.0f;
    public DistanceJoint2D joint;
    [SerializeField] private Collider2D myColliderBABY;
    [SerializeField] Transform aimer;
    public GameObject grappleParticles;
    private Vector3 grapplePoint;
    public float pullForce;
    public float grappleLength = 10f;
    [SerializeField] Transform[] objectsToMoveToGrapplePoint;
    [SerializeField] Transform hookEnd;
    [SerializeField] float penetrationAmount = 0.2f;
    [SerializeField] private UnityEvent grappleStart;
    [SerializeField] private UnityEvent grappleEnd;

    private CharacterControl characterControlScript;

    void Start()
    {
        characterControlScript = GetComponent<CharacterControl>();
    }
    void GrappleItBoy(Collider2D otherCollider, Vector3 point,Vector3 normal)
    {
        grappleStart.Invoke();
        joint.autoConfigureDistance = true;
        joint.GetComponent<DistanceJoint2D>().enabled = true;
        grapplePoint = point;
        Instantiate(grappleParticles, grapplePoint, Quaternion.identity);
        hookEnd.position = point + (normal * penetrationAmount);
        hookEnd.SetParent(otherCollider.transform);
        foreach (Transform tran in objectsToMoveToGrapplePoint)
        {
            tran.position = point;
        }
        joint.connectedBody = otherCollider.attachedRigidbody;
        joint.connectedAnchor = otherCollider.transform.InverseTransformPoint(hookEnd.position);
        joint.enableCollision = true;

        //joint.distance = Vector3.Distance(point, transform.TransformPoint(joint.anchor));
        joint.autoConfigureDistance = false;
        //StartCoroutine(GrappleRoutine());
        characterControlScript.canJump = true;
    }

    void GrapplePull(Collider2D otherCollider, Vector3 point)
    {

        joint.GetComponent<DistanceJoint2D>().enabled = true;

        grapplePoint = point;
        Instantiate(grappleParticles, grapplePoint, Quaternion.identity);
        joint.connectedBody = otherCollider.GetComponent<Rigidbody2D>();
        joint.connectedAnchor = otherCollider.transform.InverseTransformPoint(point);

        try
        {
            otherCollider.attachedRigidbody.AddForce(-Vector2.right * pullForce * Time.deltaTime, ForceMode2D.Impulse);
        }
        catch
        {
            return;
        }

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
        aimer.transform.position = transform.position + dir * 2;
        RaycastHit2D hit = (Physics2D.Raycast((Vector2)transform.position, (Vector2)dir, grappleLength));
        if (hit.collider != null)
        {
            OnGrappled grappleEvent = hit.collider.gameObject.GetComponent<OnGrappled>();
            if (grappleEvent != null)
            {
                grappleEvent.Grappled();
            }
            GrappleItBoy(hit.collider, hit.point,hit.normal);
        }
        myColliderBABY.enabled = true;
    }

    void ShootGrapplePull()
    {
        myColliderBABY.enabled = false;
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -Camera.main.transform.position.z;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3 checkPos = transform.position;
        mousePos.z = 0;
        checkPos.z = 0;
        Vector3 dir = (mousePos - checkPos).normalized;
        aimer.transform.position = transform.position + dir * 2;
        RaycastHit2D hit = (Physics2D.Raycast((Vector2)transform.position, (Vector2)dir, Mathf.Infinity));
        if (hit.collider != null)
        {
            GrapplePull(hit.collider, hit.point);
        }
        myColliderBABY.enabled = true;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && characterControlScript.isGrounded == false)
        {
            ShootGrapple();
        }
        if (Input.GetMouseButtonUp(0) || Input.GetKeyDown(KeyCode.Space))
        {
            grappleEnd.Invoke();
            joint.connectedBody = null;
            joint.GetComponent<DistanceJoint2D>().enabled = false;
        }

        if(Input.GetMouseButtonDown(0) && characterControlScript.isGrounded == true)
        {
            ShootGrapple();
            //ShootGrapplePull();
        }

        if (joint != null)
        {
            Vector3 dir = (grapplePoint - transform.position);
            dir.z = 0;
            dir.Normalize();
            aimer.transform.position = transform.position + ((Vector3)dir * 2);
        }
    }

    IEnumerator GrappleRoutine()
    {
        yield return null;
        //yield return null;
        joint.autoConfigureDistance = false;
    }
}
