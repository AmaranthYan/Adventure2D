using UnityEngine;
using System.Collections;

public class CrusherController : MonoBehaviour {
	[SerializeField]
	private float speed = 0.3f;
	private bool isActive;

	void Start() {
		isActive = false;
	}

	void FixedUpdate() {
		if (isActive)
			GetComponent<Rigidbody2D>().velocity = -Vector2.up * speed;
	}

	public bool GetIsActive() {
		return isActive;
	}

	public void Run() {
		isActive = true;
	} 

	public void Pause() {
		isActive = false;
		GetComponent<Rigidbody2D>().velocity = Vector2.zero;
	}
}
