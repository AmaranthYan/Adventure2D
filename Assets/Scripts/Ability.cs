using UnityEngine;
using System;
using System.Collections;

public abstract class Ability : MonoBehaviour {
	[Serializable]
	protected struct CustomCursor {
		public Texture2D cursorIcon;
		public Vector2 offset;
	}
	protected const int MOUSE_LEFT_BUTTON = 0;
	protected const int MOUSE_RIGHT_BUTTON = 1;
	protected ScriptController scriptController;
	protected CharacterController2D characterController;
	protected bool isActive = false;
	protected bool isAllowed = true;
	protected Vector3 mousePosition;
	[SerializeField]
	protected float range = 2.5f;
	[SerializeField]
	protected CustomCursor[] cursors;

	void Awake() {
		scriptController = GameObject.Find("Characters").GetComponentInParent<ScriptController>();
		characterController = this.GetComponent<CharacterController2D>();
	}

	void Start() {

	}

	void Update() {
		ActiveCheck();
		if (isActive) {
			MousePositionUpdate();
			Action();
		}
	}

	void OnDisable() {
		Disable();
	}

	protected void ActiveCheck() {
		if (Input.GetMouseButtonDown(MOUSE_RIGHT_BUTTON)) {
			if (!isActive) {
				if (!characterController.GroundCheck())
					return;
				Enable();
			} else
				Disable();
		}
	}

	protected void MousePositionUpdate() {
		mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mousePosition.z = 0;
	}

	protected void Enable() {
		scriptController.DisableCharacterSwitch();
		scriptController.DisableCharacterController(gameObject);
		characterController.Halt();
		isActive = true;
		OnActionEnable();
	}

	protected void Disable() {
		isActive = false;
		OnActionDisable();
		scriptController.EnableCharacterController(gameObject);
		scriptController.EnableCharacterSwitch();
	}

	protected void SetCursor(CustomCursor cursor) {
		Cursor.SetCursor(
			cursor.cursorIcon,
			new Vector2(cursor.cursorIcon.width * cursor.offset.x, cursor.cursorIcon.height * cursor.offset.y),
			CursorMode.Auto
		);
	}

	protected void ResetCursor() {
		Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
	}

	protected virtual void Action() {
		ValidityCheck();
		if (isAllowed)
			Apply();
	}

	protected abstract void OnActionEnable();
	
	protected abstract void OnActionDisable();

	protected virtual bool ValidityCheck() {
		isAllowed = true;
		float distance = Vector2.Distance(
			new Vector2(mousePosition.x, mousePosition.y),
			new Vector2(gameObject.transform.position.x, gameObject.transform.position.y)
			);
		if (distance > range)
			isAllowed = false;
		return isAllowed;
	}

	protected abstract void Apply();
}
