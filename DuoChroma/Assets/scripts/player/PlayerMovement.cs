/*
Project:	Duo Chroma
Developer:	Justinas Grigas - https://mail.google.com/mail/u/0/?view=cm&fs=1&tf=1&to=jgrigas@elam.co.uk
Version:	0.2.2
Date:		12/04/2018 13:34
*/

using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	// Variables.
	private PlayerManager _playerManager;
	private GlobalVarables _globalVariables;
	private Animator _playerAnimator;
	private Rigidbody2D _playerRigidbody2D;
	
	private bool _isRed;
	private bool _isFalling = true;
	private bool _hasJumped = true;

	private float _horizontalSpeed;
	private float _jumpHeight;
	private float _maxXvelocity;
	private float _maxYvelocity;

	private void Awake()
	{
		_globalVariables = GameObject.Find("Persistent").GetComponent<GlobalVarables>();
		_playerManager = GetComponent<PlayerManager>();
		_playerAnimator = GetComponent<Animator>();
		_playerRigidbody2D = GetComponent<Rigidbody2D>();

		_horizontalSpeed = _playerManager.horizontalSpeed;
		_jumpHeight = _playerManager.jumpHeight;
		_maxXvelocity = _playerManager.maxXvelocity;
		_maxYvelocity = _playerManager.maxYvelocity;
	}

	private void FixedUpdate()
	{
		float horizontalInput = 0;
		float verticalInput = 0;

		// Get input from the player.
		if (Input.GetKey(_globalVariables.left) || Input.GetKey(_globalVariables.leftAlt))
		{
			horizontalInput = -1;
		}
		else if (Input.GetKey(_globalVariables.right) || Input.GetKey(_globalVariables.rightAlt))
		{
			horizontalInput = 1;
		}

		if (Input.GetKey(_globalVariables.up) || Input.GetKey(_globalVariables.upAlt))
		{
			verticalInput = 1;
		}
		else if (Input.GetKey(_globalVariables.down) || Input.GetKey(_globalVariables.downAlt))
		{
			verticalInput = -1;
		}

		// Check if the player can jump.
		if (verticalInput != 0 && !_hasJumped)
		{
			_playerAnimator.SetTrigger("Jump");
			_hasJumped = true;
			_playerRigidbody2D.AddForce(Vector2.up * verticalInput * _jumpHeight);
		}

		// Move the player horizontally.
		if (horizontalInput != 0)
		{
			_playerRigidbody2D.AddForce(Vector2.right * horizontalInput * _horizontalSpeed);
			transform.localScale = new Vector3(horizontalInput, transform.localScale.y, 1);
		}

		// Check if the player is moving horizontally.
		if (_playerRigidbody2D.velocity.x == 0 || _hasJumped)
		{
			if (!_hasJumped)
			{
				transform.position = new Vector3(Mathf.Round(transform.position.x * 100) / 100, transform.position.y, transform.position.z);
			}
			_playerAnimator.SetBool("isWalking", false);
		}
		else
		{
			_playerAnimator.SetBool("isWalking", true);
		}

		// Update the is red variable.
		if (transform.position.y > 0)
		{
			_isRed = true;
			transform.localScale = new Vector3(transform.localScale.x, 1, 1);
			_playerRigidbody2D.gravityScale = 1;

			if (_hasJumped && _playerRigidbody2D.velocity.y <= 0 && !_isFalling)
			{
				_isFalling = true;
			}
		}
		else
		{
			_isRed = false;
			transform.localScale = new Vector3(transform.localScale.x, -1, 1);
			_playerRigidbody2D.gravityScale = -1;

			if (_hasJumped && _playerRigidbody2D.velocity.y >= 0 && !_isFalling)
			{
				_isFalling = true;
			}
		}

		// Limit the velocity of the player
		if (_playerRigidbody2D.velocity.x > _maxXvelocity)
		{
			_playerRigidbody2D.velocity = new Vector2(_maxXvelocity, _playerRigidbody2D.velocity.y);
		}
		else if (_playerRigidbody2D.velocity.x < 0 -_maxXvelocity)
		{
			_playerRigidbody2D.velocity = new Vector2(0 - _maxXvelocity, _playerRigidbody2D.velocity.y);
		}

		if (_playerRigidbody2D.velocity.y > _maxYvelocity)
		{
			_playerRigidbody2D.velocity = new Vector2(_playerRigidbody2D.velocity.x, _maxYvelocity);
		}
		else if (_playerRigidbody2D.velocity.y < 0 - _maxYvelocity)
		{
			_playerRigidbody2D.velocity = new Vector2(_playerRigidbody2D.velocity.x, 0 - _maxYvelocity);
		}

		// Update animator variables.
		_playerManager.isRed = _isRed;
		_playerAnimator.SetBool("isFalling", _isFalling);
		_playerAnimator.SetBool("isRed", _isRed);
		_playerAnimator.SetFloat("hSpeed", Mathf.Abs(_playerRigidbody2D.velocity.x) / 2 + 0.6f);

		// Restart Game.
		if (Input.GetKeyDown(_globalVariables.restart))
		{
			_playerManager.Restart();
		}
	}

	// Checks for collisions.
	private void OnCollisionStay2D(Collision2D collision)
	{
		// Checks if the player touches the floor.
		if (collision.gameObject.tag == "Map" && _isFalling && _playerRigidbody2D.velocity.y == 0)
		{
			_hasJumped = false;
			_isFalling = false;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Gem")
		{
			GetComponent<PlayerHealth>().isHealing = true;
			Destroy(collision.gameObject);
		}
		else if (collision.gameObject.tag == "Key")
		{
			_playerManager.keyCount++;
			Destroy(collision.gameObject);
		}
		else if (collision.gameObject.tag == "Door" && _playerManager.keyCount > 0)
		{
			_playerManager.keyCount--;
			collision.gameObject.GetComponent<Door>().isOpening = true;
		}
		else if (collision.gameObject.tag == "MultiGem")
		{
			_playerManager.loadNextScene = true;
		}
	}
}
