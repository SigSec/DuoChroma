// DuoChroma Gravity Management script by Justinas Grigas
// Copyright (c) Sigma Games
// https://sigsec.github.io
// This script manages the switching of gravity in the game.

using UnityEngine;

public class GravityManager : MonoBehaviour {

	[HideInInspector] public bool isRed = true;
	public float gravityScale = 1.0f;

	private Rigidbody2D _rb2d;

	private void Awake()
	{
		_rb2d = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
		if (transform.position.y > 0)
		{
			isRed = true;
			_rb2d.gravityScale = gravityScale;
			transform.localScale = new Vector3(transform.localScale.x, 1, transform.localScale.z);
		}
		else
		{
			isRed = false;
			_rb2d.gravityScale = 0 - gravityScale;
			transform.localScale = new Vector3(transform.localScale.x, -1, transform.localScale.z);
		}
	}
}
