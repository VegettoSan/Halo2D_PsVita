using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuStack
{
	MenuLayout activeMenu;
	List<MenuLayout> menuStack = new List<MenuLayout>();

	public void SetMenu(MenuLayout menu)
	{
		menuStack.Clear();
		activeMenu = menu;
	}

	public MenuLayout GetMenu()
	{
		return activeMenu;
	}

	public void PushMenu(MenuLayout menu)
	{
		menuStack.Add(activeMenu);
		activeMenu = menu;
	}

	public void PopMenu()
	{
		if (menuStack.Count > 0)
		{
			activeMenu = menuStack[menuStack.Count - 1];
			menuStack.RemoveAt(menuStack.Count - 1);
		}
	}
};
