using UnityEngine;
using UnityEngine.Windows.Speech;

public class VoiceMenu : MonoBehaviour
{
	public string[] keywords = { "play" };
	public ConfidenceLevel confidence = ConfidenceLevel.High;
	protected string word = "play";
	[SerializeField] private SaveManager saveManager;
	[SerializeField] private SceneController sceneController;
	KeywordRecognizer recognizer;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start() {
		recognizer = new KeywordRecognizer(keywords, confidence);
		recognizer.OnPhraseRecognized += Recognizer_OnPhraseRecognized;
		recognizer.Start();
	}

	private void Recognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args) {
		if (args.text == "play") {
			saveManager.IncreaseAttempts();
			sceneController.SceneChange("Test");
		}
	}

	private void OnApplicationQuit() {
		if (recognizer != null && recognizer.IsRunning) {
			recognizer.OnPhraseRecognized -= Recognizer_OnPhraseRecognized;
			recognizer.Stop();
		}
	}
}
