using UnityEngine;
using System.Collections;

public class CameraFade : GUIFade {
	public Texture shader;

	protected override void Fade() {
		GUI.color = new Color(0.0f, 0.0f, 0.0f, 1.0f - alpha);
	}

	protected override void Draw() {
		GUI.DrawTexture(new Rect(0.0f, 0.0f, Screen.width, Screen.height), shader, ScaleMode.StretchToFill, true, 0.0f);
	}
}
