﻿using UnityEngine;

public class WorldCursor : MonoBehaviour
{
	private MeshRenderer meshRenderer;

	void Start()
	{
		// Grab the mesh renderer that's on the same object as this script.
		meshRenderer = this.gameObject.GetComponentInChildren<MeshRenderer>();
	}

	void Update()
	{
		// Do a raycast into the world based on the user's head position and orientation.
		var headPosition = Camera.main.transform.position;
		var gazeDirection = Camera.main.transform.forward;

		RaycastHit hitInfo;

		if (Physics.Raycast(headPosition, gazeDirection, out hitInfo))
		{
			// If the raycast hit a hologram Display the cursor mesh.
			meshRenderer.enabled = true;

			// Move the cursor to the point where the raycast hit.
			this.transform.position = hitInfo.point;

			// Rotate the cursor to hug the surface of the hologram.
			this.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
		}
		else
		{
			// If the raycast did not hit a hologram (3D Object), hide the cursor mesh.
			meshRenderer.enabled = false;
		}
	}
}
