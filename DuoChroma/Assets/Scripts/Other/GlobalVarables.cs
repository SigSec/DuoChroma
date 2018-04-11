/*
Project:	Duo Chroma
Developer:	Justinas Grigas - https://mail.google.com/mail/u/0/?view=cm&fs=1&tf=1&to=jgrigas@elam.co.uk
Version:	0.2.0
Date:		10/04/2018 19:12
*/

using System.Collections.Generic;
using UnityEngine;

public class GlobalVarables : MonoBehaviour
{
	// Public variables
	public static GlobalVarables self;

	[HideInInspector] public int pixelSize;
	[HideInInspector] public int uiScale;

	[HideInInspector] public int musicVolume;
	[HideInInspector] public int sfxVolume;

	[HideInInspector] public int level = 1;

	// Key inputs.
	[HideInInspector] public List<KeyCode> keys = new List<KeyCode>();

	[HideInInspector] public KeyCode left;
	[HideInInspector] public KeyCode leftAlt;
	[HideInInspector] public KeyCode right;
	[HideInInspector] public KeyCode rightAlt;
	[HideInInspector] public KeyCode up;
	[HideInInspector] public KeyCode upAlt;
	[HideInInspector] public KeyCode down;
	[HideInInspector] public KeyCode downAlt;
	[HideInInspector] public KeyCode restart;
	[HideInInspector] public KeyCode menu;
	[HideInInspector] public KeyCode quit;
	[HideInInspector] public KeyCode enter;

	private void Awake()
	{
		// Always keep the cursor hidden.
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;

		// Check if the prefrences exit, otherwise write them.
		if (!PlayerPrefs.HasKey("pixelSize")) {pixelSize = 2; }
		else { pixelSize = PlayerPrefs.GetInt("pixelSize");}

		if (!PlayerPrefs.HasKey("uiScale")) { uiScale = 2;}
		else { uiScale = PlayerPrefs.GetInt("uiScale"); }

		if (!PlayerPrefs.HasKey("musicVolume")) { musicVolume = 10; }
		else { musicVolume = PlayerPrefs.GetInt("musicVolume"); }

		if (!PlayerPrefs.HasKey("sfxVolume")) { sfxVolume = 10;}
		else { sfxVolume = PlayerPrefs.GetInt("sfxVolume"); }

		if (!PlayerPrefs.HasKey("level")) { level = 1;}
		else { level = PlayerPrefs.GetInt("level"); }

		if (!PlayerPrefs.HasKey("left")) { left = KeyCode.A; }
		else { left = (KeyCode)PlayerPrefs.GetInt("left"); }

		if (!PlayerPrefs.HasKey("leftAlt")) { leftAlt = KeyCode.LeftArrow; }
		else { leftAlt = (KeyCode)PlayerPrefs.GetInt("leftAlt"); }

		if (!PlayerPrefs.HasKey("right")) { right = KeyCode.D; }
		else { right = (KeyCode)PlayerPrefs.GetInt("right"); }

		if (!PlayerPrefs.HasKey("rightAlt")) { rightAlt = KeyCode.RightArrow; }
		else { rightAlt = (KeyCode)PlayerPrefs.GetInt("rightAlt"); }

		if (!PlayerPrefs.HasKey("up")) { up = KeyCode.W; }
		else { up = (KeyCode)PlayerPrefs.GetInt("up"); }

		if (!PlayerPrefs.HasKey("upAlt")) { upAlt = KeyCode.UpArrow; }
		else { upAlt = (KeyCode)PlayerPrefs.GetInt("upAlt"); }

		if (!PlayerPrefs.HasKey("down")) { down = KeyCode.S; }
		else { down = (KeyCode)PlayerPrefs.GetInt("down"); }

		if (!PlayerPrefs.HasKey("downAlt")) { downAlt = KeyCode.DownArrow; }
		else { downAlt = (KeyCode)PlayerPrefs.GetInt("downAlt"); }

		if (!PlayerPrefs.HasKey("restart")) { restart = KeyCode.R; }
		else { restart = (KeyCode)PlayerPrefs.GetInt("restart"); }

		if (!PlayerPrefs.HasKey("menu")) { menu = KeyCode.P; }
		else { menu = (KeyCode)PlayerPrefs.GetInt("menu"); }

		if(!PlayerPrefs.HasKey("quit")) { quit = KeyCode.Escape; }
		else { quit = (KeyCode)PlayerPrefs.GetInt("quit"); }

		if(!PlayerPrefs.HasKey("enter")) { enter = KeyCode.Return; }
		else { enter = (KeyCode)PlayerPrefs.GetInt("enter"); }

		UpdatePrefs();

		// Update keys so that they can be remapped.
		keys.Add(left);
		keys.Add(leftAlt);
		keys.Add(right);
		keys.Add(rightAlt);
		keys.Add(up);
		keys.Add(upAlt);
		keys.Add(down);
		keys.Add(downAlt);
		keys.Add(restart);
		keys.Add(menu);
		keys.Add(quit);

		// Singleton class.
		if (self == null)
		{
			DontDestroyOnLoad(gameObject);
			self = this;
		}
		else if (self != this)
		{
			Destroy(gameObject);
		}
	}

	public void UpdatePrefs()
	{
		PlayerPrefs.SetInt("pixelSize", pixelSize);
		PlayerPrefs.SetInt("uiScale", uiScale);
		PlayerPrefs.SetInt("musicVolume", musicVolume);
		PlayerPrefs.SetInt("sfxVolume", sfxVolume);
		PlayerPrefs.SetInt("level", level);

		PlayerPrefs.SetInt("left", (int)left);
		PlayerPrefs.SetInt("leftAlt", (int)leftAlt);
		PlayerPrefs.SetInt("right", (int)right);
		PlayerPrefs.SetInt("rightAlt", (int)rightAlt);
		PlayerPrefs.SetInt("up", (int)up);
		PlayerPrefs.SetInt("upAlt", (int)upAlt);
		PlayerPrefs.SetInt("down", (int)down);
		PlayerPrefs.SetInt("downAlt", (int)downAlt);
		PlayerPrefs.SetInt("restart", (int)restart);
		PlayerPrefs.SetInt("menu", (int)menu);
		PlayerPrefs.SetInt("quit", (int)quit);
		PlayerPrefs.SetInt("enter", (int)enter);

		PlayerPrefs.Save();
	}
}
