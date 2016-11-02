using UnityEngine;
using System.Collections;

public class BreakableObject : MonoBehaviour {
	public float lifeTotal = 10.0f;
	[SerializeField]
	private float lifeLeft;
	private const float DAMAGE_SCALE = 10.0f;
	private Animator animator;

	void Awake() {
		animator = this.GetComponent<Animator>();
	}

	void Start() {
		if ((lifeLeft > lifeTotal) || (lifeLeft <= 0.0f))
			lifeLeft = lifeTotal;
	}

	void Update() {
		LifeUpdate();
	}

	private void LifeUpdate() {
		float lifePercentage = lifeLeft / lifeTotal;
		if (animator != null)
			animator.SetFloat("life", lifePercentage);
	}

	public void ApplyDamage(float damage) {
		lifeLeft -= damage;
	}

	public void Destroy() {
		DestroyObject(gameObject);
	}
}
