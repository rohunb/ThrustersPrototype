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
	
	// Update is called once per frame
	void Update () {
        if (guiTexture.color.a > 0.01f && guiTexture.color.a < 0.99f)
            isTransitioning = true;
        else
            isTransitioning = false;
            
        if (currentState == ScreenState.Starting)
        {
            isTransitioning = true;
            FadeOut();

            if (guiTexture.color.a <= 0.01f)
            {
                guiTexture.color = Color.clear;
                guiTexture.enabled = false;

                currentState = ScreenState.None;
                isTransitioning = false;
            }
        }
        if (currentState == ScreenState.Ending)
        {
            guiTexture.enabled = true;
            isTransitioning = true;

            FadeIn();

            if (guiTexture.color.a >= 0.99f)
            {
                guiTexture.color = Color.black;
                isTransitioning = false;
            }
        }
	}

    //might change to smoothstep instead
    void FadeOut()
    {
        guiTexture.color = Color.Lerp(guiTexture.color, Color.clear, 2f * Time.deltaTime);
    }

    void FadeIn()
    {
        guiTexture.color = Color.Lerp(guiTexture.color, Color.black, 2f * Time.deltaTime);
    }

    public static void StartScene()
    {
        currentState = ScreenState.Starting;
    }

    public static void EndScene()
    {
        currentState = ScreenState.Ending;
    }

    public static bool IsTransitioning
    {
        get { return isTransitioning; }
    }
}
