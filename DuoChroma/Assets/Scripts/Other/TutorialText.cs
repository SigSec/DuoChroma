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

	private string[] arrayOfActiveColours;

	private void Awake()
	{
		_global = GameObject.Find("Persistent").GetComponent<GlobalVarables>();

		arrayOfActiveColours = new string[arrayOfText.Length];

		// Set the colours for each text box to depend on its vertical position
		for (int i = 0; i < arrayOfText.Length; i++)
		{
			if (arrayOfText[i].gameObject.GetComponent<RectTransform>().anchoredPosition.y < 750)
			{
				arrayOfText[i].color = new Color(138f / 255f, 028f / 255f, 053f / 255f, 1f);
				arrayOfActiveColours[i] = "#d13b3b";
			}

			else
			{
				arrayOfText[i].color = new Color(007f / 255f, 049f / 255f, 054f / 255f, 1f);
				arrayOfActiveColours[i] = "#155748";
			}
		}
	}

	private void LateUpdate()
	{
		arrayOfText[0].text = "Press <color=" + arrayOfActiveColours[0] + ">" + _global.left + "</color> or <color=" + arrayOfActiveColours[0] + ">" + _global.leftAlt + "</color> to walk left";
		arrayOfText[1].text = "Press <color=" + arrayOfActiveColours[1] + ">" + _global.right + "</color> or <color=" + arrayOfActiveColours[1] + ">" + _global.rightAlt + "</color> to walk right";
		arrayOfText[2].text = "Press <color=" + arrayOfActiveColours[2] + ">" + _global.restart + "</color> to restart the level";
		arrayOfText[3].text = "Press <color=" + arrayOfActiveColours[3] + ">" + _global.quit + "</color> to quit";
		arrayOfText[4].text = "Press <color=" + arrayOfActiveColours[4] + ">" + _global.up + "</color> or <color=" + arrayOfActiveColours[4] + ">" + _global.upAlt + "</color> to jump";
		arrayOfText[5].text = "Press <color=" + arrayOfActiveColours[5] + ">" + _global.menu + "</color> to open and close the menu";
		arrayOfText[6].text = "Now press <color=" + arrayOfActiveColours[6] + ">" + _global.down + "</color> or <color=" + arrayOfActiveColours[6] + ">" + _global.downAlt + "</color> to jump";
		arrayOfText[7].text = "You can remap your controls in the <color=" + arrayOfActiveColours[7] + ">Options</color> menu";
		arrayOfText[8].text = "...One side is getting darker, while the other brighter, for there must be <color=" + arrayOfActiveColours[8] + ">balance</color>";
		arrayOfText[9].text = "...Gems <color=" + arrayOfActiveColours[9] + ">brighten</color> up the side you are on";
		arrayOfText[10].text = "<color=" + arrayOfActiveColours[10] + ">Keys</color> open doors on either side";
		arrayOfText[11].text = "<color=" + arrayOfActiveColours[11] + ">Doors</color> can be opened as long as you have at least one <color=" + arrayOfActiveColours[11] + ">Key</color>";
		arrayOfText[12].text = "...<color=" + arrayOfActiveColours[12] + ">Duo Gems</color> mark the end of the level. Find all of them to restore the <color=" + arrayOfActiveColours[12] + ">Balance</color>...";
	}
}
