using UnityEngine;
using UnityEngine.PSVita;

public class ExampleRenderTexturePlayback : MonoBehaviour
{
    public string m_MoviePath;
    public RenderTexture m_RenderTexture;
    public GUISkin m_Skin;
	bool m_IsPlaying = false;

    void Start()
    {
        PSVitaVideoPlayer.Init(m_RenderTexture);
        PSVitaVideoPlayer.Play(m_MoviePath, PSVitaVideoPlayer.Looping.Continuous, PSVitaVideoPlayer.Mode.RenderToTexture);
    }

    void OnPreRender()
    {
        PSVitaVideoPlayer.Update();
    }

    void OnGUI()
    {
        GUI.skin = m_Skin;
        GUILayout.BeginArea(new Rect(10,10,200,Screen.height));
        if (GUILayout.Button("Stop/Play"))
        {
			if (m_IsPlaying)
			{
				PSVitaVideoPlayer.Stop();
			}
			else
			{
                PSVitaVideoPlayer.Init(m_RenderTexture);
				PSVitaVideoPlayer.Play(m_MoviePath, PSVitaVideoPlayer.Looping.Continuous, PSVitaVideoPlayer.Mode.RenderToTexture);
			}
        }
        GUILayout.EndArea();
    }

	void OnMovieEvent(int eventID)
	{
		PSVitaVideoPlayer.MovieEvent movieEvent = (PSVitaVideoPlayer.MovieEvent)eventID;
		switch (movieEvent)
		{
			case PSVitaVideoPlayer.MovieEvent.PLAY:
				m_IsPlaying = true;
				break;

			case PSVitaVideoPlayer.MovieEvent.STOP:
				m_IsPlaying = false;
				break;
		}
	}
}
