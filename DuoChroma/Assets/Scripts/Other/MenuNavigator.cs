/*
Project:	Duo Chroma
Developer:	Justinas Grigas - https://mail.google.com/mail/u/0/?view=cm&fs=1&tf=1&to=jgrigas@elam.co.uk
Version:	0.2.0
Date:		10/04/2018 19:12
*/

using System;
using System.Collections.Generic;
using UnityEngine;

public class MenuNavigator : MonoBehaviour {
	// Variables
	public Canvas[] arrayOfMenus;
	public UnityEngine.UI.Text[] arrayOfSettings;
	public UnityEngine.UI.Text[] arrayOfKeys;

	public bool alwaysPaused = false;
	public bool isPaused;

	private GlobalVarables _global;
	private GravityManager _gManager;
	private List<GameObject> arrayOfOptions = new List<GameObject>();

	private KeyCode lastKey;

	private Color _inactiveTextColour;
	private Color _activeTextColour;

	private int _menuIndex = 0;
	private int _optionIndex = 2;
	private int _keyToChange = 0;

	private bool _waitingForInput = false;

	// Custom functions.

	private void UpdateArrayOfOptions()
	{
		arrayOfOptions.Clear();

		for (int i = 0; i < arrayOfMenus[_menuIndex].gameObject.transform.childCount; i++)
		{
			arrayOfOptions.Add(arrayOfMenus[_menuIndex].gameObject.transform.GetChild(i).gameObject);
		}
	}

