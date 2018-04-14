/*
Project:	Duo Chroma
Developer:	Justinas Grigas - https://mail.google.com/mail/u/0/?view=cm&fs=1&tf=1&to=jgrigas@elam.co.uk
Version:	0.2.2
Date:		12/04/2018 13:40
*/

using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour {

	[Header("Health")]
	public float healthMax;
	public float healthRegen;
	public float healthDecay;
	public float healingSpeed;

	[Space]
	[Header("Movement")]
	public float horizontalSpeed;
	public float jumpHeight;
	public float maxXvelocity;
	public float maxYvelocity;

	[Space]
	[Header("Miscellaneous")]
	public bool isRed = true;
	public bool loadNextScene = false;
	public bool isPaused;

	public int nextSceneIndex;
	public float fadeSpeed;

	[HideInInspector]
	public int keyCount;

	private MenuNavigator _menuNavigator;
	private SpriteRenderer _fadePlane;

	private void Awake()
	{
		_menuNavigator = GameObject.Find("PauseMenu").GetComponent<MenuNavigator>();
		_fadePlane = GameObject.Find("FadePlane").GetComponent<SpriteRenderer>();
	}

	private void Update()
	{
		isPaused = _menuNavigator.isPaused;

		// Loading next scene
		if(loadNextScene)
		{
			if (_fadePlane.color.a < 1)
			{
				_fadePlane.color = new Color(_fadePlane.color.r, _fadePlane.color.g, _fadePlane.color.b, _fadePlane.color.a + fadeSpeed * Time.deltaTime);
			}
			else
			{
				SceneManager.LoadScene(nextSceneIndex);
			}
		}
	}

	public void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
