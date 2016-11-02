using UnityEngine;
using System.Collections;

public class Crush : MonoBehaviour {
	[SerializeField]
	private CrusherController crusherController;
	public float force = 7.5f;

	void OnTriggerEnter2D(Collider2D collider) {
		if (!crusherController.GetIsActive())
			return;
		if (collider.gameObject.layer == LayerMask.NameToLayer("Character"))
			Scene.ReloadLevel();
	}
	
	void OnTriggerStay2D(Collider2D collider) {
		if (!crusherController.GetIsActive())
			return;
		if (collider.gameObject.layer == LayerMask.NameToLayer("BreakableObject"))
			collider.SendMessage("ApplyDamage", force * Time.fixedDeltaTime);
	}
}
