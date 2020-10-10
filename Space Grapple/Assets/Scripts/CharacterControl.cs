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

    private Rigidbody2D _rigidbody;

    private int extraJumps = 1;
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
        Debug.Log(_rigidbody.velocity);
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        if(isGrounded == true)
        {
            extraJumps = 1;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
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
