using UnityEngine;
using System.Collections;

public class Scene8 : Scene {
	protected GameObject puppy;
	protected GameObject bunny;


    protected override void Awake()
    {
        base.Awake();
        puppy = GameObject.Find("Puppy");
        bunny = GameObject.Find("Bunny");
    }
	
	protected override void Initialize() {
		base.Initialize();
		puppy.transform.position = new Vector3(-6.95f, -0.125f, 0.0f);
		puppy.GetComponent<CharacterController2D>().defaultDirection = CharacterController2D.Direction.RIGHT;
		puppy.GetComponent<CharacterController2D>().ResetCurrentDirection();
		Camera.main.transform.position = new Vector3(-0.8f, 0.0f, -10.0f);
	}
	
	protected override IEnumerator Narration() {
		cameraFade.FadeIn();
		yield return new WaitForSeconds(1.5f);
		narrator.SetTextFont(narrator.NarrationFont, FontStyle.Normal);
		narrator.DisplayText("Puppy found bunny in the depths of the cavern...", 2.0f);
		while (!isNarrationTriggered[0]) {
			isNarrationTriggered[0] = narrationTriggers[0].GetEvent();
			yield return new WaitForEndOfFrame();
		}
		scriptController.DisableAbility(puppy);
		scriptController.DisableCharacterController(puppy);
		puppy.GetComponent<CharacterController2D>().Halt();
		while (!isNarrationTriggered[1]) {
			isNarrationTriggered[1] = narrationTriggers[1].GetEvent();
			bunny.GetComponent<CharacterController2D>().Walk(CharacterController2D.Direction.LEFT);
			yield return new WaitForEndOfFrame();
		}
		bunny.GetComponent<CharacterController2D>().Halt();
		characterSwitch.AddCharacter(bunny);
		yield return new WaitForSeconds(0.5f);
		while (!isNarrationTriggered[2] || !isNarrationTriggered[3]) {
			isNarrationTriggered[2] = narrationTriggers[2].GetEvent();
			if (!isNarrationTriggered[2])
				bunny.GetComponent<CharacterController2D>().Walk(CharacterController2D.Direction.RIGHT);
			else
				bunny.GetComponent<CharacterController2D>().Halt();
			isNarrationTriggered[3] = narrationTriggers[3].GetEvent();
			if (!isNarrationTriggered[3])
				puppy.GetComponent<CharacterController2D>().Walk(CharacterController2D.Direction.RIGHT);
			else
				puppy.GetComponent<CharacterController2D>().Halt();
			yield return new WaitForEndOfFrame();
		}
		scriptController.switchDisabler = false;
		scriptController.EnableCharacterSwitch();
		scriptController.EnableCharacterController(puppy);
		scriptController.EnableAbility(puppy);
	}
	
	protected override IEnumerator CutScene() {
		while (!isCutSceneTriggered) {
			isCutSceneTriggered = true;
			foreach (EventTrigger trigger in cutsceneTriggers)
				isCutSceneTriggered &= trigger.GetEvent();
			if (isCutSceneTriggered) {
				cameraFade.FadeOut();
				yield return new WaitForSeconds(1.0f);
				Application.LoadLevel("Scene9");
			}
			yield return new WaitForEndOfFrame();
		}
	}

	protected override void Restart() {
		
	}
}
