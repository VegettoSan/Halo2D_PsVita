using UnityEngine;
using System.Collections;
using UnityEngine.PSVita;

public class WebCamSetup : MonoBehaviour
{
    int outputX = Screen.width - 240;
    int activeWebcam = 0;
    int activeEffect = 0;
    bool aeLock = false;
    PSVitaCamera.Device[] cameras = { PSVitaCamera.Device.Front, PSVitaCamera.Device.Back };
    PSVitaCamera.Effect[] effects = { PSVitaCamera.Effect.Normal, PSVitaCamera.Effect.Bw, PSVitaCamera.Effect.Bluish, PSVitaCamera.Effect.Greenish, PSVitaCamera.Effect.Reddish, PSVitaCamera.Effect.Sepia, PSVitaCamera.Effect.Nega };
    WebCamTexture[] webCams = { null, null };
    public GUISkin skin;
    int requestWidth = 640;
    int requestHeight = 460;

	void Start () 
	{
        WebCamDevice[] devices = WebCamTexture.devices;
        if (devices.Length > 0)
        {
            webCams[0] = new WebCamTexture(devices[0].name);
            webCams[0].requestedFPS = 30;
            webCams[0].requestedWidth = requestWidth;
            webCams[0].requestedHeight = requestHeight;
            webCams[0].Play();
            GetComponent<Renderer>().material.mainTexture = webCams[0];
            activeWebcam = 0;

            // Set object scales to match the actual aspect ratio of the camera.
            transform.localScale = new Vector3(webCams[0].width / 1000.0f, 1.0f, webCams[0].height / 1000.0f);
            GameObject.Find("Captured").transform.localScale = new Vector3(webCams[0].width / 2000.0f, 1.0f, webCams[0].height / 2000.0f);

            PSVitaCamera.SetReverse(cameras[activeWebcam], PSVitaCamera.Reverse.flip);
            PSVitaCamera.SetAntiFlicker(cameras[activeWebcam], PSVitaCamera.AntiFlicker.hz50);
        }

        if (devices.Length > 1)
        {
            webCams[1] = new WebCamTexture(devices[1].name);
            webCams[1].requestedFPS = 30;
            webCams[1].requestedWidth = requestWidth;
            webCams[1].requestedHeight = requestHeight;

            PSVitaCamera.SetReverse(PSVitaCamera.Device.Back, PSVitaCamera.Reverse.flip);
            PSVitaCamera.SetAntiFlicker(PSVitaCamera.Device.Back, PSVitaCamera.AntiFlicker.hz50);
        }
    }

	void Update () 
	{
	}

    void OnGUI()
    {
        int y = 0;
        int bh = 60;
        int yStep = bh + 5;

        GUI.skin = skin;

        if (GUI.Button(new Rect(0, y, 100, bh), (activeWebcam == 0) ? "Select Back" : "Select Front"))
        {
            webCams[activeWebcam].Stop();
            activeWebcam = activeWebcam == 0 ? 1 : 0;
            GetComponent<Renderer>().material.mainTexture = webCams[activeWebcam];
            webCams[activeWebcam].Play();
        }
        y += yStep;
        
        if (GUI.Button(new Rect(0, y, 100, bh), "Stop/Play"))
        {
            switch (webCams[activeWebcam].isPlaying)
            {
                case true:
                    webCams[activeWebcam].Stop();
                    break;
                default:
                    webCams[activeWebcam].Play();
                    break;
            }            
        }
        y += yStep;

        if (GUI.Button(new Rect(0, y, 100, bh), "Pause"))
        {
            webCams[activeWebcam].Pause();
        }
        y += yStep;

        if (GUI.Button(new Rect(0, y, 100, bh), "Effect: " + effects[activeEffect]))
        {
            activeEffect ++;
            if(activeEffect >= effects.Length)
            {
                activeEffect = 0;
            }
            PSVitaCamera.SetEffect(cameras[activeWebcam], effects[activeEffect]);
        }
        y += yStep;

        if (GUI.Button(new Rect(0, y, 100, bh), aeLock ? "AE Unlock" : "AE Lock"))
        {
            aeLock = !aeLock;
            PSVitaCamera.SetAutoControlHold(cameras[activeWebcam], aeLock);
        }
        y += yStep;

        y += yStep;
        if (GUI.Button(new Rect(0, y, 100, bh), "Capture"))
        {
            PSVitaCamera.DoShutterSound(PSVitaCamera.ShutterSound.IMAGE);

            Color32[] pixels = webCams[activeWebcam].GetPixels32();
            Texture2D captureTex = new Texture2D(webCams[activeWebcam].width, webCams[activeWebcam].height, TextureFormat.ARGB32, false);
            captureTex.SetPixels32(pixels);
            captureTex.Apply();

            GameObject.Find("Captured").GetComponent<Renderer>().material.mainTexture = captureTex;
        }
        y += yStep;

        GUI.BeginGroup(new Rect(outputX, 10, 240, Screen.height));
        if (webCams[activeWebcam] != null)
        {
            GUILayout.Label("DeviceName: " + webCams[activeWebcam].deviceName.ToString());
            GUILayout.Label("resolution: " + webCams[activeWebcam].width + " x " + webCams[activeWebcam].height);
            GUILayout.Label("isPlaying: " + webCams[activeWebcam].isPlaying.ToString());
            GUILayout.Label("requestedFPS: " + webCams[activeWebcam].requestedFPS.ToString());
            GUILayout.Label("Effect: " + PSVitaCamera.GetEffect(cameras[activeWebcam]));
            GUILayout.Label("AE Lock: " + PSVitaCamera.GetAutoControlHold(cameras[activeWebcam]));
            GUILayout.Label("Reverse: " + PSVitaCamera.GetReverse(cameras[activeWebcam]));
        }
        GUI.EndGroup();
    }

}
