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

	[Space]
	public int nextSceneIndex;

	[HideInInspector]
	public bool _isRed;
	public bool _isOpen = false;
	public bool _loadNextScene = false;

	[Header("Timing")]
	[Range(1,32)]
	public int pixelsToMove;
	[Range(0, 1)]
	public float pause;

	public float alphaIncrease;

	private SpriteRenderer fadePlane;
	private float _currentTimer;

	private Vector3 startPosition;

	private void Awake()
	{
		startPosition = transform.position;
		_currentTimer = pause;

		fadePlane = GameObject.Find("FadePlane").GetComponent<SpriteRenderer>();

		if (transform.position.y > 0)
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = greenSprite;
			_isRed = true;
		}
		else
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = redSprite;
			_isRed = false;
		}
	}

	private void FixedUpdate()
	{
		// Door opening
		if (_isRed && _isOpen && _currentTimer >= pause)
		{
			if (transform.position.y > startPosition.y - 0.32)
			{
				transform.position = new Vector3(transform.position.x, transform.position.y - (float)pixelsToMove / 100, transform.position.z);
				_currentTimer = 0;
			}

			else
			{
				Destroy(gameObject);
			}
		}
		else if (!_isRed && _isOpen && _currentTimer >= pause)
		{
			if (transform.position.y < startPosition.y + 0.32)
			{
				transform.position = new Vector3(transform.position.x, transform.position.y + (float)pixelsToMove / 100, transform.position.z);
				_currentTimer = 0;
			}

			else
			{
				Destroy(gameObject);
			}
		}

		_currentTimer += Time.deltaTime;

		// Loading next scene
		if (_loadNextScene)
		{
			if (fadePlane.color.a < 1)
			{
				fadePlane.color = new Color(fadePlane.color.r, fadePlane.color.g, fadePlane.color.b, fadePlane.color.a + alphaIncrease * Time.deltaTime);
			}

			else
			{
				UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneIndex);
			}
		}
	}
}
