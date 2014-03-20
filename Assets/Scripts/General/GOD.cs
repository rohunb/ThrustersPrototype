using UnityEngine;
using System.Collections;

public class GOD : MonoBehaviour {

	public static AudioEngine audioengine;

	// Use this for initialization
	void Start () {
		audioengine = (AudioEngine)FindObjectOfType(typeof(AudioEngine));
	}
	

}
