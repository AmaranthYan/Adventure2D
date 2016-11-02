using UnityEngine;
using System.Collections;

public class LightController : MonoBehaviour {
	private Animator animator;
	private GameObject puppy;

	void Awake() {
		animator = this.GetComponent<Animator>();
		puppy = GameObject.Find("Puppy");
	}

	void Start () {
		Invoke("TurnOn", 1.0f);
	}
	
	private void TurnOn() {
		animator.SetTrigger("turnOn");
	}

	private void FocusPuppy() {
		SpriteRenderer puppyRenderer = puppy.GetComponent<Renderer>() as SpriteRenderer;
		Color color = new Color(1.0f, 1.0f, 1.0f, puppyRenderer.color.a);
		puppyRenderer.color = color;
	}
}
