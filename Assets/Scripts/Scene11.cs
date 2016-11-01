using UnityEngine;
using System.Collections;

public class Scene11 : Scene {
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
		puppy.transform.position = new Vector3(6.1f, 2.925f, 0.0f);
		puppy.GetComponent<CharacterController2D>().defaultDirection = CharacterController2D.Direction.LEFT;
		puppy.GetComponent<CharacterController2D>().ResetCurrentDirection();
		scriptController.DisableAbility(puppy);
		scriptController.DisableCharacterController(puppy);
		Camera.main.transform.position = new Vector3(0.3f, 0.0f, -10.0f);
		cameraTrack.leftBound = -7.5f;
		cameraTrack.rightBound = 7.5f;
	}
	
	protected override IEnumerator Narration() {
		cameraFade.FadeIn();
		yield return new WaitForSeconds(1.5f);
		narrator.SetTextFont(narrator.NarrationFont, FontStyle.Normal);
		narrator.DisplayText("...then she recalled those times when she got hurt...", 2.0f);
		yield return new WaitForSeconds(3.0f);
		narrator.SetTextFont(narrator.NarrationFont, FontStyle.Italic);
		narrator.DisplayText("\"Trust me, I will catch you...\" said puppy", 1.5f);
		yield return new WaitForSeconds(1.0f);
		bunny.GetComponent<CharacterController2D>().Walk(CharacterController2D.Direction.RIGHT);
		bunny.GetComponent<CharacterController2D>().Halt();
		bunny.GetComponent<CharacterController2D>().Jump();
		yield return new WaitForSeconds(1.1f);
		bunny.GetComponent<CharacterController2D>().Jump();
		yield return new WaitForSeconds(0.2f);
		scriptController.EnableCharacterController(puppy);
		scriptController.EnableAbility(puppy);
		while (!isNarrationTriggered[0]) {
			isNarrationTriggered[0] = narrationTriggers[0].GetEvent();
			float deltaX = (puppy.transform.position.x - bunny.transform.position.x);
			int direction = (int)(deltaX / Mathf.Abs(deltaX));
			if (direction * (int)bunny.GetComponent<CharacterController2D>().GetCurrentDirection() < 0) {
				bunny.GetComponent<CharacterController2D>().Walk((CharacterController2D.Direction)direction);
				bunny.GetComponent<CharacterController2D>().Halt();
			}
			yield return new WaitForEndOfFrame();
		}
		puppy.GetComponent<CharacterController2D>().Walk(CharacterController2D.Direction.RIGHT);
		puppy.GetComponent<CharacterController2D>().Halt();
		narrator.SetTextFont(narrator.NarrationFont, FontStyle.Italic);
		narrator.DisplayText("\"No! Don't...\"", 1.0f);
		while (!isNarrationTriggered[1]) {
			isNarrationTriggered[1] = narrationTriggers[1].GetEvent();
			yield return new WaitForEndOfFrame();
		}
		bunny.GetComponent<CharacterController2D>().Walk(CharacterController2D.Direction.RIGHT);
		bunny.GetComponent<CharacterController2D>().Jump();
		exclamation.transform.parent = bunny.transform;
		exclamation.enabled = true;
		narrator.SetTextFont(narrator.NarrationFont, FontStyle.Normal);
		narrator.DisplayText("...so she ran away...", 1.0f);
		bunny.GetComponent<CharacterController2D>().walkSpeed = 4.0f;
		while (!isNarrationTriggered[2]) {
			isNarrationTriggered[2] = narrationTriggers[2].GetEvent();
			bunny.transform.rotation = Quaternion.identity;
			bunny.GetComponent<CharacterController2D>().Walk(CharacterController2D.Direction.RIGHT);
			yield return new WaitForEndOfFrame();
		}
		bunny.GetComponent<CharacterController2D>().Halt();
		GameObject.DestroyObject(bunny);
	}
	
	protected override IEnumerator CutScene() {
		while (!isCutSceneTriggered) {
			isCutSceneTriggered = cutsceneTriggers[0].GetEvent();
			if (isCutSceneTriggered) {
				cameraFade.FadeOut();
				yield return new WaitForSeconds(1.0f);
				Application.LoadLevel("Scene12");
			}
			yield return new WaitForEndOfFrame();
		}
	}
}
