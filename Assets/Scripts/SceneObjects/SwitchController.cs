using UnityEngine;
using System.Collections;

public class SwitchController : MonoBehaviour {
	private bool isActivated;
	private CharacterSwitch characterSwitch;
	private Animator animator;

	void Awake() {
		characterSwitch = GameObject.Find("Characters").GetComponent<CharacterSwitch>();
		animator = this.GetComponent<Animator>();
	}

	void Start() {
		isActivated = false;
	}

	void OnTriggerStay2D(Collider2D collider) {
		if (collider.gameObject.Equals(characterSwitch.GetCurrentCharacter())) {
			if (Input.GetKeyDown(KeyCode.E)) {
				Switch();
			}
		}
	}

	public bool GetIsActivated() {
		return isActivated;
	}

	public void Switch() {
		if (animator != null) {
			animator.SetTrigger("switch");
			isActivated = true;
		}
	}
}
