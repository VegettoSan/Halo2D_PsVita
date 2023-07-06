using UnityEngine;
using System.Collections;
using UnityEngine.PSVita;

public class TestMotion : MonoBehaviour
{
    private GUIText gui;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!gui)
        {
            GameObject go = new GameObject("Motion Info");
            go.AddComponent<GUIText>();
            go.hideFlags = HideFlags.HideAndDontSave;
            go.transform.position = new Vector3(0.7f, 0.9f, 0);
            gui = go.GetComponent<GUIText>();
        }

        gui.pixelOffset = new Vector2(0, 0);

        Input.compass.enabled = true;
		PSVitaInput.gyroTiltCorrectionEnabled = false;
		PSVitaInput.gyroDeadbandFilterEnabled = true;

        gui.text = "\nInput";

        gui.text += "\n .deviceOrientation: ";
        gui.text += "\n   " + Input.deviceOrientation;

        gui.text += "\n\nInput.acceleration";
        gui.text += "\n .x,y,z: " + Input.acceleration;
        gui.text += "\n .magnitude: " + Input.acceleration.magnitude;

        gui.text += "\n\nInput.gyro";
        gui.text += "\n .enabled: " + Input.gyro.enabled;
        gui.text += "\n .attitude: " + Input.gyro.attitude;
        gui.text += "\n .gravity: " + Input.gyro.gravity;
        gui.text += "\n .rotationRate: " + Input.gyro.rotationRate;
        gui.text += "\n .rotationRateUnbiased: " + Input.gyro.rotationRateUnbiased;
        gui.text += "\n .updateInterval: " + Input.gyro.updateInterval;
        gui.text += "\n .userAcceleration: " + Input.gyro.userAcceleration;
        gui.text += "\nPSVitaInput.gyroDeadbandFilterEnabled: " + PSVitaInput.gyroDeadbandFilterEnabled;
        gui.text += "\nPSVitaInput.gyroTiltCorrectionEnabled: " + PSVitaInput.gyroTiltCorrectionEnabled;

        gui.text += "\n\nInput.compass";
        gui.text += "\n .enabled: " + Input.compass.enabled;
        gui.text += "\n .magneticHeading: " + Input.compass.magneticHeading;
        gui.text += "\n .trueHeading: " + Input.compass.trueHeading;
        gui.text += "\n .rawVector: " + Input.compass.rawVector;
        gui.text += "\n .timestamp: " + Input.compass.timestamp;
        gui.text += "\n PSVitaInput.compassFieldStability:";
        gui.text += "\n   " + PSVitaInput.compassFieldStability;

        if (PSVitaInput.compassFieldStability != PSVitaInput.CompassStability.CompassVeryStable)
        {
            gui.text += "\nCompass unstable, needs calibration!";
            // In a real app you would probably display the standard compass calibration message here."
        }
    }
}
