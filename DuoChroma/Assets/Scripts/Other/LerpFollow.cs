// DuoChroma Lerp Follow script by Justinas Grigas
// Copyright (c) Sigma Games
// https://sigsec.github.io
// This script makes an object follow the target.

using UnityEngine;

public class LerpFollow : MonoBehaviour {
	// Variables
	public Transform targetObject;
	public float lerpSpeed = 1.0f;

	private void Update()
	{
		// Lerp between positions
		float xPosition = Mathf.Lerp(transform.position.x, targetObject.position.x, lerpSpeed);
		float yPosition = Mathf.Lerp(transform.position.y, targetObject.position.y, lerpSpeed);

		// Update current position
		transform.position = new Vector3(xPosition, yPosition, transform.position.z);
	}
}
