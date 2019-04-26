using UnityEngine;

public class TapToPlaceParent : MonoBehaviour
{
	bool placing = false;

	// Linked with GazeGestureManager when the user performs a "Select gesture"
	void OnSelect()
	{
		// On each Select gesture, toggle whether the user is in placing mode.
		placing = !placing;

		// If the user is in placing mode, display the spatial mapping mesh.
		if (placing)
		{
			SpatialMapping.Instance.DrawVisualMeshes = true;
		}
		// If the user is not in placing mode, hide the spatial mapping mesh.
		else
		{
			SpatialMapping.Instance.DrawVisualMeshes = false;
		}
	}

	void Update()
	{
		// If the user is in placing mode, it updates the placement to match the user's gaze.
		if (placing)
		{
			// Do a raycast (line between) into the world that will only hit the Spatial Mapping mesh.
			// That is, by the gazeDirection and the headPosition the rayscast detects and do the Spatial Mapping 
			var headPosition = Camera.main.transform.position;
			var gazeDirection = Camera.main.transform.forward;

			RaycastHit hitInfo;
			if (Physics.Raycast(headPosition, gazeDirection, out hitInfo,
				30.0f, SpatialMapping.PhysicsRaycastMask))
			{
				// Here it updates the raycast when it hits the Spatial Mapping mesh by the camera.
				this.transform.parent.position = hitInfo.point;

				// Rotate the object's parent object to face the user.
				Quaternion toQuat = Camera.main.transform.localRotation;
				toQuat.x = 0;
				toQuat.z = 0;
				this.transform.parent.rotation = toQuat;
			}
		}
	}
}
