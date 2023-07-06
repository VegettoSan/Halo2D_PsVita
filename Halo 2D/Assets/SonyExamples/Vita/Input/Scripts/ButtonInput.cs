using UnityEngine;
using System.Collections;
using UnityEngine.PSVita;

public class ButtonInput : MonoBehaviour
{
    void OnGUI()
    {
        int y = 40;
		
		#region WIRELESS
		GUI.Toggle(new Rect(85, y, 300, 20), PSVitaInput.WirelesslyControlled() != false, " Wireless Controller detected");
		y += 15;
		#endregion
		
        #region SHOULDER BUTTONS
        GUI.Toggle(new Rect(85, y, 300, 20), Input.GetButton("Left Shoulder") != false, " Left Shoulder");
        GUI.Toggle(new Rect(385, y, 300, 20), Input.GetButton("Right Shoulder") != false, " Right Shoulder");
        y += 15;
		
		GUI.Toggle(new Rect(85, y, 300, 20), Input.GetButton("Left Shoulder 1") != false, " Left Shoulder 1 (Vita TV Only)");
        GUI.Toggle(new Rect(385, y, 300, 20), Input.GetButton("Right Shoulder 1") != false, " Right Shoulder 1 (Vita TV Only)");
		y += 15;
		
		GUI.Toggle(new Rect(85, y, 300, 20), Input.GetButton("Left Stick") != false, " Left Stick (Vita TV Only)");
        GUI.Toggle(new Rect(385, y, 300, 20), Input.GetButton("Right Stick") != false, " Right Stick (Vita TV Only)");
		y += 30;
        #endregion

        #region DPAD
        int dPadOffset_X = 80;
        int dPadOffset_Y = y;

        GUI.TextField(new Rect(dPadOffset_X, dPadOffset_Y, 140, 20), " D-Pad");
        GUI.Toggle(new Rect(dPadOffset_X, dPadOffset_Y + 60, 100, 20), Input.GetButton("Dleft") != false, " Left");
        GUI.Toggle(new Rect(dPadOffset_X + 40, dPadOffset_Y + 30, 100, 20), Input.GetButton("Dup") != false, " Up");
        GUI.Toggle(new Rect(dPadOffset_X + 80, dPadOffset_Y + 60, 100, 20), Input.GetButton("Dright") != false, " Right");
        GUI.Toggle(new Rect(dPadOffset_X + 40, dPadOffset_Y + 90, 100, 20), Input.GetButton("Ddown") != false, " Down"); 
        #endregion

		#region MAIN BUTTONS
		int buttonsOffset_X = 380;
		int buttonsOffset_Y = y;

		GUI.TextField(new Rect(buttonsOffset_X, buttonsOffset_Y, 140, 20), " Main Buttons");
		GUI.Toggle(new Rect(buttonsOffset_X, buttonsOffset_Y + 60, 100, 20), Input.GetButton("Square") != false, " Square");
		GUI.Toggle(new Rect(buttonsOffset_X + 40, buttonsOffset_Y + 30, 100, 20), Input.GetButton("Triangle") != false, " Triangle");
		GUI.Toggle(new Rect(buttonsOffset_X + 80, buttonsOffset_Y + 60, 100, 20), Input.GetButton("Circle") != false, " Circle");
		GUI.Toggle(new Rect(buttonsOffset_X + 40, buttonsOffset_Y + 90, 100, 20), Input.GetButton("Cross") != false, " Cross");
		#endregion

        #region START AND SELECT
        int miscOffset_X = 590;
        int miscOffset_Y = y;

        GUI.Toggle(new Rect(miscOffset_X, miscOffset_Y, 140, 20), Input.GetButton("Select") != false, " Select");
        GUI.Toggle(new Rect(miscOffset_X, miscOffset_Y + 20, 140, 20), Input.GetButton("Start") != false, " Start");
        #endregion
        
        #region LEFT STICK
        y += 120;
        int leftStickOffset_X = 50;
        int leftStickOffset_Y = y;

        // Important Note...
        //
        // SCE Technical Requirement TRC R3173: The center value dead zone +-0x20 data of the left stick
        // and right stick of the PlayStation Vita system is not used in the application.
        //
        // This equates to a Unity dead zone value of 0.25 so in order to comply with this requirement you
        // should set 0.25 as the dead values for each stick axis in the Unity input manager.

        GUI.TextField(new Rect(leftStickOffset_X, leftStickOffset_Y, 200, 20), " Left Stick X Axis: " + Input.GetAxis("Left Stick Horizontal"));
        GUI.TextField(new Rect(leftStickOffset_X, leftStickOffset_Y + 25, 200, 20), " Left Stick Y Axis: " + Input.GetAxis("Left Stick Vertical")); 
        #endregion

        #region RIGHT STICK
        int rightStickOffset_X = 350;
        int rightStickOffset_Y = y;

        GUI.TextField(new Rect(rightStickOffset_X, rightStickOffset_Y, 200, 20), " Right Stick X Axis: " + Input.GetAxis("Right Stick Horizontal"));
        GUI.TextField(new Rect(rightStickOffset_X, rightStickOffset_Y + 25, 200, 20), " Right Stick Y Axis: " + Input.GetAxis("Right Stick Vertical")); 
        #endregion
    }

	// Update is called once per frame
	void Update () {
	
	}
}
