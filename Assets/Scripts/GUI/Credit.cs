using UnityEngine;
using System.Collections;

public class Credit : MonoBehaviour {
	private CameraFade cameraFade;	
	[SerializeField]
	private GameObject credit;
	
	void Awake() {
		cameraFade = Camera.main.GetComponent<CameraFade>();		
	}
	
	void Start() {
		StartCoroutine(DisplayCredit());
	}
	
	void OnDestroy() {
		StopCoroutine(DisplayCredit());
	}
	
	private IEnumerator DisplayCredit() {
        credit.SetActive(true);
		cameraFade.FadeIn();
		yield return new WaitForSeconds(4.0f);
		cameraFade.FadeOut();
		yield return new WaitForSeconds(1.0f);
		Application.LoadLevel("MainMenu");
	}
}