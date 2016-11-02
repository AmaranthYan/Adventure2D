using UnityEngine;
using System.Collections;

public class Destroy : Ability {
	public float damage = 5.0f;
	private GameObject selected;

	protected override void Action() {
		Select();
		base.Action();
	}

	protected override void OnActionEnable() {
		SetCursor(cursors[0]);
	}

	protected override void OnActionDisable() {
		ResetCursor();
	}

	private void Select() {
		float mouseX = mousePosition.x;
		float mouseY= mousePosition.y;
		RaycastHit2D horizontal = Physics2D.Linecast(new Vector2(mouseX - 0.1f, mouseY), new Vector2(mouseX + 0.1f, mouseY));
		RaycastHit2D vertical = Physics2D.Linecast(new Vector2(mouseX, mouseY + 0.1f), new Vector2(mouseX, mouseY - 0.1f));
		Debug.DrawLine(new Vector3(mouseX - 0.1f, mouseY, 0.0f), new Vector3(mouseX + 0.1f, mouseY, 0.0f));
		Debug.DrawLine(new Vector3(mouseX, mouseY + 0.1f, 0.0f), new Vector3(mouseX, mouseY - 0.1f, 0.0f));
		selected = (
			(horizontal.collider != null) &&
			(vertical.collider != null) &&
			(horizontal.collider == vertical.collider)
		) ? horizontal.collider.gameObject : null;
	}

	protected override bool ValidityCheck() {
		base.ValidityCheck();
		if ((selected == null) || (selected.layer != LayerMask.NameToLayer("BreakableObject")))
			isAllowed = false;
		if (!isAllowed)
			SetCursor(cursors[1]);
		else
			SetCursor(cursors[0]);
		return isAllowed;
	}

	protected override void Apply() {
		if (Input.GetMouseButtonDown(MOUSE_LEFT_BUTTON)) {
			selected.SendMessage("ApplyDamage", damage);
		}
	}
}
