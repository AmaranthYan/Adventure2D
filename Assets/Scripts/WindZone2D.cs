using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WindZone2D : MonoBehaviour {
	public enum Direction {LEFT = -1, RIGHT = 1};
	public Direction windDirection = Direction.RIGHT;
	private Direction currentDirection;
	public float density = 2.5f;
	public float intensity = 10.0f;
	private Dictionary<Rigidbody2D, int> objects;
	[SerializeField]
	private BoxCollider2D windZone;
	private const float MAX_DAMAGE = 1000.0f;

	void Start() {
		currentDirection = windDirection;
	}

	void Update() {
		WindDirectionCheck();
		ContactCheck();
		ApplyWind();
	}
		
	void OnEnable() {
		windZone.enabled = true;
	}

	void OnDisable() {
		windZone.enabled = false;
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.layer == LayerMask.NameToLayer("Character"))
			Scene.ReloadLevel();
	}

	void OnTriggerStay2D(Collider2D collider) {
		if (collider.gameObject.layer == LayerMask.NameToLayer("BreakableObject"))
			collider.SendMessage("ApplyDamage", MAX_DAMAGE);
	}

	private void WindDirectionCheck() {
		if (currentDirection != windDirection) {
			Vector3 scale = transform.localScale;
			scale.x *= -1;
			transform.localScale = scale;
			currentDirection = windDirection;
		}
	}

	private void ContactCheck() {
		Vector2 windCenter = new Vector2(
			windZone.offset.x * transform.localScale.x + transform.position.x,
			windZone.offset.y * transform.localScale.y + transform.position.y
		);
		objects = new Dictionary<Rigidbody2D, int>();
		float positionY = 0.0f;
		while (positionY <= windZone.size.y * transform.localScale.y / 2) {
			if (positionY == 0.0f) {
				RayCheckAtPosition(windCenter);
			} else {
				RayCheckAtPosition(windCenter + new Vector2(0.0f, positionY));
				RayCheckAtPosition(windCenter - new Vector2(0.0f, positionY));
			}
			positionY += 1.0f / density;
		}
	}

	private void RayCheckAtPosition(Vector2 position) {
		Vector2 direction = new Vector2((int)windDirection, 0);
		RaycastHit2D[] hits = Physics2D.RaycastAll(position, direction);
		int index = 0;
		while (index < hits.Length) {
			if (hits[index].collider.CompareTag("WindBlock"))
				break;
			Rigidbody2D rigidbodyHit = hits[index].rigidbody as Rigidbody2D;
			if (rigidbodyHit != null) {
				if (objects.ContainsKey(rigidbodyHit))
					objects[rigidbodyHit]++;
				else
					objects.Add(rigidbodyHit, 1);
			}
			index++;
		}
		Debug.DrawRay(new Vector3(position.x, position.y, 0), new Vector3(direction.x * 10, direction.y, 0));
	}

	private void ApplyWind() {
		Vector2 direction = new Vector2((int)windDirection, 0);
		foreach (KeyValuePair<Rigidbody2D, int> obj in objects) {
			obj.Key.AddForce(obj.Value * intensity * direction);
		}
	}
}
