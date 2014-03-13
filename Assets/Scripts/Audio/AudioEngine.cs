using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class AudioEngine : MonoBehaviour {

	//Global Audio shit
	private static float _volume;
	private static float _beforeVolume;
	private static bool _mute;
	private AudioSource audioSource;
	private AudioSource audioSource1;
	private AudioSource audioSource2;

	//Sound Effect shit
	private AudioClip currentSFX;
	private AudioClip bgSFX;
	private AudioClip shipSFX;

	//Dictionary of shit
	public Dictionary<string, AudioClip> audionames; 

	// Use this for initialization
	void Start () {

		_volume = 0.75f;
		_beforeVolume = 0.0f;
		_mute = false;
		AudioListener.volume = _volume;
		AudioSource[] aSource = GetComponents<AudioSource>();
		audioSource = aSource[0];
		audioSource1 = aSource[1];
		audioSource2 = aSource[2];

		//audioSource = gameObject.GetComponent<AudioSource>();

		//Dictionary
		string audioPath = "Audio/Effects/";
		audionames = new Dictionary<string, AudioClip>();

		currentSFX = Resources.Load<AudioClip>(audioPath+"Laser");
		audionames.Add("Laser", currentSFX);

		currentSFX = Resources.Load<AudioClip>(audioPath+"MissleLaunch");
		audionames.Add("MissleLaunch", currentSFX);

		currentSFX = Resources.Load<AudioClip>(audioPath+"Torpedo");
		audionames.Add("Torpedo", currentSFX);

		currentSFX = Resources.Load<AudioClip>(audioPath+"Railgun");
		audionames.Add("Railgun", currentSFX);

		currentSFX = Resources.Load<AudioClip>(audioPath+"MiningLaser");
		audionames.Add("MiningLaser", currentSFX);

		//
		bgSFX = Resources.Load<AudioClip>("Audio/Background/InGameBG");
		shipSFX = Resources.Load<AudioClip>(audioPath+"Thrusters");
		audioSource1.loop = true;
		audioSource1.clip = bgSFX;
		audioSource1.Play();
		audioSource2.loop = true;
		audioSource2.clip = shipSFX;
		audioSource2.Play();
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
	}

	public void isVolumeDown() {
		if(_volume < 0) {
			_mute = true;
			Debug.Log("Now muted");
		} else {
			_mute = false;
			_volume -= 0.10f;
			AudioListener.volume = _volume;
			Debug.Log("Volume -0.1");
		}
	}

	public void isVolumeUp() {
		if(_mute) {
			_mute = false;
			_volume += 0.10f;
			AudioListener.volume = _volume;
			Debug.Log("Volume +0.1 | Now unmuted");
		} else {
			_mute = false;
			_volume += 0.10f;
			AudioListener.volume = _volume;
			Debug.Log("Volume +0.1");
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
		if (audionames.ContainsKey(snd)) {
			currentSFX = audionames[snd];
			audioSource.PlayOneShot(currentSFX);
			//Debug.Log("Audio File played");
		} else {
			Debug.Log("Audio File failed to play!");
		}

	}
}
