using UnityEngine;
using System.Collections;

public class DeathZone : MonoBehaviour {
	public GameObject[] characters;

	void OnTriggerEnter2D(Collider2D collider) {
		foreach (GameObject character in characters) {
			if (collider.gameObject.name == character.name) {
				Scene.ReloadLevel();
				return;
			}
		}
	}
}
