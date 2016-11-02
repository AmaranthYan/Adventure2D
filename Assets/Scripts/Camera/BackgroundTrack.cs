using UnityEngine;
using System.Collections;

public class BackgroundTrack : MonoBehaviour {
	private Camera mainCamera;
	private Vector3 cameraPreviousPosition;
	public float ratio = 0.0f;
	
	void Start() {
		mainCamera = Camera.main;
		cameraPreviousPosition = mainCamera.transform.position;
	}
	
	void FixedUpdate() {
		Track();
	}
	
	private void Track() {
		if (mainCamera == null)
			return;
		Vector3 distance = mainCamera.transform.position - cameraPreviousPosition; 
		Vector3 currentPosition = transform.position;
		Vector3 targetPosition = currentPosition + distance * ratio;
		transform.position = new Vector3(targetPosition.x, currentPosition.y, currentPosition.z);
		cameraPreviousPosition = mainCamera.transform.position;
	}
}