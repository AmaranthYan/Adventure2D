using UnityEngine;
using System.Collections;

public class Scene9 : Scene {
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
		puppy.transform.position = new Vector3(8.0f, 2.72f, 0.0f);
		puppy.GetComponent<CharacterController2D>().defaultDirection = CharacterController2D.Direction.LEFT;
		puppy.GetComponent<CharacterController2D>().ResetCurrentDirection();
		bunny.transform.position = new Vector3(7.85f, 2.68f, 0.0f);
		bunny.GetComponent<CharacterController2D>().defaultDirection = CharacterController2D.Direction.LEFT;
		bunny.GetComponent<CharacterController2D>().ResetCurrentDirection();
		Camera.main.transform.position = new Vector3(1.8f, 0.0f, -10.0f);
		cameraTrack.leftBound = -9.1f;
		cameraTrack.rightBound = 9.1f;
        scriptController.switchDisabler = false;
        scriptController.EnableCharacterSwitch();
    }
	
	protected override IEnumerator Narration() {
		cameraFade.FadeIn();
		yield return new WaitForSeconds(1.5f);
		narrator.SetTextFont(narrator.NarrationFont, FontStyle.Normal);
		narrator.DisplayText("Puppy wanted to shelter bunny...", 1.5f);
	}
	
	protected override IEnumerator CutScene() {
		while (!isCutSceneTriggered) {
			isCutSceneTriggered = true;
			foreach (EventTrigger trigger in cutsceneTriggers)
				isCutSceneTriggered &= trigger.GetEvent();
			if (isCutSceneTriggered) {
				cameraFade.FadeOut();
				yield return new WaitForSeconds(1.0f);
				Application.LoadLevel("Scene10");
			}
			yield return new WaitForEndOfFrame();
		}
	}
}
