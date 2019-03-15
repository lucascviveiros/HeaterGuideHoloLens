using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class SpeechManager : MonoBehaviour
{
	KeywordRecognizer keywordRecognizer = null;
	Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

	public GameObject watertap;
	// Use this for initialization
	void Start()
	{
		keywords.Add("Reset", () =>
		{
			// Call the OnReset method on every descendant object.
//			this.BroadcastMessage("OnReset");
			watertap.SendMessage("OnReset", SendMessageOptions.DontRequireReceiver);

		});

		keywords.Add("Next Scene", () =>
		{
			//var focusObject = GazeGestureManager.Instance.FocusedObject;
			//focusObject.SendMessage("OnNext", SendMessageOptions.DontRequireReceiver);
			watertap.SendMessage("OnNext", SendMessageOptions.DontRequireReceiver);

		});

		keywords.Add("Back Scene", () =>
		{
			//var focusObject = GazeGestureManager.Instance.FocusedObject;
			//focusObject.SendMessage("OnBack", SendMessageOptions.DontRequireReceiver);
			watertap.SendMessage("OnBack", SendMessageOptions.DontRequireReceiver);

		});

		keywords.Add("Exit", () =>
		{
			//var focusObject = GazeGestureManager.Instance.FocusedObject;
			//focusObject.SendMessage("OnExit", SendMessageOptions.DontRequireReceiver);
			watertap.SendMessage("OnExit", SendMessageOptions.DontRequireReceiver);

		});

		// Tell the KeywordRecognizer about our keywords.
		keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

		// Register a callback for the KeywordRecognizer and start recognizing!
		keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
		keywordRecognizer.Start();
	}

	void Update()
	{
		// Sending commands direct to the object through mouse
		if (Input.GetButtonDown("Fire1"))
		{
			watertap.SendMessage("OnNext", SendMessageOptions.DontRequireReceiver);

		}
		if (Input.GetButtonDown("Fire2"))
		{
			watertap.SendMessage("OnBack", SendMessageOptions.DontRequireReceiver);

		}
	}

		private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
	{
		System.Action keywordAction;
		if (keywords.TryGetValue(args.text, out keywordAction))
		{
			keywordAction.Invoke();
		}
	}
}