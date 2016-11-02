using UnityEngine;
using System.Collections;

public class Summon : Ability {
	[SerializeField]
	private GameObject prefab;
	private GameObject summoned;

	protected override void Action() {
		CreateOrMoveInstance();
		base.Action();
	}

	protected override void OnActionEnable() {

	}
	
	protected override void OnActionDisable() {
		if (summoned != null) 
			GameObject.DestroyObject(summoned);
		ResetCursor();
	}

	private void CreateOrMoveInstance() {
		if (summoned == null) {
			GameObject instance = GameObject.Find(prefab.name);
			if (instance != null) 
				GameObject.DestroyObject(instance);
			summoned = Instantiate(prefab) as GameObject;
			summoned.name = prefab.name;
		}
		summoned.transform.position = mousePosition;
	}

	protected override bool ValidityCheck() {
		base.ValidityCheck();
		DetectionTrigger collisionDetectionTrigger = summoned.transform.FindChild("Detector/CollisionDetector").GetComponent<DetectionTrigger>();
		DetectionTrigger contactDetectionTrigger = summoned.transform.FindChild("Detector/ContactDetector").GetComponent<DetectionTrigger>();
		if (collisionDetectionTrigger.GetDetection() || !contactDetectionTrigger.GetDetection())
			isAllowed = false;
		if (!isAllowed)
			SetCursor(cursors[0]);
		else
			ResetCursor();
		return isAllowed;
	}

	protected override void Apply() {
		if (Input.GetMouseButtonDown(MOUSE_LEFT_BUTTON)) {
			GameObject.DestroyObject(summoned.transform.FindChild("Detector").gameObject);
			Collider2D[] colliders = summoned.GetComponentsInChildren<Collider2D>();
			foreach (Collider2D collider in colliders) {
				if (collider.name == "Block")
					collider.enabled = true;
			}
			summoned = null;
			Disable();
		}
	}
}
