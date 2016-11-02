using UnityEngine;
using System.Collections;

public abstract class Scene : MonoBehaviour {
	protected const int MOUSE_LEFT_BUTTON = 0;
	protected const int MOUSE_RIGHT_BUTTON = 1;
	[SerializeField]
	protected EventTrigger[] narrationTriggers;
	protected bool[] isNarrationTriggered;
	[SerializeField]
	protected EventTrigger[] cutsceneTriggers;
	protected bool isCutSceneTriggered;
    [SerializeField]
    protected GameObject[] instantiatedSceneObjects;

    protected CameraFade cameraFade;
    protected CameraTrack cameraTrack;
    protected Narrator narrator;
    protected ScriptController scriptController;
    protected CharacterSwitch characterSwitch;

    protected virtual void Awake() {
        cameraFade = Camera.main.GetComponent<CameraFade>();
        cameraTrack = Camera.main.GetComponent<CameraTrack>();
        narrator = FindObjectOfType<Narrator>();
        scriptController = GameObject.Find("Characters").GetComponent<ScriptController>();
        characterSwitch = GameObject.Find("Characters").GetComponent<CharacterSwitch>();
    }

	void Start() {
		Initialize();
		StartCoroutine(Narration());
		StartCoroutine(CutScene());
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.R))
			Restart();
		if (Input.GetKeyDown(KeyCode.Escape))
			Application.Quit();
	}

	void OnDestroy() {
		StopCoroutine(Narration());
		StopCoroutine(CutScene());
	}

	protected virtual void Initialize() {
		isNarrationTriggered = new bool[narrationTriggers.Length];
		for (int i = 0; i < isNarrationTriggered.Length; i++)
			isNarrationTriggered[i] = false;
		isCutSceneTriggered = false;
	}

	protected abstract IEnumerator Narration();

	protected abstract IEnumerator CutScene();

	protected virtual void Restart() {
		ReloadLevel();
	}

	public static void ReloadLevel() {
		Application.LoadLevel(Application.loadedLevel);
	}
}
