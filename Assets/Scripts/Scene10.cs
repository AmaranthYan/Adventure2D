using UnityEngine;
using System.Collections;

public class Scene10 : Scene {
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
		puppy.transform.position = new Vector3(-9.25f, -1.05f, 0.0f);
		puppy.GetComponent<CharacterController2D>().defaultDirection = CharacterController2D.Direction.RIGHT;
		puppy.GetComponent<CharacterController2D>().ResetCurrentDirection();
		bunny.transform.position = new Vector3(-9.05f, -1.1f, 0.0f);
		bunny.GetComponent<CharacterController2D>().defaultDirection = CharacterController2D.Direction.RIGHT;
		bunny.GetComponent<CharacterController2D>().ResetCurrentDirection();
		Camera.main.transform.position = new Vector3(-3.3f, 0.0f, -10.0f);
		cameraTrack.leftBound = -10.5f;
		cameraTrack.rightBound = 10.5f;
        scriptController.switchDisabler = false;
        scriptController.EnableCharacterSwitch();
    }
	
	protected override IEnumerator Narration() {
		cameraFade.FadeIn();
		yield return new WaitForSeconds(1.5f);
		narrator.SetTextFont(narrator.NarrationFont, FontStyle.Normal);
		narrator.DisplayText("Bunny felt exhausted from time to time...", 2.0f);
		yield return new WaitForSeconds(3.0f);
		narrator.DisplayText("...for she did a lot while he did little...", 2.0f);
	}
	
	protected override IEnumerator CutScene() {
		while (!isCutSceneTriggered) {
			isCutSceneTriggered = true;
			foreach (EventTrigger trigger in cutsceneTriggers)
				isCutSceneTriggered &= trigger.GetEvent();
			if (isCutSceneTriggered) {
				cameraFade.FadeOut();
				yield return new WaitForSeconds(1.0f);
				characterSwitch.RemoveCharacter(bunny);
				scriptController.switchDisabler = true;
				scriptController.DisableCharacterSwitch();
				Application.LoadLevel("Scene11");
			}
			yield return new WaitForEndOfFrame();
		}
	}
}
