using UnityEngine;
using System.Collections;

public class Scene6 : Scene {
	protected GameObject puppy;
	protected GameObject bunny;

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
        if (!bunny)
        {
            bunny = Instantiate(instantiatedSceneObjects[1]);
            bunny.name = "Bunny";
        }
        FindObjectOfType<CharacterSwitch>().AddCharacter(bunny);
    }
	
	protected override void Initialize() {
		base.Initialize();
		puppy.transform.position = new Vector3(-7.6f, -1.05f, 0.0f);
		puppy.GetComponent<CharacterController2D>().defaultDirection = CharacterController2D.Direction.RIGHT;
		puppy.GetComponent<CharacterController2D>().ResetCurrentDirection();
		bunny.transform.position = new Vector3(-7.3f, -1.08f, 0.0f);
		bunny.GetComponent<CharacterController2D>().defaultDirection = CharacterController2D.Direction.RIGHT;
		bunny.GetComponent<CharacterController2D>().ResetCurrentDirection();
		characterSwitch.RemoveCharacter(bunny);
		scriptController.switchDisabler = true;
		scriptController.DisableCharacterSwitch();
		scriptController.DisableAbility(puppy);
		scriptController.DisableCharacterController(puppy);
		Camera.main.transform.position = new Vector3(-1.8f, 0.0f, -10.0f);
		cameraTrack.leftBound = -9.0f;
		cameraTrack.rightBound = 9.0f;
	}
	
	protected override IEnumerator Narration() {
		cameraFade.FadeIn();
		yield return new WaitForSeconds(1.5f);
		narrator.SetTextFont(narrator.NarrationFont, FontStyle.Normal);
		narrator.DisplayText("When bunny got scared, she escaped to her burrow...", 2.0f);
		yield return new WaitForSeconds(3.0f);
		narrator.DisplayText("...and left puppy outside...", 1.5f);
		cameraTrack.SetTrackedObject(bunny);
		while (!isNarrationTriggered[0]) {
			bunny.GetComponent<CharacterController2D>().Walk(CharacterController2D.Direction.RIGHT);
			isNarrationTriggered[0] = narrationTriggers[0].GetEvent();
			yield return new WaitForEndOfFrame();
		}
		bunny.GetComponent<CharacterController2D>().Halt();
		bunny.GetComponent<CharacterController2D>().Walk(CharacterController2D.Direction.RIGHT);
		bunny.GetComponent<CharacterController2D>().Jump();
		while (!isNarrationTriggered[1]) {
			isNarrationTriggered[1] = narrationTriggers[1].GetEvent();
			yield return new WaitForEndOfFrame();
		}
		GameObject.DestroyObject(bunny);
		yield return new WaitForSeconds(0.5f);
		Camera.main.GetComponent<Animator>().enabled = true;
		yield return new WaitForSeconds(4.5f);
		Camera.main.GetComponent<Animator>().enabled = false;
		cameraTrack.isAutoTracking = true;
		scriptController.EnableCharacterController(puppy);
		scriptController.EnableAbility(puppy);
	}
	
	protected override IEnumerator CutScene() {
		while (!isCutSceneTriggered) {
			isCutSceneTriggered = cutsceneTriggers[0].GetEvent();	
			if (isCutSceneTriggered) {
				cameraFade.FadeOut();
				yield return new WaitForSeconds(1.0f);
				Application.LoadLevel("Scene7");
			}
			yield return new WaitForEndOfFrame();
		}
	}

	protected override void Restart() {

	}
}
