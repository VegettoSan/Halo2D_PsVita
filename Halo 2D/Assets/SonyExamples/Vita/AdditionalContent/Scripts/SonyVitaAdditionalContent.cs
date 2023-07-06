using UnityEngine;
using UnityEngine.PSVita;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class SonyVitaAdditionalContent : MonoBehaviour, IScreen
{
	MenuStack menuStack = null;
	MenuLayout menuMain;

	void Start()
	{
		menuMain = new MenuLayout(this, 450, 34);
		menuStack = new MenuStack();
		menuStack.SetMenu(menuMain);
	}

	void Update()
	{
	}

	void MenuMain()
	{
		menuMain.Update();

		if (menuMain.AddItem("Find Installed Content"))
		{
			EnumerateDRMContent();
		}
	}

	public void Process(MenuStack stack)
	{
		MenuMain();
	}

	void OnGUI()
	{
		MenuLayout activeMenu = menuStack.GetMenu();
		activeMenu.GetOwner().Process(menuStack);
	}


	void EnumerateDRMContentFiles(string contentDir)
	{
		PSVitaDRM.ContentOpen(contentDir);

		string filePath = "addcont0:/" + contentDir;

		OnScreenLog.Add("Found content folder: " + filePath);
		string[] files = Directory.GetFiles(filePath);
		OnScreenLog.Add(" containing " + files.Length + " files");
		foreach (string file in files)
		{
			OnScreenLog.Add("  " + file);
			if (file.Contains(".unity3d"))
			{
				AssetBundle bundle = AssetBundle.LoadFromFile(file);
				if (bundle)
				{
					Object[] assets = bundle.LoadAllAssets();
					OnScreenLog.Add("  Loaded " + assets.Length + " assets from asset bundle.");

					bundle.Unload(false);
				}
			}
		}

		PSVitaDRM.ContentClose(contentDir);
	}

	void EnumerateDRMContent()
	{
		PSVitaDRM.DrmContentFinder finder = new PSVitaDRM.DrmContentFinder();
		finder.dirHandle = -1;
		bool found = false;
		if (PSVitaDRM.ContentFinderOpen(ref finder))
		{
			found = true;
			EnumerateDRMContentFiles(finder.contentDir);
			while (PSVitaDRM.ContentFinderNext(ref finder))
			{
				EnumerateDRMContentFiles(finder.contentDir);
			};
			PSVitaDRM.ContentFinderClose(ref finder);
		}
		if (!found)
		{
			OnScreenLog.Add("No content found");
		}
	}

}
