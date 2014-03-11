using UnityEngine;
using System.Collections;

public class FlashSpotlight : MonoBehaviour {
    public float delay = 0;
    public float frequency = 0.75f;
    public float lightIntensity = 8;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        light.intensity = Mathf.Abs(Mathf.Sin(Mathf.PI * Time.timeSinceLevelLoad * frequency + delay)) * lightIntensity;
	}
}
