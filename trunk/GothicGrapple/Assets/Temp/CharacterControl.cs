using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
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
        _rigidbody.AddForce(Vector2.right * Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime);
    }

    private void Jump()
    {
        _rigidbody.AddForce(upVector * jumpSpeed);
    }
}
