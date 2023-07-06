using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitedFrame : MonoBehaviour
{
    enum Character { Fps30, Fps60, Fps120 }
    [SerializeField]
    Character Fps;

    public int ActualFps, FPS;

    private void Awake()
    {
        ActualFps = PlayerPrefs.GetInt("fps", 30);

        if (ActualFps == 30)
        {
            Fps = Character.Fps30;
        }
        if (ActualFps == 60)
        {
            Fps = Character.Fps60;
        }
        if (ActualFps == 120)
        {
            Fps = Character.Fps120;
        }
    }
    void Start()
    {

    }

    
    void Update()
    {
        FPS = Application.targetFrameRate;
        if (Fps == Character.Fps30)
        {
            fps30();
        }
        if (Fps == Character.Fps60)
        {
            fps60();
        }
        if (Fps == Character.Fps120)
        {
            fps120();
        }
    }

    public void fps60()
    {
        Application.targetFrameRate = 60;
        ActualFps = 60;
        PlayerPrefs.SetInt("fps", ActualFps);
    }

    public void fps30()
    {
        Application.targetFrameRate = 30;
        ActualFps = 30;
        PlayerPrefs.SetInt("fps", ActualFps);
    }

    public void fps120()
    {
        Application.targetFrameRate = 120;
        ActualFps = 120;
        PlayerPrefs.SetInt("fps", ActualFps);
    }
}
