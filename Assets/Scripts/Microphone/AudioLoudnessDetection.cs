using UnityEngine;

public class AudioLoudnessDetection : MonoBehaviour
{
    public int sampleWindow = 64;
    private AudioClip microphoneClip;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MicrophoneToAudioClip();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MicrophoneToAudioClip()
    {
		//get first microphone on device list
		string microphoneName = Microphone.devices[0];
		Debug.Log(Microphone.devices);
        microphoneClip = Microphone.Start(microphoneName, true, 20, AudioSettings.outputSampleRate);
    }

    public float GetMicrophoneLoudness()
    {
        return GetAudioClipLoudness(Microphone.GetPosition(Microphone.devices[0]), microphoneClip);
    }

    public float GetAudioClipLoudness(int clipPosition, AudioClip clip)
    {
        int startPosition = clipPosition - sampleWindow;

        // Guard clause BEFORE calling GetData
        if (startPosition < 0)
        {
            return 0;
        }

        float[] waveData = new float[sampleWindow];
        clip.GetData(waveData, startPosition);

        // Compute loudness
        float totalLoudness = 0;
        for (int i = 0; i < sampleWindow; i++)
        {
            totalLoudness += Mathf.Abs(waveData[i]);
        }

        return totalLoudness / sampleWindow;
    }
}
