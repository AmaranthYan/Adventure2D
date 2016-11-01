using UnityEngine;
using System.Collections;

public class DetectionTrigger : MonoBehaviour {
	[SerializeField]
	private LayerMask layer;
	private bool isDetected = false;

	void FixedUpdate() {
		isDetected = false;
	}
	
	void OnTriggerStay2D(Collider2D collider) {
		if (collider.isTrigger)
			return;
		if ((1 & (layer >> collider.gameObject.layer)) == 1)
			isDetected = true;
	}

	public bool GetDetection() {
		return isDetected;
	}
}
