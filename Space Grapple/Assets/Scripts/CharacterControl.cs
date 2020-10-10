using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterControl : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float jumpSpeed = -10.0f;
    private Rigidbody2D _rigidbody;
    protected Vector2 upVector { get {return Vector2.up; } }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        MoveInput();
    }

    private void MoveInput()
    {
        _rigidbody.AddForce(Vector2.right * Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime,ForceMode2D.Impulse);
        //_rigidbody.velocity = new Vector2( Input.GetAxisRaw("Horizontal") * moveSpeed, _rigidbody.velocity.y);
    }

    private void Jump()
    {
        _rigidbody.AddForce(upVector * jumpSpeed, ForceMode2D.Impulse);
    }
}
