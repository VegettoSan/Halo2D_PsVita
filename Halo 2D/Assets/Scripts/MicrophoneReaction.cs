using UnityEngine;
using System.Collections;

public class MicrophoneReaction : MonoBehaviour
{
    int sampleRate = 44100;
    int clipLength = 1;
    int position;
    public float sensitivity = 0.00025f;
    public float loudness = 0;
    float analysisWindow = 0.2f;
    AudioClip clip;
    
	void Start ()
    {
        clip = Microphone.Start(null, true, clipLength, sampleRate);
	}
	
	void Update ()
    {
        loudness = Mathf.Clamp(GetAveragedVolume() * sensitivity, 0f, 0.3f);
        transform.localScale = Vector3.ClampMagnitude(new Vector3(0.1f + loudness, 0.1f + loudness, 0.1f + loudness), 1.5f);
	}

    float GetAveragedVolume()
    {
        float[] data = new float[(int)(analysisWindow * sampleRate)];
        float samples = analysisWindow * sampleRate;

        position = Microphone.GetPosition(null);
        if (position < clip.length)
            position += clipLength * sampleRate;

        if (position - data.Length > 0)
            clip.GetData(data, position - data.Length);
        else
            return 0.0f;

        float rms = 0.0f;
        foreach (float level in data)
        {
			rms += level * level;
		}

        if (rms == 0.0f)
        {
            return 0.0f;
        }

		rms = Mathf.Sqrt(rms / clip.length);

        return 0.05f * (1f + Mathf.Log10(rms));
    }
}
