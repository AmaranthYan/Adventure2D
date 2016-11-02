using UnityEngine;
using System.Collections;

public class CameraTrack : MonoBehaviour {
	private Camera mainCamera;
	private GameObject tracked;
	public float leftBound;
	public float rightBound;
	public bool isAutoTracking = true;
	public float trackingSpeed = 5.0f;
	[SerializeField]
	private CharacterSwitch characterSwitch;

	void Awake() {
		GameObject.DontDestroyOnLoad(this);
	}

	void Start() {
		mainCamera = Camera.main;
		float cameraLeftSide = mainCamera.transform.position.x - mainCamera.aspect * mainCamera.orthographicSize;
		float cameraRightSide = mainCamera.transform.position.x + mainCamera.aspect * mainCamera.orthographicSize;
		if (leftBound > cameraLeftSide)
			leftBound = cameraLeftSide;
		if (rightBound < cameraRightSide)
			rightBound = cameraRightSide;
	}

	void FixedUpdate() {
		if (isAutoTracking)
			tracked = characterSwitch.GetCurrentCharacter();
		Track();
	}

	public void SetTrackedObject(GameObject trackedObject) {
		isAutoTracking = false;
		tracked = trackedObject;
	}

	private void Track() {
		if (mainCamera == null)
			return;
		if (tracked == null)
			return;
		Vector3 currentPosition = mainCamera.transform.position;
		Vector3 targetPosition = tracked.transform.position;
		if (targetPosition.x < leftBound + mainCamera.aspect * mainCamera.orthographicSize)
			targetPosition.x = leftBound + mainCamera.aspect * mainCamera.orthographicSize;
		if (targetPosition.x > rightBound - mainCamera.aspect * mainCamera.orthographicSize)
			targetPosition.x = rightBound - mainCamera.aspect * mainCamera.orthographicSize;
		mainCamera.transform.position = Vector3.Lerp(
			new Vector3(currentPosition.x, currentPosition.y, currentPosition.z),
			new Vector3(targetPosition.x, currentPosition.y, currentPosition.z),
			Time.fixedDeltaTime * trackingSpeed
		);
	}
}
