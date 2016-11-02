using UnityEngine;
using System;

public class LevelSelector : MonoBehaviour {
    [SerializeField]
    private Camera gameCamera;

    public void LoadLevel(string levelName)
    {
        Application.LoadLevel(levelName);
        gameCamera.GetComponent<CameraFade>().enabled = true;
        gameCamera.GetComponent<CameraTrack>().enabled = true;
    }
}
