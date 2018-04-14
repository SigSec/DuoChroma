/*
Project:	Duo Chroma
Developer:	Justinas Grigas - https://mail.google.com/mail/u/0/?view=cm&fs=1&tf=1&to=jgrigas@elam.co.uk
Version:	0.2.3
Date:		14/04/2018 14:08
*/

using UnityEngine;

public class Door : MonoBehaviour
{
	public bool isOpening;

	public float openingSpeed;
	public int pixelToMove;

	private Vector3 _startPosition;
	private float _timer;

	private void Awake()
	{
		_startPosition = transform.position;
	}

	private void Update()
	{
		if (GetComponent<Pickup>().isRed && isOpening && _timer >= openingSpeed)
		{
			if (transform.position.y > _startPosition.y - 0.32f)
			{
				transform.position = new Vector3(transform.position.x, transform.position.y - (float)pixelToMove / 100, transform.position.z);
				_timer = 0;
			}
			else
			{
				Destroy(gameObject);
			}
		}
		else if (!GetComponent<Pickup>().isRed && isOpening && _timer >= openingSpeed)
		{
			if (transform.position.y < _startPosition.y + 0.32f)
			{
				transform.position = new Vector3(transform.position.x, transform.position.y + (float)pixelToMove / 100, transform.position.z);
				_timer = 0;
			}
			else
			{
				Destroy(gameObject);
			}
		}

		_timer += Time.deltaTime;
	}
}