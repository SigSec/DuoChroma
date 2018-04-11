/*
Project:	Duo Chroma
Developer:	Justinas Grigas - https://mail.google.com/mail/u/0/?view=cm&fs=1&tf=1&to=jgrigas@elam.co.uk
Version:	0.2.1
Date:		11/04/2018 14:18
*/

using UnityEngine;

public class LerpFollow : MonoBehaviour {
	// Variables
	public Transform targetObject;
	public float lerpSpeed = 1.0f;

	private void Awake()
	{
		transform.position = new Vector3(targetObject.position.x, targetObject.position.y, -10);
	}

	private void Update()
	{
		// Lerp between positions
		float xPosition = Mathf.Lerp(transform.position.x, targetObject.position.x, lerpSpeed);
		float yPosition = Mathf.Lerp(transform.position.y, targetObject.position.y, lerpSpeed);

		// Update current position
		transform.position = new Vector3(xPosition, yPosition, transform.position.z);
	}
}
