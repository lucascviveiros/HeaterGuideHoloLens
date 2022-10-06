using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class GazeGestureManager : MonoBehaviour
{
	public static GazeGestureManager Instance { get; private set; }
	public GameObject FocusedObject { get; private set; }

	GestureRecognizer recognizer;

	void Awake()
	{
		Instance = this;
		recognizer = new GestureRecognizer();
		recognizer.Tapped += (args) =>
		{
			if (FocusedObject != null)
			{
				FocusedObject.SendMessageUpwards("OnSelect", SendMessageOptions.DontRequireReceiver);
			}
		};
		recognizer.StartCapturingGestures();
	}

	void Update()
	{
		GameObject oldFocusObject = FocusedObject;
		var headPosition = Camera.main.transform.position;
		var gazeDirection = Camera.main.transform.forward;

		RaycastHit hitInfo;
		if (Physics.Raycast(headPosition, gazeDirection, out hitInfo))
		{
			FocusedObject = hitInfo.collider.gameObject;
		}
		else
		{
			FocusedObject = null;
		}

		if (FocusedObject != oldFocusObject)
		{
			recognizer.CancelGestures();
			recognizer.StartCapturingGestures();
		}

	}
}
