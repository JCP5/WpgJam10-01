using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterControl : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float jumpSpeed = -10.0f;
    public float DIForce = 10f;
    [SerializeField] private float lerpTime = 0.25f;
    private Rigidbody2D _rigidbody;

    [HideInInspector] public int extraJumps = 1;
    public bool isGrounded;
    public Transform groundCheck;
    public float checkRadius = 0.3f;
    public LayerMask whatIsGround;
    private DistanceJoint2D joint;
    public Animator playerAnimator;

    private bool facingRight = true;
    private bool IsGrappled { get { return (joint.connectedBody != null && joint.enabled !=false); } }
    protected Vector2 upVector { get {return Vector2.up; } }

    private void Awake()
    {
        joint = GetComponent<DistanceJoint2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        if (isGrounded == false)
        {
            playerAnimator.SetBool("isGrounded", false);
            playerAnimator.SetBool("isHanging", IsGrappled);
            _rigidbody.constraints = RigidbodyConstraints2D.None;
            _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        if (isGrounded == true)
        {
            playerAnimator.SetBool("isGrounded", true);
            playerAnimator.SetBool("isHanging", IsGrappled);
            extraJumps = 1;

            if(joint.connectedBody != null)
            {
                _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;//RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            }
            else if(joint.connectedBody == null)
            {
                _rigidbody.constraints = RigidbodyConstraints2D.None;
                _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            Jump();
        }

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded == false && extraJumps > 0)
        {
            DoubleJump();
        }

        
    }

    private void FixedUpdate()
    {
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
        if (!IsGrappled || (isGrounded && IsGrappled))
        {
            float changeInVelocity = desiredVelocity - _rigidbody.velocity.x;
            if (changeInVelocity != 0 && !(Input.GetAxis("Horizontal") == 0 && !isGrounded))
            {
                float forceVector = (changeInVelocity) / lerpTime * _rigidbody.mass;
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
        _rigidbody.velocity = new Vector2(0, jumpSpeed) / _rigidbody.mass;
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
}
