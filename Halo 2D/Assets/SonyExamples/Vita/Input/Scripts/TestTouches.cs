using UnityEngine;
using System.Collections;
using UnityEngine.PSVita;

public class TestTouches : MonoBehaviour {

    private GUIText gui;

	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!gui)
		{
			GameObject go = new GameObject("Touch Info");
			//GameObject guiText = UnityEngine.GameObject.Find("GUI Text");
            go.AddComponent<GUIText>();
			go.hideFlags = HideFlags.HideAndDontSave;
			go.transform.position = new Vector3(0.1f, 0.5f, 0);
			gui = go.GetComponent<GUIText>();
			gui.pixelOffset = new Vector2(5,100);
		}

        PSVitaInput.secondaryTouchIsScreenSpace = true;

		gui.text = "\n\n\n\n\n\n\n\nSimulated Mouse\n";
		gui.text += " pos: " + Input.mousePosition.x + ", " + Input.mousePosition.y + "\n";
		for(int i=0; i<3; i++)
		{
			gui.text += " button: " + i;
			gui.text += " held: " + Input.GetMouseButton(i);
			gui.text += " up: " + Input.GetMouseButtonUp(i);
			gui.text += " down: " + Input.GetMouseButtonDown(i);
			gui.text += "\n";
		}
		gui.text += "\nTouch Screen Front";
		gui.text += "\n touchCount: " + Input.touchCount + "\n";
        foreach (Touch touch in Input.touches)
        {
            gui.text += " pos: " + touch.position.x + ", " + touch.position.y;
			gui.text += " mp: " + Input.mousePosition.x + ", " + Input.mousePosition.y;
            gui.text += " fid: " + touch.fingerId;
            gui.text += " dpos: " + touch.deltaPosition;
            gui.text += " dtime: " + touch.deltaTime;
            gui.text += " tapcount: " + touch.tapCount;
            gui.text += " phase: " + touch.phase;
            gui.text += "\n";

            if (touch.phase == TouchPhase.Began)
            {
                print("Began");
            }

            if (touch.phase == TouchPhase.Ended)
            {
                if (touch.tapCount == 2)
                {
                    print("Ended Multitap");
                }
                else
                {
                    print("Ended");
                }
            }
        }

        gui.text += "\nRear Touch Pad";
        gui.text += "\n isScreenSpace: " + PSVitaInput.secondaryTouchIsScreenSpace;
        if (PSVitaInput.secondaryTouchIsScreenSpace == false)
        {
            gui.text += "\n width: " + PSVitaInput.secondaryTouchWidth;
            gui.text += " height: " + PSVitaInput.secondaryTouchHeight;
        }
        gui.text += "\n touchCount: " + PSVitaInput.touchCountSecondary + "\n";
        foreach (Touch touch in PSVitaInput.touchesSecondary)
        {
            gui.text += " pos: " + touch.position.x + ", " + touch.position.y;
            gui.text += " fid: " + touch.fingerId;
            gui.text += " dpos: " + touch.deltaPosition;
            gui.text += " dtime: " + touch.deltaTime;
            gui.text += " tapcount: " + touch.tapCount;
            gui.text += " phase: " + touch.phase;
            gui.text += "\n";
        }
	}
}
