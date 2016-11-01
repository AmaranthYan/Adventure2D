using UnityEngine;
using System.Collections;

public class Scene3 : Scene {
	protected GameObject puppy;
	protected GameObject bunny;
	[SerializeField]
	private SwitchController elevatorSwitch;
	[SerializeField]
	private GameObject fileCabinet;

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
		puppy.transform.position = new Vector3(-10.3f, -0.5f, 0.0f);
		puppy.GetComponent<CharacterController2D>().defaultDirection = CharacterController2D.Direction.RIGHT;
		puppy.GetComponent<CharacterController2D>().ResetCurrentDirection();
		if (bunny != null) {
			bunny.transform.position = new Vector3(-10.0f, -0.55f, 0.0f);
			bunny.GetComponent<CharacterController2D>().defaultDirection = CharacterController2D.Direction.RIGHT;
			bunny.GetComponent<CharacterController2D>().ResetCurrentDirection();
			fileCabinet.GetComponent<Rigidbody2D>().isKinematic = true;
		}
		Camera.main.transform.position = new Vector3(-4.75f, 0.0f, -10.0f);
		cameraTrack.leftBound = -12.0f;
		cameraTrack.rightBound = 12.0f;
	}
	
	protected override IEnumerator Narration() {
		cameraFade.FadeIn();
		yield return new WaitForSeconds(1.5f);
		if (bunny == null) {
			narrator.SetTextFont(narrator.NarrationFont, FontStyle.Normal);
			narrator.DisplayText("Sometimes, things could get tricky...", 2.0f);
		} else {
			narrator.SetTextFont(narrator.NarrationFont, FontStyle.Normal);
			narrator.DisplayText("They went through a lot together...", 1.5f);
		}
		yield return new WaitForSeconds(2.5f);
		while (!isNarrationTriggered[0]) {
			isNarrationTriggered[0] = narrationTriggers[0].GetEvent();
			yield return new WaitForEndOfFrame();
		}
		narrator.SetTextFont(narrator.InstructionFont, FontStyle.BoldAndItalic, 40);
		narrator.DisplayText("use [E] to activate the switch");
		yield return new WaitForSeconds(1.0f);
		while (!elevatorSwitch.GetIsActivated())
			yield return new WaitForEndOfFrame();
		narrator.FadeOut();
	}
	
	protected override IEnumerator CutScene() {
		while (!isCutSceneTriggered) {
			if (bunny == null)
            {
                isCutSceneTriggered = cutsceneTriggers[0].GetEvent();
            }
            if (isCutSceneTriggered) {
				cameraFade.FadeOut();
				yield return new WaitForSeconds(1.0f);
				Application.LoadLevel("Scene4");
			} else {
				isCutSceneTriggered = true;
				foreach (EventTrigger trigger in cutsceneTriggers)
					isCutSceneTriggered &= trigger.GetEvent();
				if (isCutSceneTriggered) {
					cameraFade.FadeOut();
					yield return new WaitForSeconds(1.0f);
					Application.LoadLevel("Scene5");
				}
			}
			yield return new WaitForEndOfFrame();
		}
	}
}
