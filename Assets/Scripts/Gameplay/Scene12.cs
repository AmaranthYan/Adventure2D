using UnityEngine;
using System.Collections;

public class Scene12 : Scene {
	protected GameObject puppy;
	protected GameObject bunny;
	[SerializeField]
	private Rope[] ropes;
	[SerializeField]
	private SwitchController crusherRod;

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
		puppy.transform.position = new Vector3(-9.4f, -1.755f, 0.0f);
		puppy.GetComponent<CharacterController2D>().defaultDirection = CharacterController2D.Direction.RIGHT;
		puppy.GetComponent<CharacterController2D>().ResetCurrentDirection();
		bunny.GetComponent<CharacterController2D>().defaultDirection = CharacterController2D.Direction.RIGHT;
		bunny.GetComponent<CharacterController2D>().ResetCurrentDirection();
		Camera.main.transform.position = new Vector3(-3.4f, 0.0f, -10.0f);
		cameraTrack.leftBound = -10.65f;
		cameraTrack.rightBound = 10.65f;
	}
	
	protected override IEnumerator Narration() {
		cameraFade.FadeIn();
		yield return new WaitForSeconds(0.5f);
		narrator.SetTextFont(narrator.NarrationFont, FontStyle.Normal);
		narrator.DisplayText("Puppy detemined to get her back...", 2.0f);
		while (!isNarrationTriggered[0]) {
			isNarrationTriggered[0] = narrationTriggers[0].GetEvent();
			bunny.GetComponent<CharacterController2D>().Walk(CharacterController2D.Direction.RIGHT);
			yield return new WaitForEndOfFrame();
		}
		ropes[0].Cut(4);
		yield return new WaitForSeconds(0.1f);
		ropes[1].Cut(3);
		while (!isNarrationTriggered[1]) {
			isNarrationTriggered[1] = narrationTriggers[1].GetEvent();
			bunny.GetComponent<CharacterController2D>().Walk(CharacterController2D.Direction.RIGHT);
			yield return new WaitForEndOfFrame();
		}
		narrator.SetTextFont(narrator.NarrationFont, FontStyle.Normal);
		narrator.DisplayText("...come hell and high water...", 1.5f);
		bunny.GetComponent<CharacterController2D>().Walk(CharacterController2D.Direction.LEFT);
		bunny.GetComponent<CharacterController2D>().Halt();
		while (!isNarrationTriggered[2]) {
			isNarrationTriggered[2] = narrationTriggers[2].GetEvent();
			yield return new WaitForEndOfFrame();
		}
		crusherRod.Switch();
		while (!isNarrationTriggered[3]) {
			isNarrationTriggered[3] = narrationTriggers[3].GetEvent();
			bunny.GetComponent<CharacterController2D>().Walk(CharacterController2D.Direction.RIGHT);
			yield return new WaitForEndOfFrame();
		}
		bunny.GetComponent<CharacterController2D>().Halt();
		GameObject.DestroyObject(bunny);
		while (!isNarrationTriggered[4]) {
			isNarrationTriggered[4] = narrationTriggers[4].GetEvent();
			yield return new WaitForEndOfFrame();
		}
		Scene.ReloadLevel();
	}
	
	protected override IEnumerator CutScene() {
		while (!isCutSceneTriggered) {
			isCutSceneTriggered = cutsceneTriggers[0].GetEvent();
			if (isCutSceneTriggered) {
				cameraFade.FadeOut();
				yield return new WaitForSeconds(1.0f);
				Application.LoadLevel("Scene13");
			}
			yield return new WaitForEndOfFrame();
		}
	}
}