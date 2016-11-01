using UnityEngine;
using System.Collections;

public class Hit : MonoBehaviour {
	public float criticalVelocity;
	
	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.CompareTag("Character")) {
			if (GetComponent<Rigidbody2D>().velocity.magnitude > criticalVelocity)
				Scene.ReloadLevel();
		}
	}
}
