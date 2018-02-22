using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    // Set up variables.
    public float xVelocityCap = 8.0f;
    public float yVelocityCap = 10.0f;

    public float gravityScale = 0.7f;
    public float runningSpeed = 7.0f;
    public float jumpForce = 300.0f;

    private Transform _playerTransform;
    private Rigidbody2D _playerRigidbody;

    private float _horizontalInput;
    private float _verticalInput;

    private bool _hasJumped = true;
    private bool _hasStomped = true;
	
    private void Awake()
    {
        _playerTransform = GetComponent<Transform>();
        _playerRigidbody = GetComponent<Rigidbody2D>();
    }

	private void Update () {
        // Get user input.
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");

        // Checks which side the user is on.
        if (_playerTransform.position.y > 0)
        {
            _playerRigidbody.gravityScale = gravityScale;
        }
        else
        {
            // Inverts vertical input.
            _verticalInput *= -1;
            _playerRigidbody.gravityScale = -1 * gravityScale;
        }

        // Checks if the user has pressed up, has not jumped, and his y velocity is zero.
        if (_verticalInput == 1 && !_hasJumped && _playerRigidbody.velocity.y == 0)
        {
            _hasJumped = true;
            _playerRigidbody.velocity = new Vector2(_playerRigidbody.velocity.x, 0.0f);
        }
        else if (_verticalInput == -1 && !_hasStomped)
        {
            _hasStomped = true;
            _playerRigidbody.velocity = new Vector2(_playerRigidbody.velocity.x, 0.0f);
        }
        // Resets the vertical input.
        else
        {
            _verticalInput = 0;
        }
    }

    private void FixedUpdate()
    {
        // Process input.
        // Checks if the velocity of the player has gone over the maximi
        if (_playerRigidbody.velocity.x > xVelocityCap)
        {
            _playerRigidbody.velocity = new Vector2(xVelocityCap, _playerRigidbody.velocity.y);
        } else if (_playerRigidbody.velocity.x < 0 - xVelocityCap)
        {
            _playerRigidbody.velocity = new Vector2(0 - xVelocityCap, _playerRigidbody.velocity.y);
        }
        // Does the same for the y velocity.
        if (_playerRigidbody.velocity.y > yVelocityCap)
        {
            _playerRigidbody.velocity = new Vector2(_playerRigidbody.velocity.x, yVelocityCap);
        }
        else if (_playerRigidbody.velocity.y < 0 - yVelocityCap)
        {
            _playerRigidbody.velocity = new Vector2(_playerRigidbody.velocity.x, 0 - yVelocityCap);
        }
        _playerRigidbody.AddForce(new Vector2(_horizontalInput * runningSpeed, _verticalInput * jumpForce));
    }

    private void OnCollisionStay2D(Collision2D collision2D)
    {
        if(collision2D.collider.sharedMaterial.name == "tile")
        {
            _hasJumped = _hasStomped = false;
        }
    }
}