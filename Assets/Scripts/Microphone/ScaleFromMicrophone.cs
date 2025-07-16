using UnityEngine;

public class ScaleFromMicrophone : MonoBehaviour
{
    public enum Mode
    {
        Normal,
        Scaling //only if you want the player to scale when using mic
    }

    [Header("Changable stuff")]
    [Tooltip("Select whether the object moves or scales based on voice.")]
    public Mode result;

    [Range(1, 300)]
    [Tooltip("How sensitive the microphone is to sound. Higher values make it more responsive to quieter sounds.")]
    public float loudnessSensibility = 100;

    [Range(0.1f, 10f)]
    [Tooltip("Minimum loudness required to trigger a response. Values below this are ignored.")]
    public float threshold = 0.1f;

    [Header("For Scaling Mode")]
    public Vector2 minScale;
    public Vector2 maxScale;

    [Header("Misc")]
    [SerializeField] private AudioLoudnessDetection detector;
    public float loudness;
    [Header("For Shooting Test")]
    public float normalizedLoudness;

    // Update is called once per frame
    void Update()
    {
        loudness = detector.GetMicrophoneLoudness() * loudnessSensibility;

        if (loudness < threshold)
        {
            loudness = 0;
        }

        normalizedLoudness = Mathf.InverseLerp(0, loudnessSensibility, loudness);

        if (result == Mode.Scaling)
        {
            transform.localScale = Vector2.Lerp(minScale, maxScale, normalizedLoudness);
        }
    }
}
