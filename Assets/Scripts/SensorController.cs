using UnityEngine;
using System.Collections;

public class SensorController : MonoBehaviour {
	private Animator animator;

	void Awake() {
		animator = this.GetComponent<Animator>();
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.layer == LayerMask.NameToLayer("Character"))
			animator.SetTrigger("activate");
	}
}
