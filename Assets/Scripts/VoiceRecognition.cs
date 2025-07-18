using UnityEngine;
using UnityEngine.Windows.Speech;

public class VoiceRecognition : MonoBehaviour
{
	public string[] keywords = { "boom", "hello" };
	public ConfidenceLevel confidence = ConfidenceLevel.High;
	protected string word = "boom";
	KeywordRecognizer recognizer;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start() {
		recognizer = new KeywordRecognizer(keywords, confidence);
		recognizer.OnPhraseRecognized += Recognizer_OnPhraseRecognized;
		recognizer.Start();
	}

	private void Recognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args) {
		Debug.Log(args.text);
	}

	private void OnApplicationQuit() {
		if (recognizer != null && recognizer.IsRunning) {
			recognizer.OnPhraseRecognized -= Recognizer_OnPhraseRecognized;
			recognizer.Stop();
		}
	}
}
