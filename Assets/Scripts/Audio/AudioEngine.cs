using UnityEngine;
using System.Collections;

public class AudioEngine : MonoBehaviour {

	//Global Audio shit
	private static float _volume;
	private static float _beforeVolume;
	private static bool _mute;
	private AudioSource audioSource;

	//Sound Effect shit
	//public AudioClip lazer1;
	public AudioClip currentSFX;

	// Use this for initialization
	void Start () {
        audioSource = gameObject.GetComponent<AudioSource>();
		_volume = 1.0f;
		_beforeVolume = 0.0f;
		_mute = false;
		AudioListener.volume = _volume;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.End)) {
			isMute();
		}
		if (Input.GetKeyDown(KeyCode.KeypadMinus)) {
			isVolumeDown();
		}
		if (Input.GetKeyDown(KeyCode.KeypadPlus)) {
			if(_volume > 1.0f) {
				Debug.Log("Volume is at max");
			} else {
				isVolumeUp();
			}
		}
        //playSFX();
	}

	public void isVolumeDown() {
		if(_volume < 0) {
			_mute = true;
			Debug.Log("Now muted");
		} else {
			_volume -= 0.10f;
			AudioListener.volume = _volume;
			Debug.Log("Volume -1");
		}
	}

	public void isVolumeUp() {
		if(_mute) {
			_mute = false;
			_volume += 0.10f;
			AudioListener.volume = _volume;
			Debug.Log("Volume +1 | Now unmuted");
		} else {
			_volume += 0.10f;
			AudioListener.volume = _volume;
			Debug.Log("Volume +1");
		}
	}

	public void isMute() {
		if(!_mute) {
			_mute = true;
			_beforeVolume = _volume;
			AudioListener.volume = 0.0f;
			Debug.Log("MUTE!");
		} else {
			_mute = false;
			AudioListener.volume = _beforeVolume;
			Debug.Log("UNMUTE!");
		}
	}

	public void playSFX(string snd) {
       
		Debug.Log(snd);
		string newpath = "Audio/Effects/"+snd;
        //string newpath = "Audio/Effects/Laser";
		currentSFX = Resources.Load<AudioClip>(newpath);
        //currentSFX = Resources.Load(newpath) as AudioClip;
		Debug.Log(currentSFX);
        audioSource.PlayOneShot(currentSFX);
        //audioSource.clip = currentSFX;
		//audioSource.Play();
        
        
        //currentSFX
	}
}
