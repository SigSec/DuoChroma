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

	public bool isPaused;

	private GlobalVarables _global;
	private GravityManager _gManager;
	private List<GameObject> arrayOfOptions = new List<GameObject>();

	private int _menuIndex = 0;
	private int _optionIndex = 2;

	private float _inputWaitTime = 0.2f;
	private float _currentInputWait;

	private Color _inactiveTextColour;
	private Color _activeTextColour;

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

		// Makes it so that you don't have to wait for the input pause at the start of the game.
		_currentInputWait = _inputWaitTime;
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

		if (Input.GetKeyDown(_global.menu))
		{
			isPaused = !isPaused;
		}

		if (isPaused)
		{
			UpdateArrayOfOptions();

			// Get input.
			float _horizontalInput = Input.GetAxisRaw("Horizontal");
			float _verticalInput = Input.GetAxisRaw("Vertical");

			// Manage vertical input.
			if (_verticalInput > 0 && _currentInputWait >= _inputWaitTime)
			{
				_optionIndex--;
				_currentInputWait = 0;
			}
			else if (_verticalInput < 0 && _currentInputWait >= _inputWaitTime)
			{
				_optionIndex++;
				_currentInputWait = 0;
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
			if (Input.GetKeyDown(KeyCode.Return))
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
					/* Need to add an option to remap, and reset controls.
					else if (crntOptn == "Controls")
					{
						_menuIndex = 3;
					}
					*/
					else if (crntOptn == "Audio")
					{
						_menuIndex = 3;
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
			}
			// Changing setting.
			if (crntMenu == "Graphics")
			{
				if (crntOptn == "Pixel Size")
				{
					if (_horizontalInput > 0 && _currentInputWait >= _inputWaitTime && _global.pixelSize < 3)
					{
						_global.pixelSize++;
						_global.UpdatePrefs();
						_currentInputWait = 0;
					}
					else if (_horizontalInput < 0 && _currentInputWait >= _inputWaitTime && _global.pixelSize > 1)
					{
						_global.pixelSize--;
						_global.UpdatePrefs();
						_currentInputWait = 0;
					}
				}

				else if (crntOptn == "UI Scale")
				{
					if (_horizontalInput > 0 && _currentInputWait >= _inputWaitTime && _global.uiScale < 2)
					{
						_global.uiScale++;
						_global.UpdatePrefs();
						_currentInputWait = 0;
					}
					else if (_horizontalInput < 0 && _currentInputWait >= _inputWaitTime && _global.uiScale > 1)
					{
						_global.uiScale--;
						_global.UpdatePrefs();
						_currentInputWait = 0;
					}
				}
			}
			else if (crntMenu == "Audio")
			{
				if (crntOptn == "Music Volume")
				{
					if (_horizontalInput > 0 && _currentInputWait >= _inputWaitTime && _global.musicVolume < 10)
					{
						_global.musicVolume++;
						_global.UpdatePrefs();
						_currentInputWait = 0;
					}
					else if (_horizontalInput < 0 && _currentInputWait >= _inputWaitTime && _global.musicVolume > 0)
					{
						_global.musicVolume--;
						_global.UpdatePrefs();
						_currentInputWait = 0;
					}
				}

				else if (crntOptn == "SFX Volume")
				{
					if (_horizontalInput > 0 && _currentInputWait >= _inputWaitTime && _global.sfxVolume < 10)
					{
						_global.sfxVolume++;
						_global.UpdatePrefs();
						_currentInputWait = 0;
					}
					else if (_horizontalInput < 0 && _currentInputWait >= _inputWaitTime && _global.sfxVolume > 0)
					{
						_global.sfxVolume--;
						_global.UpdatePrefs();
						_currentInputWait = 0;
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

			// Update timer.
			_currentInputWait += Time.deltaTime;
		}
		else
		{
			arrayOfMenus[_menuIndex].gameObject.SetActive(false);
			_menuIndex = 0;
			_optionIndex = 2;
		}
	}
}
