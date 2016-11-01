using UnityEngine;
using System.Collections;

public class Scene7 : Scene {
	protected GameObject puppy;
	private static int pass = 0;
	private static bool fail = false;
	private static Vector3[] triggerPositions = new Vector3[2];
	private static bool isInitialized = false;

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
		puppy.transform.position = new Vector3(-6.95f, -0.125f, 0.0f);
		puppy.GetComponent<CharacterController2D>().defaultDirection = CharacterController2D.Direction.RIGHT;
		puppy.GetComponent<CharacterController2D>().ResetCurrentDirection();
        Camera.main.transform.position = new Vector3(-0.8f, 0.0f, -10.0f);
        cameraTrack.leftBound = -8.0f;
		cameraTrack.rightBound = 8.0f;
		if (!isInitialized) {
			triggerPositions[0] = cutsceneTriggers[0].transform.position;
			triggerPositions[1] = cutsceneTriggers[1].transform.position;
			isInitialized = true;
		}
		if (!fail)
			if (Random.value >= 0.5f)
				ExchangePosition(ref triggerPositions[0], ref triggerPositions[1]);
		cutsceneTriggers[0].transform.position = triggerPositions[0];
		cutsceneTriggers[1].transform.position = triggerPositions[1];
	}
	
	protected override IEnumerator Narration() {
		cameraFade.FadeIn();
		yield return new WaitForSeconds(1.5f);
		narrator.SetTextFont(narrator.NarrationFont, FontStyle.Normal);
		if ((pass == 0)  && !fail) {
			narrator.DisplayText("Puppy followed bunny into the cavern...", 1.5f);
			yield return new WaitForSeconds(2.5f);
			narrator.DisplayText("... but he got lost every now and then...", 1.5f);
		} else if (fail)
			narrator.DisplayText("\"Maybe I should leave a mark somehow...\" he thought...", 2.0f);
	}
	
	protected override IEnumerator CutScene() {
		while (!isCutSceneTriggered) {
			if (cutsceneTriggers[0].GetEvent()) {
				isCutSceneTriggered = true;
				cameraFade.FadeOut();
				yield return new WaitForSeconds(1.0f);
				scriptController.DisableAbility(puppy);
				GameObject wall = GameObject.Find("Wall");
				if (wall != null)
					GameObject.DestroyObject(wall);
				scriptController.EnableAbility(puppy);
				fail = false;
				if (pass < 2) {
					pass++;
					Application.LoadLevel(Application.loadedLevel);
				} else {
					Application.LoadLevel("Scene8");
				}
			}
			if (cutsceneTriggers[1].GetEvent()) {
				isCutSceneTriggered = true;
				cameraFade.FadeOut();
				yield return new WaitForSeconds(1.0f);
				scriptController.DisableAbility(puppy);
				GameObject wall = GameObject.Find("Wall");
				if (wall != null)
					GameObject.DontDestroyOnLoad(wall);
				scriptController.EnableAbility(puppy);
				fail = true;
				Application.LoadLevel(Application.loadedLevel);
			}
			yield return new WaitForEndOfFrame();
		}
	}

	private void ExchangePosition(ref Vector3 positionA, ref Vector3 positionB) {
		Vector3 buffer = positionA;
		positionA = positionB;
		positionB = buffer;
	}
}
