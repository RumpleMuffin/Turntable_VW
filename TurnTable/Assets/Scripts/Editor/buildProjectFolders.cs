/*Directory creation script for quick creation of directories in a new Unity3D or 2D project...
Use :
1. Create a new C# script and save as buildProjectFolders, put in a flder for future use
2. Create a folder in your project called Editor, import or drag script into it
3. Click Edit -> Create Project Folders* as long as there are no build errors, you will see a new menu item near the bootom of the Edit menu
4.  If you want to include a Resources folde, clicking the checkbox will add or remove it
5. If you are using Namespaces, clicking the checkbox will include three basic namespce folders
6.  Right clicking on a list item will let you delete the item, if you want
7.  Increasing the List size will add another item with the prior items name, click in the space to rename.
8.  Clicking "Create" will create all the files listed, the Namespace folders will be added to the script directory.
*/



using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class buildProjectFolders : ScriptableWizard
{

	public bool IncludeResourceFolder = false;
	public bool UseNamespace = false;
	private string SFGUID;
	public List<string> nsFolders = new List<string>();
	public List<string> folders = new List<string>() { "Scenes", "Scripts", "Animation", "Audio", "Materials", "Models", "Prefabs", "Artwork", "Textures", "Sprites" };
	[MenuItem("Edit/Create Project Folders...")]
	static void CreateWizard()
	{
		ScriptableWizard.DisplayWizard("Create Project Folders", typeof(buildProjectFolders), "Create");
	}

	//Called when the window first appears
	void OnEnable()
	{

	}
	//Create button click
	void OnWizardCreate()
	{

		//create all the folders required in a project
		//primary and sub folders
		foreach (string folder in folders)
		{
			string guid = AssetDatabase.CreateFolder("Assets", folder);
			string newFolderPath = AssetDatabase.GUIDToAssetPath(guid);
			if (folder == "Scripts")
				SFGUID = newFolderPath;
		}

		AssetDatabase.Refresh();
		if (UseNamespace == true)
		{
			foreach (string nsf in nsFolders)
			{
				//AssetDatabase.Contain
				string guid = AssetDatabase.CreateFolder("Assets/Scripts", nsf);
				string newFolderPath = AssetDatabase.GUIDToAssetPath(guid);

			}
		}
	}
	//Runs whenever something changes in the editor window...
	void OnWizardUpdate()
	{
		if (IncludeResourceFolder == true && !folders.Contains("Resources"))
			folders.Add("Resources");
		if (IncludeResourceFolder == false && folders.Contains("Resources"))
			folders.Remove("Resources");

		if (UseNamespace == true)
			addNamespaceFolders();
		if (UseNamespace == false)
			removeNamespceFolders();

	}
	void addNamespaceFolders()
	{


		if (!nsFolders.Contains("Interfaces"))
			nsFolders.Add("Interfaces");

		if (!nsFolders.Contains("Classes"))
			nsFolders.Add("Classes");


		if (!nsFolders.Contains("States"))
			nsFolders.Add("States");

		// (nsFolders);
	}

	void removeNamespceFolders()
	{
		if (nsFolders.Count > 0) nsFolders.Clear();
	}
}
