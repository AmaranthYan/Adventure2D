using UnityEngine;
using System.Collections;

public abstract class GUIResize : MonoBehaviour {
	void OnGUI() {
		Resize();
	}

	protected abstract void Resize();
}