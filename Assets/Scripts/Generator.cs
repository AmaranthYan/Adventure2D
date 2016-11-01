using UnityEngine;
using System.Collections;

public class Generator : MonoBehaviour {
	public float offset = 0.6f;
	[SerializeField]
	private GameObject item;
	public float timeInterval;
	public float initialSpeed;
	private Vector3 direction;

	void Start() {
		StartCoroutine(Generate());
	}

	void OnDestroy() {
		StopCoroutine(Generate());
	}

	private IEnumerator Generate() {
		while (true) {
			Vector3 direction = -transform.up;
			Vector3 position = transform.position + offset * direction;
			Quaternion rotation = transform.rotation;
			GameObject generated = GameObject.Instantiate(item, position, rotation) as GameObject;
			generated.GetComponent<Rigidbody2D>().velocity = initialSpeed * direction;
			yield return new WaitForSeconds(timeInterval);
		}
	}
}
