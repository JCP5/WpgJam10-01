using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterControl : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float jumpSpeed = -10.0f;
    [SerializeField] private float doubleJumpSpeed = 150f;
    public float DIForce = 10f;
    [SerializeField] private float lerpTime = 0.25f;
    private Rigidbody2D _rigidbody;

    public int extraJumps;
    [SerializeField] public int maxExtraJumps = 3;

    public bool canJump;
    public bool isGrounded;
    public Transform groundCheck;
    public float checkRadius = 0.3f;
    public LayerMask whatIsGround;
    private DistanceJoint2D joint;
    public Animator playerAnimator;

    private bool facingRight = true;
    private bool IsGrappled { get { return (joint.connectedBody != null && joint.enabled !=false); } }
    protected Vector2 upVector { get {return Vector2.up; } }
    [SerializeField] private float maxGroundAngle = 60;
    [SerializeField] CapsuleCollider2D capsule;

    public GameObject deathEffect;


    private void Awake()
    {
        joint = GetComponent<DistanceJoint2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        extraJumps = maxExtraJumps;
    }
    private void Update()
    {
        //Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        if (isGrounded == false)
        {
            playerAnimator.SetBool("isGrounded", false);
            playerAnimator.SetBool("isHanging", IsGrappled);
            _rigidbody.constraints = RigidbodyConstraints2D.None;
            _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        if (isGrounded == true)
        {
            canJump = true;
            playerAnimator.SetBool("isGrounded", true);
            playerAnimator.SetBool("isHanging", IsGrappled);
            extraJumps = maxExtraJumps;

            if(joint.connectedBody != null)
            {
                _rigidbody.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            }
            else if(joint.connectedBody == null)
            {
                _rigidbody.constraints = RigidbodyConstraints2D.None;
                _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true && canJump == true)
        {
            Jump();
        }

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded == false && extraJumps > 0 && canJump == true)
        {
            DoubleJump();
        }

        
    }

    /*private void OnCollisionStay2D(Collision2D collision)
    {
        foreach (ContactPoint2D point in collision.contacts)
            {
                if (Vector3.Angle(point.normal, Vector3.up) <= maxGroundAngle)
                {
                    isGrounded = true;
                }
            }
    }*/

    private void FixedUpdate()
    {
        isGrounded = false;
        Vector3 circleSpot = capsule.bounds.center + (-transform.up * ((capsule.size.y / 2) - capsule.size.x / 2));
        RaycastHit2D[] hits = new RaycastHit2D[2];
        ContactFilter2D filter = new ContactFilter2D();
        capsule.enabled = false;
        if (Physics2D.CircleCast(circleSpot, capsule.size.x / 2,  -transform.up, filter, hits, Physics2D.defaultContactOffset + 0.1f)>0)
        {
            foreach(RaycastHit2D hit in hits)
            {
                if (hit==null || hit.collider==null || hit.collider.gameObject == this.gameObject)
                {
                    continue;
                }
               // Debug.Log(Vector3.Angle(hit.normal, transform.up));
                if (Vector3.Angle(hit.normal, transform.up) <= maxGroundAngle)
                {
                    isGrounded = true;
                }
            }
            
        }
        capsule.enabled = true;
        MoveInput();
        
    }

    private void MoveInput()
    {
        float desiredVelocity = Input.GetAxis("Horizontal") * moveSpeed;
        playerAnimator.SetFloat("Speed", Mathf.Abs(desiredVelocity));


        if (facingRight == false && desiredVelocity > 0)
        {
            Flip();
        }
        if (facingRight == true && desiredVelocity < 0)
        {
            Flip();
        }



        /*if (isGrounded == true)
        {
            //_rigidbody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, _rigidbody.velocity.y);
            _rigidbody.AddForce(Vector2.right * Input.GetAxis("Horizontal") * DICheck(_rigidbody.velocity.x) * Time.deltaTime * moveSpeed, ForceMode2D.Impulse);

        }
        else
        {
            _rigidbody.AddForce(Vector2.right * Input.GetAxis("Horizontal") * DICheck(_rigidbody.velocity.x) * Time.deltaTime * moveSpeed, ForceMode2D.Impulse);
        }*/
        if ( (!IsGrappled && isGrounded) || (isGrounded && IsGrappled))
        {
            float frameLerpTime = lerpTime;
            if ((Mathf.Abs(_rigidbody.velocity.x)) > Mathf.Abs(desiredVelocity))
            {
                if (desiredVelocity != 0 && Mathf.Sign(_rigidbody.velocity.x) == Mathf.Sign(desiredVelocity))
                {
                    frameLerpTime *= 40;
                }
                else
                {
                    frameLerpTime *= 5;
                }
            }
            float changeInVelocity = desiredVelocity - _rigidbody.velocity.x;
            if (changeInVelocity != 0 && !(Input.GetAxis("Horizontal") == 0 && !isGrounded))
            {
                float forceVector = (changeInVelocity) / frameLerpTime * _rigidbody.mass;
                _rigidbody.AddForce(new Vector2(forceVector, 0) * Time.fixedDeltaTime);
            }
           
        }
        else
        {
            _rigidbody.AddForce(Vector2.right * Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed * _rigidbody.mass * DIForce);
        }
    }

    private void Jump()
    {
        _rigidbody.AddForce(upVector * jumpSpeed, ForceMode2D.Impulse);
    }

    private void DoubleJump()
    {
        canJump = false;
        //_rigidbody.AddForce(new Vector2(0, jumpSpeed),ForceMode2D.Impulse);
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, doubleJumpSpeed/_rigidbody.mass);
        extraJumps--;
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    float DICheck(float xVelocity)
    {
        if(xVelocity * Input.GetAxis("Horizontal") > 0)
        {
            return DIForce / 2;
        }
        else
        {
            return DIForce;
        }
    }

    public void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        this.gameObject.SetActive(false);
        scenenavigation.instance.Invoke("ReloadLevel", 1.5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Laser"))
        {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Laser"))
        {
            Die();
        }
    }
}
