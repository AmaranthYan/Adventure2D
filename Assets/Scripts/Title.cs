using UnityEngine;
using System.Collections;

public class Title : MonoBehaviour {
	private CameraFade cameraFade;
	private SpriteRenderer spriteRenderer;
	[SerializeField]
	private Sprite logo;
	[SerializeField]
	private GameObject dedication;

	void Awake() {
		cameraFade = Camera.main.GetComponent<CameraFade>();
		spriteRenderer = this.GetComponent<SpriteRenderer>();
	}

	void Start() {
		StartCoroutine(DisplayTitle());
	}

	void OnDestroy() {
		StopCoroutine(DisplayTitle());
	}

	private IEnumerator DisplayTitle() {
		spriteRenderer.sprite = logo;
		cameraFade.FadeIn();
		yield return new WaitForSeconds(2.0f);
		cameraFade.FadeOut();
		yield return new WaitForSeconds(1.0f);
        spriteRenderer.sprite = null;
        dedication.SetActive(true);
        cameraFade.FadeIn();
		yield return new WaitForSeconds(2.0f);
		cameraFade.FadeOut();
		yield return new WaitForSeconds(1.0f);
		Application.LoadLevel("MainMenu");
	}
}
