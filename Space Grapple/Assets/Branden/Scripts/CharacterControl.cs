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

    private float jumpBuffer;
    private float jumpBufferTimer = 0.2f;

    private float groundBuffer;
    private float groundBufferTimer = 0.2f;

    private float jumpCoolDown;
    private float jumpCoolDownTimer = 0.25f;

    private Rigidbody2D _rigidbody;

    [HideInInspector] public int extraJumps = 1;
    public bool isGrounded;
    public Transform groundCheck;
    public float checkRadius = 0.3f;
    public LayerMask whatIsGround;

    protected Vector2 upVector { get {return Vector2.up; } }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        jumpBuffer -= Time.deltaTime;
        groundBuffer -= Time.deltaTime;
        jumpCoolDown -= Time.deltaTime;

        if (isGrounded == false)
        {
            _rigidbody.constraints = RigidbodyConstraints2D.None;
            _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        if (isGrounded == true)
        {
            extraJumps = 1;
            groundBuffer = groundBufferTimer;

            if(GetComponent<DistanceJoint2D>().connectedBody != null)
            {
                _rigidbody.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            }
            else if(GetComponent<DistanceJoint2D>().connectedBody == null)
            {
                _rigidbody.constraints = RigidbodyConstraints2D.None;
                _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpBuffer = jumpBufferTimer;
        }

        if (jumpBuffer > 0 && groundBuffer > 0 && jumpCoolDown < 0)
        {
            Jump();
        }

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded == false && extraJumps > 0)
        {
            DoubleJump();
        }

        MoveInput();
    }

    private void MoveInput()
    {
        if(isGrounded == true)
        {
            _rigidbody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, _rigidbody.velocity.y);
        }
        else
        {
            _rigidbody.AddForce(Vector2.right * Input.GetAxis("Horizontal") * DICheck(_rigidbody.velocity.x) * Time.deltaTime, ForceMode2D.Impulse);
        }
    }

    private void Jump()
    {
        jumpCoolDown = jumpCoolDownTimer;
        _rigidbody.AddForce(upVector * jumpSpeed, ForceMode2D.Impulse);
    }

    private void DoubleJump()
    {
        _rigidbody.velocity = new Vector2(0, jumpSpeed);
        extraJumps--;
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
