/*
Project:	Duo Chroma
Developer:	Justinas Grigas - https://mail.google.com/mail/u/0/?view=cm&fs=1&tf=1&to=jgrigas@elam.co.uk
Version:	0.2.1
Date:		11/04/2018 13:15
*/

using UnityEngine;

public class TutorialText : MonoBehaviour {
	// Variables
	public UnityEngine.UI.Text[] arrayOfText;
	private GlobalVarables _global;

	private void Awake()
	{
		_global = GameObject.Find("Persistent").GetComponent<GlobalVarables>();
	}

	private void LateUpdate()
	{
		//                            r            g            b           a       hex values
		Color darkRed =		new Color(138f / 255f, 028f / 255f, 053f/ 255f, 1f); // #8a1c35
		Color lightRed =	new Color(209f / 255f, 059f / 255f, 059f/ 255f, 1f); // #d13b3b
		Color darkGreen =	new Color(007f / 255f, 049f / 255f, 054f/ 255f, 1f); // #073136
		Color lightGreen =	new Color(021f / 255f, 087f / 255f, 072f/ 255f, 1f); // #155748

		arrayOfText[0].color = darkRed;
		arrayOfText[0].text = "Press <color=#d13b3b>" + _global.left + "</color> or <color=#d13b3b>" + _global.leftAlt + "</color> to walk left";

		arrayOfText[1].color = darkRed;
		arrayOfText[1].text = "Press <color=#d13b3b>" + _global.right + "</color> or <color=#d13b3b>" + _global.rightAlt + "</color> to walk right";

		arrayOfText[2].color = darkRed;
		arrayOfText[2].text = "Press <color=#d13b3b>" + _global.restart + "</color> to restart the level";

		arrayOfText[3].color = darkRed;
		arrayOfText[3].text = "Press <color=#d13b3b>" + _global.quit + "</color> to quit";

		arrayOfText[4].color = darkRed;
		arrayOfText[4].text = "Press <color=#d13b3b>" + _global.up + "</color> or <color=#d13b3b>" + _global.upAlt + "</color> to jump";

		arrayOfText[5].color = darkRed;
		arrayOfText[5].text = "Press <color=#d13b3b>" + _global.menu + "</color> to open and close the menu";

		arrayOfText[6].color = darkGreen;
		arrayOfText[6].text = "Now press <color=#155748>" + _global.down + "</color> or <color=#155748>" + _global.downAlt + "</color> to jump";

		arrayOfText[7].color = darkGreen;
		arrayOfText[7].text = "You can remap your controls in the <color=#155748> Options </color> menu";
	}
}
