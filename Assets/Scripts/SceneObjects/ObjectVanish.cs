using UnityEngine;
using System.Collections;

public class ObjectVanish : MonoBehaviour {
	public float timeExist = 2.5f;
	private Animator animator;

	void Awake() {
		animator = this.GetComponent<Animator>();
	}

	void Start() {
		Invoke("Vanish", timeExist);
	}

	private void Vanish() {
		if (animator != null)
			animator.SetTrigger("vanish");
	}
	
	public void Destroy() {
		GameObject.DestroyObject(gameObject);
	}
}
