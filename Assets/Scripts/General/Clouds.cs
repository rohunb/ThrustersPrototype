using UnityEngine;
using System.Collections;

public class Clouds : MonoBehaviour
{
    public float scrollSpeed;

    void Update()
    {
        renderer.material.SetTextureOffset("_MainTex", new Vector2(Time.time * scrollSpeed, 0));
    }
}

//Kyle Nokes
