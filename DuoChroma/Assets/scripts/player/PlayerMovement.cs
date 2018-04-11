// DuoChroma Player Movement script by Justinas Grigas
// Copyright (c) Sigma Games
// https://sigsec.github.io
// This script manages the movement of the player.

using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	// Variables.
	[HideInInspector] public bool hasJumped = true;

	public float speed = 10.0f;
	public float jumpHeight = 20.0f;

	public float maxSpeed = 1.5f;
	public float maxJump = 500.0f;

	private Rigidbody2D _rb2d;
	private Animator _anim;
	private GlobalVarables _global;
	private MenuNavigator _menu;

	private bool _hasFallen = false;
	private bool _isRed;

	private void Awake()
	{
		_global = GameObject.Find("Persistent").GetComponent<GlobalVarables>();
		_anim = GetComponent<Animator>();
		_rb2d = GetComponent<Rigidbody2D>();
		_menu = GameObject.Find("EventSystem").GetComponent<MenuNavigator>();
	}

	private void FixedUpdate()
	{
		// Update the isRed variable.
		_isRed = GetComponent<GravityManager>().isRed;
		_anim.SetBool("isRed", _isRed);
		_anim.SetBool("isFalling", _hasFallen);

		// Get input.
		float _horizontalInput;
		float _verticalInput;

		// horizontal
		if (Input.GetKey(_global.left) || Input.GetKey(_global.leftAlt)) { _horizontalInput = -1f; }
		else if (Input.GetKey(_global.right) || Input.GetKey(_global.rightAlt)) { _horizontalInput = 1f; }
		else { _horizontalInput = 0; }

		print(_horizontalInput);

		// vertical
		if (Input.GetKey(_global.down) || Input.GetKey(_global.downAlt)) { _verticalInput = -1f; }
		else if (Input.GetKey(_global.up) || Input.GetKey(_global.upAlt)) { _verticalInput = 1f; }
		else { _verticalInput = 0; }

		if (_menu.isPaused)
		{
			_horizontalInput = _verticalInput = 0;
		}

		// Check for restart
		if (Input.GetKeyDown(_global.restart) && !_menu.isPaused)
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene(_global.level);
		}

		// Update the rotation of the charater.
		if (_horizontalInput < 0) { transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z); }
		else if (_horizontalInput > 0) { transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z); }

		// Checks if the player can jump, based on a criteria.
		if (!hasJumped && !_hasFallen && _rb2d.velocity.y == 0.0f && ((_isRed  && _verticalInput > 0.0f ) || (!_isRed && _verticalInput < 0.0f)))
		{
			hasJumped = true;
			_anim.SetTrigger("Jump");
			_rb2d.velocity = new Vector2(_rb2d.velocity.x, 0.0f);
			_rb2d.AddForce(Vector2.up * _verticalInput * jumpHeight);
		}

		// Checks if the player is moving
		if (_rb2d.velocity.x != 0.0f && _rb2d.velocity.y == 0.0f)
		{
			_anim.SetBool("isWalking", true);
		}
		else
		{
			_anim.SetBool("isWalking", false);
		}
		
		// Checks if the player has fallen.
		if (hasJumped && ((_isRed && _rb2d.velocity.y < 0) || (!_isRed && _rb2d.velocity.y > 0)))
		{
			_hasFallen = true;
		}

		// Move player horizontally
		_rb2d.AddForce(Vector2.right * _horizontalInput * speed);
		_anim.SetFloat("hSpeed", 0.8f + (_rb2d.velocity.x / 2));

		// Checks if the player hasn't gone over the max horizontal velocity.
		if (_rb2d.velocity.x > maxSpeed)
		{
			_rb2d.velocity = new Vector2(maxSpeed, _rb2d.velocity.y);
		}
		else if (_rb2d.velocity.x < 0 - maxSpeed)
		{
			_rb2d.velocity = new Vector2(0 - maxSpeed, _rb2d.velocity.y);
		}
	}

	// Checks for collisions
	private void OnCollisionStay2D(Collision2D collision)
	{
		// Checks if the player touches the floor
		if (collision.gameObject.tag == "Map" && _hasFallen)
		{
			hasJumped = _hasFallen = false;
		}
	}
}
