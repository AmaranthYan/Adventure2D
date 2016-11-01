using UnityEngine;
using System.Collections;

public class ScriptController : MonoBehaviour {
	public bool switchDisabler = true;

	void Update() {
		if (switchDisabler)
			DisableCharacterSwitch();
	}

	public void EnableCharacterSwitch() {
		this.GetComponent<CharacterSwitch>().enabled = true;
	}

	public void DisableCharacterSwitch() {
		this.GetComponent<CharacterSwitch>().enabled = false;
	}

	public void EnableCharacterController(GameObject character) {
		if (character.tag != "Character")
			return;
		character.GetComponent<CharacterController2D>().enabled = true;
	}

	public void DisableCharacterController(GameObject character) {
		if (character.tag != "Character")
			return;
		character.GetComponent<CharacterController2D>().enabled = false;
		Animator animator = character.GetComponent<Animator> ();
		animator.SetBool("isMoving", false);
		animator.SetTrigger("idle");
	}

	public void EnableAbility(GameObject character) {
		if (character.tag != "Character")
			return;
		character.GetComponent<Ability>().enabled = true;
	}

	public void DisableAbility(GameObject character) {
		if (character.tag != "Character")
			return;
		character.GetComponent<Ability>().enabled = false;
	}
}
