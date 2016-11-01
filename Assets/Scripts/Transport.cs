using UnityEngine;
using System.Collections;

public class Transport : MonoBehaviour {
	[SerializeField]
	private Transform transportation;
	private Vector3 previousPosition;
	private Vector3 deltaPosition;

	void Start() {
		previousPosition = transform.position;
	}

	void FixedUpdate() {
		deltaPosition = transform.position - previousPosition;
		previousPosition = transform.position;
	}

	void OnTriggerStay2D(Collider2D collider) {
		if (collider.transform.IsChildOf(transportation))
			return;
		if (collider.isTrigger)
			return;
		collider.transform.position += deltaPosition;
	}
}
