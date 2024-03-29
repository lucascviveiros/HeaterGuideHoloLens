﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class SpeechManager : MonoBehaviour
{
	KeywordRecognizer keywordRecognizer = null;
	Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

	public GameObject watertap;
	
	void Start()
	{
		keywords.Add("Reset", () =>
		{
			watertap.SendMessage("OnReset", SendMessageOptions.DontRequireReceiver);
		});

		keywords.Add("Next", () =>
		{
			watertap.SendMessage("OnNext", SendMessageOptions.DontRequireReceiver);
		});

		keywords.Add("Back", () =>
		{
			watertap.SendMessage("OnBack", SendMessageOptions.DontRequireReceiver);
		});

		keywords.Add("Close App", () =>
		{
			watertap.SendMessage("OnExit", SendMessageOptions.DontRequireReceiver);
		});

		keywords.Add("Right", () =>
		{
			watertap.SendMessage("OnRotateRight", SendMessageOptions.DontRequireReceiver);
		});

		keywords.Add("Left", () =>
		{
			watertap.SendMessage("OnRotateLeft", SendMessageOptions.DontRequireReceiver);
		});

		keywords.Add("Bigger", () =>
		{
			watertap.SendMessage("OnBigger", SendMessageOptions.DontRequireReceiver);
		});

		keywords.Add("Smaller", () =>
		{
			watertap.SendMessage("OnSmaller", SendMessageOptions.DontRequireReceiver);
		});

		keywords.Add("Freeze", () =>
		{
			watertap.SendMessage("OnPause", SendMessageOptions.DontRequireReceiver);
		});

		keywords.Add("Play", () =>
		{
			watertap.SendMessage("OnPlay", SendMessageOptions.DontRequireReceiver);
		});

		// KeywordRecognizer and keywords.
		keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

		// Callback for the KeywordRecognizer to all spoken words
		keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
		keywordRecognizer.Start();
	}

	void Update()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			watertap.SendMessage("OnBigger", SendMessageOptions.DontRequireReceiver);

		}
		if (Input.GetButtonDown("Fire2"))
		{
			watertap.SendMessage("OnSmaller", SendMessageOptions.DontRequireReceiver);

		}

		if (Input.GetKeyDown(KeyCode.F1)) {
			watertap.SendMessage("OnNext", SendMessageOptions.DontRequireReceiver);

		}

		if (Input.GetKeyDown(KeyCode.F2))
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
