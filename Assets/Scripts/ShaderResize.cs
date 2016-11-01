using UnityEngine;
using System.Collections;

public class ShaderResize : GUIResize {
	protected override void Resize() {
		GetComponent<GUITexture>().pixelInset = new Rect(0.0f, 0.0f, Screen.width, Screen.height);
	}
}
