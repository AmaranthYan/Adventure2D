using UnityEngine;
using System.Collections;

public class EventTrigger : MonoBehaviour {
	public GameObject[] triggers;
	private bool isTriggered = false;

	void FixedUpdate() {
		isTriggered = false;
	}

	void OnTriggerStay2D(Collider2D collider) {
		foreach (GameObject trigger in triggers) {
			if (collider.gameObject.name == trigger.name) {
				isTriggered = true;
				break;
			}
		}
	}

	public bool GetEvent() {
		return isTriggered;
	}
}
