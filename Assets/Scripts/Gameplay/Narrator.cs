using UnityEngine;
using System.Collections;

public class Narrator : GUIFade {
    public Font NarrationFont = null;
    public Font InstructionFont = null;

    private int fontSize = 0;

	void Awake() {
		GameObject.DontDestroyOnLoad(this);
        fontSize = GetComponent<GUIText>().fontSize;
    }

	public void DisplayText(string text, float timeDisplay = 0.0f) {
		CancelInvoke("FadeOut");
		FadeIn();
		GetComponent<GUIText>().text = text;
		if (timeDisplay > 0.0f) 
			Invoke("FadeOut", timeDisplay);
	}

	public void SetTextFont(Font font, FontStyle style = FontStyle.Normal, int overrideSize = -1) {
        GUIText text = GetComponent<GUIText>();
        text.font = font;
        text.fontStyle = style;
        text.fontSize = overrideSize != -1 ? overrideSize : fontSize;
    }

    protected override void Fade() {
		Color color = GetComponent<GUIText>().color;
		color.a = alpha;
		GetComponent<GUIText>().color = color;
	}

	protected override void Draw() {
	}
}
