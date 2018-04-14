/*
Project:	Duo Chroma
Developer:	Justinas Grigas - https://mail.google.com/mail/u/0/?view=cm&fs=1&tf=1&to=jgrigas@elam.co.uk
Version:	0.2.1
Date:		11/04/2018 14:19
*/

using UnityEngine;

public class Pickup : MonoBehaviour {
	// Variables
	[Header("Sprites")]
	public Sprite redSprite;
	public Sprite greenSprite;

	public bool isRed;

	private void Awake()
	{
		if (transform.position.y > 0)
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = greenSprite;
			isRed = true;
		}
		else
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = redSprite;
			isRed = false;
		}
	}
}
