/*
Project:	Duo Chroma
Developer:	Justinas Grigas - https://mail.google.com/mail/u/0/?view=cm&fs=1&tf=1&to=jgrigas@elam.co.uk
Version:	0.2.0
Date:		10/04/2018 20:44
*/

using UnityEngine;

public class PixelPerfectCanvasScaler : MonoBehaviour
{
	// Private variables.
	private Canvas _canvas;
	private GlobalVarables _global;
	// Monobehaviour functions.

	private void Awake()
	{
		_canvas = GetComponent<Canvas>();
		_global = GameObject.Find("Persistent").GetComponent<GlobalVarables>();
	}

	private void Update()
	{
		_canvas.scaleFactor = _global.pixelSize * _global.uiScale;
	}
}