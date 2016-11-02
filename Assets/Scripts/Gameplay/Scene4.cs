using UnityEngine;
using System.Collections;

public class Scene4 : Scene {
	protected GameObject puppy;
	protected GameObject bunny;
	[SerializeField]
	private SpriteRenderer exclamation;

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
        bunny = GameObject.Find("Bunny");
	}
	
	protected override void Initialize() {
		base.Initialize();
		puppy.transform.position = new Vector3(-6.65f, -1.15f, 0.0f);
		puppy.GetComponent<CharacterController2D>().defaultDirection = CharacterController2D.Direction.RIGHT;
		puppy.GetComponent<CharacterController2D>().ResetCurrentDirection();
		Camera.main.transform.position = new Vector3(-0.8f, 0.0f, -10.0f);
		cameraTrack.leftBound = -8.0f;
		cameraTrack.rightBound = 8.0f;
	}
	
	protected override IEnumerator Narration() {
		bool input;
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
		narrator.SetTextFont(narrator.NarrationFont, FontStyle.Normal);
		narrator.DisplayText("Oneday, puppy met a little bunny...", 1.5f);
		yield return new WaitForSeconds(2.0f);
		bunny.GetComponent<SpriteRenderer>().sortingLayerName = "Suspended";
		bunny.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
		exclamation.transform.parent = bunny.transform;
		yield return new WaitForSeconds(0.2f);
		exclamation.enabled = true;
		yield return new WaitForSeconds(0.5f);
		exclamation.enabled = false;
		yield return new WaitForSeconds(0.2f);
		bunny.GetComponent<CharacterController2D>().Jump();
		yield return new WaitForSeconds(1.0f);
		narrator.SetTextFont(narrator.InstructionFont, FontStyle.BoldAndItalic, 40);
		narrator.DisplayText("use [C] to switch between characters");
		characterSwitch.AddCharacter(bunny);
		scriptController.switchDisabler = false;
		scriptController.EnableCharacterSwitch();
		yield return new WaitForSeconds(1.0f);
		input = false;
		while (!input) {
			if (Input.GetKey(KeyCode.C))
				input = true;
			yield return new WaitForEndOfFrame();
		}
		narrator.FadeOut();
		while (!isNarrationTriggered[2]) {
			isNarrationTriggered[2] = narrationTriggers[2].GetEvent();
			yield return new WaitForEndOfFrame();
		}
		narrator.SetTextFont(narrator.InstructionFont, FontStyle.BoldAndItalic, 40);
		narrator.DisplayText("when using bunny, [Right Click] to use smash ability");
		input = false;
		while (!input) {
			if (characterSwitch.GetCurrentCharacter().Equals(bunny))
				if (Input.GetMouseButton(MOUSE_RIGHT_BUTTON))
					input = true;
			yield return new WaitForEndOfFrame();
		}
		narrator.FadeOut();
	}
	
	protected override IEnumerator CutScene() {
		while (!isCutSceneTriggered) {
			isCutSceneTriggered = true;
			foreach (EventTrigger trigger in cutsceneTriggers)
				isCutSceneTriggered &= trigger.GetEvent();
			if (isCutSceneTriggered) {
				cameraFade.FadeOut();
				yield return new WaitForSeconds(1.0f);
				GameObject.DestroyObject(exclamation.gameObject);
				Application.LoadLevel("Scene3");
			}
			yield return new WaitForEndOfFrame();
		}
	}

	protected override void Restart() {
		
	}
}