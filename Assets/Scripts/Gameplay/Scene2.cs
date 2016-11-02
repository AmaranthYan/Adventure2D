using UnityEngine;
using System.Collections;

public class Scene2 : Scene {
	protected GameObject puppy;

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
		scriptController.DisableAbility(puppy);
		puppy.transform.position = new Vector3(-9.35f, -1.2f, 0.0f);
		puppy.GetComponent<CharacterController2D>().defaultDirection = CharacterController2D.Direction.RIGHT;
		puppy.GetComponent<CharacterController2D>().ResetCurrentDirection();
		Camera.main.transform.position = new Vector3(-3.25f, 0.0f, -10.0f);
        cameraTrack.leftBound = -10.5f;
        cameraTrack.rightBound = 10.5f;
    }
	
	protected override IEnumerator Narration() {
		bool input;
		cameraFade.FadeIn();
		while (!isNarrationTriggered[0]) {
			isNarrationTriggered[0] = narrationTriggers[0].GetEvent();
			yield return new WaitForEndOfFrame();
		}
		narrator.SetTextFont(narrator.NarrationFont, FontStyle.Normal);
		narrator.DisplayText("The lonely puppy built up a barrier...", 2.0f);
		yield return new WaitForSeconds(3.0f);
		narrator.SetTextFont(narrator.InstructionFont, FontStyle.BoldAndItalic, 40);
		narrator.DisplayText("[Right Click] to set a barrier, [Left Click] to cancel");
		yield return new WaitForSeconds(1.0f);
		scriptController.EnableAbility(puppy);
		input = false;
		while (!input) {
			if (Input.GetMouseButton(MOUSE_RIGHT_BUTTON))
				input = true;
			yield return new WaitForEndOfFrame();
		}
		narrator.FadeOut();
		while (!isNarrationTriggered[1]) {
			isNarrationTriggered[1] = narrationTriggers[1].GetEvent();
			yield return new WaitForEndOfFrame();
		}
		narrator.SetTextFont(narrator.NarrationFont, FontStyle.Normal);
		narrator.DisplayText("...and hid behind it...", 1.0f);
	}
	
	protected override IEnumerator CutScene() {
		while (!isCutSceneTriggered) {
			isCutSceneTriggered = cutsceneTriggers[0].GetEvent();	
			if (isCutSceneTriggered) {
				cameraFade.FadeOut();
				yield return new WaitForSeconds(1.0f);
				Application.LoadLevel("Scene3");
			}
			yield return new WaitForEndOfFrame();
		}
	}
}
