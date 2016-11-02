using UnityEngine;
using System.Collections;

public class Scene1 : Scene {
	protected GameObject puppy;

	protected override void Awake() {
        base.Awake();
		puppy = GameObject.Find("Puppy");
        if (!puppy)
        {
            puppy = Instantiate(instantiatedSceneObjects[0]);
        }
        FindObjectOfType<CharacterSwitch>().AddCharacter(puppy);
    }

	protected override void Initialize() {
		base.Initialize();
		puppy.transform.position = new Vector3(-9.1f, -1.5f, 0.0f);
		puppy.GetComponent<CharacterController2D>().defaultDirection = CharacterController2D.Direction.RIGHT;
		puppy.GetComponent<CharacterController2D>().ResetCurrentDirection();
		Camera.main.transform.position = new Vector3(-3.25f, 0.0f, -10.0f);
		cameraTrack.leftBound = -10.5f;
		cameraTrack.rightBound = 10.5f;
	}

	protected override IEnumerator Narration() {
		bool input;
		cameraFade.FadeIn();
		yield return new WaitForSeconds(2.0f);
		narrator.SetTextFont(narrator.NarrationFont, FontStyle.Normal);
		narrator.DisplayText("The story begins with a puppy...", 1.5f);
		yield return new WaitForSeconds(2.5f);
		narrator.SetTextFont(narrator.InstructionFont, FontStyle.BoldAndItalic, 40);
		narrator.DisplayText("use [WSAD] to move your character");
		yield return new WaitForSeconds(1.0f);
		input = false;
		while (!input) {
			if (Input.GetKey(KeyCode.W) ||
			    Input.GetKey(KeyCode.S) ||
			    Input.GetKey(KeyCode.A) ||
			    Input.GetKey(KeyCode.D) ||
			    Input.GetKey(KeyCode.UpArrow) ||
			    Input.GetKey(KeyCode.DownArrow) ||
			    Input.GetKey(KeyCode.LeftArrow) ||
			    Input.GetKey(KeyCode.RightArrow)
			) {
				puppy.GetComponent<Animator>().enabled = true;
				scriptController.EnableCharacterController(puppy);
				input = true;
			}
			yield return new WaitForEndOfFrame();
		}
		narrator.FadeOut();
	}
	
	protected override IEnumerator CutScene() {
		while (!isCutSceneTriggered) {
			isCutSceneTriggered = cutsceneTriggers[0].GetEvent();		
			if (isCutSceneTriggered) {
				cameraFade.FadeOut();
				yield return new WaitForSeconds(1.0f);
				Application.LoadLevel("Scene2");
			}
			yield return new WaitForEndOfFrame();
		}
	}

	protected override void Restart() {
		GameObject.DestroyObject(narrator.gameObject);
		GameObject.DestroyObject(scriptController.gameObject);
		base.Restart();
	}
}
