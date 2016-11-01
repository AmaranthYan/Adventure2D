using UnityEngine;
using System.Collections;

public class Scene13 : Scene {
	protected GameObject puppy;
	[SerializeField]
	private Sprite puppyBack;

    protected override void Awake()
    {
        base.Awake();
        puppy = GameObject.Find("Puppy");
        if (!puppy)
        {
            puppy = Instantiate(instantiatedSceneObjects[0]);
            puppy.name = "Puppy";
        }
        FindObjectOfType<CharacterSwitch>().AddCharacter(puppy);
    }
	
	protected override void Initialize() {
		base.Initialize();
		puppy.transform.position = new Vector3(-7.5f, -2.1f, 0.0f);
		puppy.GetComponent<CharacterController2D>().defaultDirection = CharacterController2D.Direction.RIGHT;
		puppy.GetComponent<CharacterController2D>().ResetCurrentDirection();
		Camera.main.transform.position = new Vector3(-1.55f, 0.0f, -10.0f);
		cameraTrack.leftBound = -8.75f;
		cameraTrack.rightBound = 8.75f;
	}
	
	protected override IEnumerator Narration() {
		cameraFade.FadeIn();
		while (!isNarrationTriggered[0]) {
			isNarrationTriggered[0] = narrationTriggers[0].GetEvent();
			yield return new WaitForEndOfFrame();
		}
		scriptController.DisableAbility(puppy);
		scriptController.DisableCharacterController(puppy);
		while (!isNarrationTriggered[1]) {
			puppy.GetComponent<CharacterController2D>().Walk(CharacterController2D.Direction.RIGHT);
			isNarrationTriggered[1] = narrationTriggers[1].GetEvent();
			yield return new WaitForEndOfFrame();
		}
		puppy.GetComponent<CharacterController2D>().Halt();
		yield return new WaitForSeconds(0.2f);
		puppy.GetComponent<Animator>().enabled = false;
		puppy.GetComponent<SpriteRenderer>().sprite = puppyBack;
		narrator.SetTextFont(narrator.NarrationFont, FontStyle.Normal);
		narrator.DisplayText("Fin");
		yield return new WaitForSeconds(4.0f);
		narrator.FadeOut();
		isCutSceneTriggered = true;
	}
	
	protected override IEnumerator CutScene() {
		while (!isCutSceneTriggered) {
			yield return new WaitForEndOfFrame();
		}
		if (isCutSceneTriggered) {
			cameraFade.FadeOut();
			yield return new WaitForSeconds(1.0f);
			Application.LoadLevel("Credit");
            GameObject.DestroyObject(narrator.gameObject);
            GameObject.DestroyObject(scriptController.gameObject);
            GameObject.DestroyObject(cameraFade.gameObject);
        }
	}

	protected override void Restart() {
		
	}
}