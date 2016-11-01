using UnityEngine;
using System.Collections;

public abstract class GUIFade : MonoBehaviour {
	protected float alpha = 0.0f;
	public float fadeSpeed = 1.0f;
	protected enum Mode {FadeOut = -1, OnHold = 0, FadeIn = 1};
	protected Mode currentMode = Mode.OnHold;

	void Start() {

	}

	void Update() {
		alpha += (int)currentMode * Mathf.Clamp01(Time.deltaTime * fadeSpeed);
		if ((alpha <= 0.0f) || (alpha >= 1.0f)) {
			OnFadingComplete();
			Stop();
		}
	}

	void OnGUI() {
		Fade();
		Draw();
	}

	protected virtual void Stop() {
		currentMode = Mode.OnHold;
	}
	
	public void FadeIn() {
		alpha = 0.0f;
		currentMode = Mode.FadeIn;
		OnFadingStart();
	}

	public void FadeOut() {
		alpha = 1.0f;
		currentMode = Mode.FadeOut;
		OnFadingStart();
	}

	protected abstract void Fade();

	protected abstract void Draw();

	protected virtual void OnFadingStart() {
		Fade();
	}

	protected virtual void OnFadingComplete() {

	}
}