	// MonoBehaviour functions.
	private void Awake()
	{
		if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex != 0)
		{ _gManager = GameObject.Find("Player").GetComponent<GravityManager>(); }
		_global = GameObject.Find("Persistent").GetComponent<GlobalVarables>();
	}

	private void Update()
	{

		if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex != 0)
		{
			if (_gManager.isRed)
			{
				_inactiveTextColour = new Color(138f / 255f, 28f / 255f, 53f / 255f, 1f);
				_activeTextColour = new Color(209f / 255f, 59f / 255f, 59f / 255f, 1f);
			}
			else
			{
				_inactiveTextColour = new Color(7f / 255f, 49f / 255f, 54f / 255f, 1f);
				_activeTextColour = new Color(21f / 255f, 87f / 255f, 72f / 255f, 1f);
			}
		}
		else
		{
			_inactiveTextColour = new Color(138f / 255f, 28f / 255f, 53f / 255f, 1f);
			_activeTextColour = new Color(209f / 255f, 59f / 255f, 59f / 255f, 1f);
		}

		foreach (var menu in arrayOfMenus)
		{
			menu.gameObject.SetActive(false);
		}

		if (Input.GetKeyDown(_global.menu) && !alwaysPaused)
		{
			isPaused = !isPaused;
		}

		if (isPaused)
		{
			Time.timeScale = 0;
			UpdateArrayOfOptions();

			// Get input.
			float _horizontalInput;
			float _verticalInput;
			// horizontal
			if (Input.GetKeyDown(_global.left) || Input.GetKeyDown(_global.leftAlt)) { _horizontalInput = -1f; }
			else if (Input.GetKeyDown(_global.right) || Input.GetKeyDown(_global.rightAlt)) { _horizontalInput = 1f; }
			else { _horizontalInput = 0; }

			// vertical
			if (Input.GetKeyDown(_global.down) || Input.GetKeyDown(_global.downAlt)) { _verticalInput = -1f; }
			else if (Input.GetKeyDown(_global.up) || Input.GetKeyDown(_global.upAlt)) { _verticalInput = 1f; }
			else { _verticalInput = 0; }

			// Manage vertical input.
			if (_verticalInput > 0)
			{
				_optionIndex--;
			}
			else if (_verticalInput < 0)
			{
				_optionIndex++;
			}

			// Manage option navigation
			if (_optionIndex <= 1)
			{
				_optionIndex = arrayOfOptions.Count - 1;
			}
			else if (_optionIndex >= arrayOfOptions.Count)
			{
				_optionIndex = 2;
			}

			string crntOptn = arrayOfOptions[_optionIndex].gameObject.name;
			string crntMenu = arrayOfMenus[_menuIndex].name;
			// Navigating the menus.
			if (Input.GetKeyDown(_global.enter))
			{
				if (crntMenu == "Main Menu")
				{
					if (crntOptn == "New Game")
					{
						_global.level = 1;
						_global.UpdatePrefs();
						UnityEngine.SceneManagement.SceneManager.LoadScene(_global.level);
					}
					else if (crntOptn == "Continue")
					{
						UnityEngine.SceneManagement.SceneManager.LoadScene(_global.level);
					}
					else if (crntOptn == "Options")
					{
						arrayOfMenus[_menuIndex].gameObject.SetActive(false);
						_menuIndex = 1;
						_optionIndex = 2;
						UpdateArrayOfOptions();
					}

					else if (crntOptn == "Back")
					{
						isPaused = false;
					}

					else if (crntOptn == "Quit")
					{
						if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex != 0)
						{
							UnityEngine.SceneManagement.SceneManager.LoadScene(0);
						}
						Application.Quit();
					}
				}
				else if (crntMenu == "Options")
				{
					arrayOfMenus[_menuIndex].gameObject.SetActive(false);

					if (crntOptn == "Graphics")
					{
						_menuIndex = 2;
					}

					else if (crntOptn == "Controls")
					{
						_menuIndex = 3;
					}

					else if (crntOptn == "Audio")
					{
						_menuIndex = 4;
					}

					else if (crntOptn == "Back")
					{
						_menuIndex = 0;
					}

					_optionIndex = 2;
					UpdateArrayOfOptions();
				}
				else if (crntMenu == "Graphics" || crntMenu == "Audio")
				{
					if (crntOptn == "Back")
					{
						arrayOfMenus[_menuIndex].gameObject.SetActive(false);
						_menuIndex = 1;
						_optionIndex = 2;
						UpdateArrayOfOptions();
					}
				}

				// Remaping controls
				else if (crntMenu == "Controls")
				{
					if (crntOptn == "left")
					{
						_keyToChange = 0;
						_waitingForInput = true;
					}

					else if (crntOptn == "leftAlt")
					{
						_keyToChange = 1;
						_waitingForInput = true;
					}

					else if (crntOptn == "right")
					{
						_keyToChange = 2;
						_waitingForInput = true;
					}

					else if (crntOptn == "rightAlt")
					{
						_keyToChange = 3;
						_waitingForInput = true;
					}

					else if (crntOptn == "up")
					{
						_keyToChange = 4;
						_waitingForInput = true;
					}

					else if (crntOptn == "upAlt")
					{
						_keyToChange = 5;
						_waitingForInput = true;
					}

					else if (crntOptn == "down")
					{
						_keyToChange = 6;
						_waitingForInput = true;
					}

					else if (crntOptn == "downAlt")
					{
						_keyToChange = 7;
						_waitingForInput = true;
					}

					else if (crntOptn == "restart")
					{
						_keyToChange = 8;
						_waitingForInput = true;
					}

					else if (crntOptn == "menu")
					{
						_keyToChange = 9;
						_waitingForInput = true;
					}

					else if (crntOptn == "quit")
					{
						_keyToChange = 10;
						_waitingForInput = true;
					}

					if (crntOptn == "Back")
					{
						arrayOfMenus[_menuIndex].gameObject.SetActive(false);
						_menuIndex = 1;
						_optionIndex = 2;
						UpdateArrayOfOptions();
					}
				}
			}
			// Changing setting.
			if (crntMenu == "Graphics")
			{
				if (crntOptn == "Pixel Size")
				{
					if (_horizontalInput > 0  && _global.pixelSize < 3)
					{
						_global.pixelSize++;
						_global.UpdatePrefs();
					}
					else if (_horizontalInput < 0 && _global.pixelSize > 1)
					{
						_global.pixelSize--;
						_global.UpdatePrefs();
					}
				}

				else if (crntOptn == "UI Scale")
				{
					if (_horizontalInput > 0  && _global.uiScale < 2)
					{
						_global.uiScale++;
						_global.UpdatePrefs();
					}
					else if (_horizontalInput < 0  && _global.uiScale > 1)
					{
						_global.uiScale--;
						_global.UpdatePrefs();
					}
				}
			}
			else if (crntMenu == "Audio")
			{
				if (crntOptn == "Music Volume")
				{
					if (_horizontalInput > 0  && _global.musicVolume < 10)
					{
						_global.musicVolume++;
						_global.UpdatePrefs();
					}
					else if (_horizontalInput < 0  && _global.musicVolume > 0)
					{
						_global.musicVolume--;
						_global.UpdatePrefs();
					}
				}

				else if (crntOptn == "SFX Volume")
				{
					if (_horizontalInput > 0 && _global.sfxVolume < 10)
					{
						_global.sfxVolume++;
						_global.UpdatePrefs();
					}
					else if (_horizontalInput < 0  && _global.sfxVolume > 0)
					{
						_global.sfxVolume--;
						_global.UpdatePrefs();
					}
				}
			}
			// Draw current menu.
			foreach (var option in arrayOfOptions)
			{
				if (option.gameObject.name != "BG" && option.gameObject.name != "Title")
				{
					option.GetComponent<UnityEngine.UI.Text>().color = _inactiveTextColour;
				}
			}
			arrayOfOptions[_optionIndex].GetComponent<UnityEngine.UI.Text>().color = _activeTextColour;
			arrayOfMenus[_menuIndex].gameObject.SetActive(true);

			// Update settings.
			arrayOfSettings[0].text = "Pixel Size: " + _global.pixelSize;
			arrayOfSettings[1].text = "UI Scale: " + _global.uiScale;
			arrayOfSettings[2].text = "Music Volume: " + _global.musicVolume;
			arrayOfSettings[3].text = "SFX Volume: " + _global.sfxVolume;

			// Updating keys.
			arrayOfKeys[0].text = "Left: "		+ _global.left.ToString();
			arrayOfKeys[1].text = "/ "			+ _global.leftAlt.ToString();
			arrayOfKeys[2].text = "Right: "		+ _global.right.ToString();
			arrayOfKeys[3].text = "/ "			+ _global.rightAlt.ToString();
			arrayOfKeys[4].text = "Up: "		+ _global.up.ToString();
			arrayOfKeys[5].text = "/ "			+ _global.upAlt.ToString();
			arrayOfKeys[6].text = "Down: "		+ _global.down.ToString();
			arrayOfKeys[7].text = "/ "			+ _global.downAlt.ToString();
			arrayOfKeys[8].text = "Restart: "	+ _global.restart.ToString();
			arrayOfKeys[9].text = "Menu: "		+ _global.menu.ToString();
			arrayOfKeys[10].text = "Quit: "		+ _global.quit.ToString();
		}
		else
		{
			Time.timeScale = 1;
			arrayOfMenus[_menuIndex].gameObject.SetActive(false);
			_menuIndex = 0;
			_optionIndex = 2;

			if (Input.GetKeyDown(_global.quit) && UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex != 0)
			{
				UnityEngine.SceneManagement.SceneManager.LoadScene(0);
			}
		}
	}

	private void OnGUI()
	{
		Event e = Event.current;
		if (e.isKey && e.keyCode != KeyCode.Return && e.keyCode != KeyCode.None && _waitingForInput)
		{
			if (_keyToChange == 0)
			{
				_global.left = e.keyCode;
			}

			else if (_keyToChange == 1)
			{
				_global.leftAlt = e.keyCode;
			}

			else if (_keyToChange == 2)
			{
				_global.right = e.keyCode;
			}

			else if (_keyToChange == 3)
			{
				_global.rightAlt = e.keyCode;
			}

			else if (_keyToChange == 4)
			{
				_global.up = e.keyCode;
			}

			else if (_keyToChange == 5)
			{
				_global.upAlt = e.keyCode;
			}

			else if (_keyToChange == 6)
			{
				_global.down = e.keyCode;
			}

			else if (_keyToChange == 7)
			{
				_global.downAlt = e.keyCode;
			}

			else if (_keyToChange == 8)
			{
				_global.restart = e.keyCode;
			}

			else if (_keyToChange == 9)
			{
				_global.menu = e.keyCode;
			}

			else if (_keyToChange == 10)
			{
				_global.quit = e.keyCode;
			}

			_global.UpdatePrefs();
			_waitingForInput = false;
		}
	}
}
