using UnityEngine;
using System.Collections.Generic;

public class OnScreenLog : MonoBehaviour
{
	static int msgCount = 0;
	static private List<string> log = new List<string>();
	static int maxLines = 16;
	static int fontSize = 24;
	int frameCount = 0;

	// Use this for initialization
	void Start ()
	{
	}

	// Update is called once per frame
	void Update ()
	{
		frameCount ++;	
	}

	void OnGUI()
	{
		GUIStyle style = GUI.skin.GetStyle("Label");
        style.fontSize = fontSize;
        style.alignment = TextAnchor.UpperLeft;
        style.wordWrap = false;
		
		float height = 0;
		string logText = "";
		foreach(string s in log)
		{
            logText += " " + s;
			logText += "\n";
			height += style.lineHeight;
		}
		height += 6;

        GUI.Label(new Rect(0, 0, Screen.width - 1, height), logText, style);

		height = style.lineHeight + 4;
        GUI.Label(new Rect(Screen.width - 100, Screen.height - 100, Screen.width - 1, height), frameCount.ToString());
	}

	static public void Add(string msg)
	{
		string cleaned = msg.Replace("\r", " ");
		cleaned = cleaned.Replace("\n", " ");

        System.Console.WriteLine("[APP] " + cleaned);

		log.Add(cleaned);
		msgCount ++;

		if(msgCount > maxLines)
		{
			log.RemoveAt(0);
		}
	}
}
