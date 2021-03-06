using UnityEngine;

internal sealed class ScriptTemplate : UnityEditor.AssetModificationProcessor
{
	private const string _PROJECT = "Duo Chroma";
	private const string _VERSION = "0.2.0";
	private const string _DEVELOPER = "Justinas Grigas - https://mail.google.com/mail/u/0/?view=cm&fs=1&tf=1&to=jgrigas@elam.co.uk";
	private const string _COMPANY = "Sigma Games";
	private const string _WEBSITE = "https://sigsec.github.io";

	// UNITY DOCS: This is called by Unity when it is about to create an asset not imported by the user, eg. ".meta" files.
	public static void OnWillCreateAsset(string path)
	{
		// The path looks like this when created "Assets/ExampleScript.cs.meta"
		// So our first job is to remove the ".meta " part from the path
		path = path.Replace(".meta", "");

		// Find the index of '.' before extension, in what index the extension starts?
		var index = path.LastIndexOf(".");
		// If it does not contain a '.' character after removing the ".meta", return, it's not what we are looking for
		if (index == -1) return;

		// Get the substring after '.' using the above extension index (get file extension)
		var file = path.Substring(index);

		// Now check the extension we have to determine if it's a script file, if not, do nothing
		if (file != ".cs" && file != ".js" && file != ".boo") return;

		// "Application.dataPath" gives us "<path to project folder>/Assets"
		// We find the start index of the "Assets" folder, we will use it to get the full name of the script file we've created
		index = Application.dataPath.LastIndexOf("Assets");

		// Get the absolute path to the created script so we can feed it into a ReadAllText (see the next code line)
		// Before this, the path is "Assets/ExampleScript.cs"
		// It becomes, "DRIVE LETTER:/Projects/YourProject/src/Assets/ExampleScript.cs" in my case, i.e. becomes absolute
		path = Application.dataPath.Substring(0, index) + path;

		// Read all the text the script contains into a string
		// MSDN: Opens a text file, reads all lines of the file, and then closes the file.
		file = System.IO.File.ReadAllText(path);

		// Now we replace any amount of custom keywords we want. These should match the ones in your default script template, otherwise it's pointless
		file = file.Replace("#CREATIONDATE#", System.DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
		file = file.Replace("#PROJECTNAME#", _PROJECT);
		file = file.Replace("#DEVELOPERNAME#", _DEVELOPER);
		file = file.Replace("#COMPANY#", _COMPANY);
		file = file.Replace("#VERSION#", _VERSION);

		// We read the script into a string, changed our keywords, now we write the modified version back into the script file
		System.IO.File.WriteAllText(path, file);
	}
}