using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuLayout
{
	int width;
	int height;
	int ySpace;
	int x;
	int y;
	GUIStyle style;
    GUIStyle styleSelected;
    int selectedItemIndex = 0;
    bool buttonPressed = false;
    bool backButtonPressed = false;
    int numItems = 0;
    int fontSize = 16;
#if !UNITY_PSP2
    int inputFilter = 0;
    static int deBounce = 0;
#endif
    int currCount = 0;
	IScreen owner = null;

	public IScreen GetOwner()
	{
		return owner;
	}

    public MenuLayout(IScreen screen, int itemWidth, int itemFontSize)
	{
		owner = screen;
		numItems = 0;	// itemCount;
        width = itemWidth;
        fontSize = itemFontSize;
	}

    public void DoLayout()
    {
		numItems = currCount;

        style = new GUIStyle(GUI.skin.GetStyle("Button"));
        style.fontSize = fontSize;
        style.alignment = TextAnchor.MiddleCenter;

        styleSelected = new GUIStyle(GUI.skin.GetStyle("Button"));
        styleSelected.fontSize = fontSize + 8;
        styleSelected.alignment = TextAnchor.MiddleCenter;
        
        height = style.fontSize + 16;
        ySpace = 8;
        x = (Screen.width - width) / 2;
        y = (Screen.height - (height + ySpace) * numItems) / 2;

        currCount = 0;
    }

    public void SetSelectedItem(int index)
    {
        if (index < 0)
        {
            selectedItemIndex = 0;
        }

        if (index > numItems - 1)
        {
            selectedItemIndex = numItems - 1;
        }
    }

    public void ItemNext()
    {
        if (numItems > 0)
        {
            selectedItemIndex++;
            if (selectedItemIndex >= numItems)
            {
                selectedItemIndex = 0;
            }
        }
    }

    public void ItemPrev()
    {
        if (numItems > 0)
        {
            selectedItemIndex--;
            if (selectedItemIndex < 0)
            {
                selectedItemIndex = numItems - 1;
            }
        }
    }

    public void Update()
    {
        DoLayout();
        HandleInput();
    }

    public void HandleInput()
    {
#if !UNITY_PSP2
        //A - select
        buttonPressed = false;
        backButtonPressed = false;

        if (Input.GetButtonDown("Fire1"))   // (X)
        {
            if (deBounce <= 0)
            {
                buttonPressed = true;
                deBounce = 20;
                return;
            }
        }

        if (Input.GetButtonDown("Fire2"))   // (O)
        {
            if (deBounce <= 0)
            {
                backButtonPressed = true;
                deBounce = 20;
				return;
            }
        }

        if (deBounce > 0)
        {
            deBounce--;
        }

        // up and down navigation
        float direction = -Input.GetAxis("Vertical");      // Left stick
        bool down = direction > 0.1f;
        bool up = direction < -0.1f;

        if (down && inputFilter == 0)
        {
            ItemNext();
            inputFilter = 20;
        }
        if (up && inputFilter == 0)
        {
            ItemPrev();
            inputFilter = 20;
        }

        // cool down on input, to make navigation easier on high frame rate systems
        if (inputFilter > 0)
        {
            inputFilter--;
        }
#endif
    }
    
    private bool AddButton(string text, bool enabled = true, bool selected = false)
	{
		GUI.enabled = enabled;
        bool ret = GUI.Button(GetRect(), text, selected ? styleSelected : style);
        y += height + ySpace;
        return ret;
	}

    public bool AddItem(string name, bool enabled = true)
    {
        bool clicked = false;
		if (numItems > 0)
		{
			if (AddButton(name, enabled, selectedItemIndex == currCount))
			{
				selectedItemIndex = currCount;
				clicked = true;
			}
			else if (buttonPressed && enabled && selectedItemIndex == currCount)
			{
				clicked = true;
				buttonPressed = false;
			}
		}

        currCount++;

        return clicked;
    }

    public bool AddBackIndex(string name, bool enabled = true)
    {
        bool clicked = false;

		if (numItems > 0)
		{
			if (AddButton(name, enabled, selectedItemIndex == currCount))
			{
				selectedItemIndex = currCount;
				clicked = true;
			}
			else if (buttonPressed && enabled && selectedItemIndex == currCount)
			{
				clicked = true;
				buttonPressed = false;
			}
			else if (backButtonPressed && enabled)
			{
				clicked = true;
				backButtonPressed = false;
			}
		}

        currCount++;

        return clicked;
    }
    
    public Rect GetRect()
	{
		return new Rect(x, y, width, height);
	}
};

