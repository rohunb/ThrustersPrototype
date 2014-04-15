using UnityEngine;
using System.Collections;

public class ScreenFader : MonoBehaviour {
    public enum ScreenState
    {
        Starting,
        Ending,
        None
    }

    private static ScreenState currentState = ScreenState.Starting;
    private static bool isTransitioning = false;

	// Use this for initialization
	void Start () {
        guiTexture.pixelInset = new Rect(0, 0, Screen.width, Screen.height);
	}
}
