using UnityEngine;
using System.Collections;

public class TextResize : GUIResize {
	private const float FONT_SCREEN_HEIGHT_RATIO = 0.0764526f;

	protected override void Resize() {
		GetComponent<GUIText>().fontSize = (int)(Screen.height * FONT_SCREEN_HEIGHT_RATIO);
	}
}
