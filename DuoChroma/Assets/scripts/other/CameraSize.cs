/*
Project:	Duo Chroma
Developer:	Justinas Grigas - https://mail.google.com/mail/u/0/?view=cm&fs=1&tf=1&to=jgrigas@elam.co.uk
Version:	0.2.0
Date:		10/04/2018 19:12
*/

using UnityEngine;

public class CameraSize : MonoBehaviour
{
	// Private variables.
	private Camera _camera;
	private GlobalVarables _global;

	private float _screenWidth;
	private float _screenHeight;

	// Monobehaviour functions.
	private void Awake()
	{
		// Grabs the camera component from the game object.
		_camera = GetComponent<Camera>();
		_global = GameObject.Find("Persistent").GetComponent<GlobalVarables>();
	}

	private void Update()
	{
		// Derives the current screen dimensions.
		_screenHeight = Mathf.FloorToInt(Screen.height / 18.0f);
		_screenWidth = Mathf.FloorToInt(Screen.width / 32.0f);

		// Picks the smaller screen size to make sure it fits.
		if (_screenHeight < _screenWidth)
		{
			_screenWidth = _screenHeight;
		}
		else
		{
			_screenHeight = _screenWidth;
		}

		_screenHeight *= 18.0f;
		_screenWidth *= 32.0f;

		_camera.orthographicSize = _screenWidth / 355.029585799f / _global.GetComponent<GlobalVarables>().pixelSize;

		// Creates a new camera rectangle and modifies it.
		Rect _rect = _camera.rect;
		_rect.width = _screenWidth / Screen.width;
		_rect.height = _screenHeight / Screen.height;
		_rect.x = Mathf.Round(((1 - _rect.width) / 2) * 5000) / 5000;
		_rect.y = Mathf.Round(((1 - _rect.height) / 2) * 5000) / 5000;
		_camera.rect = _rect;
	}
}