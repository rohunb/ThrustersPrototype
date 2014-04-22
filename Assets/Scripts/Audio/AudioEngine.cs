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

	private bool isSfxPlaying;
	private bool isSfxPlaying1;
	private bool isSfxPlaying2;
	private bool isSfxPlaying3;

	private bool audioPlay;

	//Dictionary of shit
	public Dictionary<string, AudioClip> audionames; 

	// Use this for initialization
	void Start () {

		_volume = 0.75f;
		_beforeVolume = 0.0f;
		_mute = false;
		isSfxPlaying = false;
		isSfxPlaying1 = false;
		isSfxPlaying2 = false;
		isSfxPlaying3 = false;
		AudioListener.volume = _volume;
		AudioSource[] aSource = GetComponents<AudioSource>();
		audioSource = aSource[0];
		audioSource1 = aSource[1];
		audioSource2 = aSource[2];

		string[] audioTags = new string[] { 
			"Thrusters", "Laser", "MissleLaunch", "Torpedo", "Railgun", "MiningLaser", "missionTerminalEnd", "missionTerminalEnter", "TerminalEnter", "TerminalExit", "TerminalBtnYes", "TerminalBtn", "MenuPlayBtn", "Warning", "FtlMove", "FtlAlert", "FtlFlame", "Walk", "clusterExplosion", "Target", "FtlBtn", "ShipExplosion", "gameOverShipExplosion", "Missionsuccess", "Missionfailed"
		};

		//Dictionary
		string audioPath = "Audio/Effects/";
		audionames = new Dictionary<string, AudioClip>();

		foreach (string a in audioTags) {
			currentSFX = Resources.Load<AudioClip>(audioPath+a);
			audionames.Add(a, currentSFX);
		}
	}
	
	// Update is called once per frame
	void Update () {

		if(Application.loadedLevelName == "MainMenu") {
			if(!isSfxPlaying) {
				isSfxPlaying1 = false;
				isSfxPlaying2 = false;
				isSfxPlaying3 = false;
				audioSource1.Stop();
				audioSource2.Stop();
				bgSFX = Resources.Load<AudioClip>("Audio/Background/IntroBG");
				audioSource1.loop = true;
				audioSource1.clip = bgSFX;
				audioSource1.Play();
				isSfxPlaying = true;
			}
		} else if(Application.loadedLevelName == "GameScene") {
			if(!isSfxPlaying1) {
				isSfxPlaying = false;
				isSfxPlaying2 = false;
				isSfxPlaying3 = false;
				audioSource1.Stop();
				audioSource2.Stop();
				bgSFX = Resources.Load<AudioClip>("Audio/Background/InGameBG");
				shipSFX = Resources.Load<AudioClip>("Audio/Effects/Thrusters");
				audioSource1.loop = true;
				audioSource1.clip = bgSFX;
				audioSource1.Play();
				audioSource2.loop = true;
				audioSource2.clip = shipSFX;
				audioSource2.Play();
				isSfxPlaying1 = true;
			}
		} else if(Application.loadedLevelName == "DockedScene") {
			if(!isSfxPlaying2) {
				isSfxPlaying = false;
				isSfxPlaying1 = false;
				isSfxPlaying3 = false;
				audioSource1.Stop();
				audioSource2.Stop();
				bgSFX = Resources.Load<AudioClip>("Audio/Background/DockBg");
				audioSource1.loop = true;
				audioSource1.clip = bgSFX;
				audioSource1.Play();
				isSfxPlaying2 = true;
			}
		} else if(Application.loadedLevelName == "FTLScene") {
			if(!isSfxPlaying3) {
				isSfxPlaying = false;
				isSfxPlaying1 = false;
				isSfxPlaying3 = false;
				audioSource1.Stop();
				audioSource2.Stop();
				bgSFX = Resources.Load<AudioClip>("Audio/Background/FTLBg");
				audioSource1.loop = true;
				audioSource1.clip = bgSFX;
				audioSource1.Play();
				isSfxPlaying3 = true;
			}
		} else if(Application.loadedLevelName == "GameOverScene") {
			if(!isSfxPlaying3) {
				isSfxPlaying = false;
				isSfxPlaying1 = false;
				isSfxPlaying3 = false;
				audioSource1.Stop();
				audioSource2.Stop();
				bgSFX = Resources.Load<AudioClip>("Audio/Background/EndBg");
				audioSource1.loop = true;
				audioSource1.clip = bgSFX;
				audioSource1.Play();
				isSfxPlaying3 = true;
			}
		}

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

	public void setAllAudioVolume(float _musicVolume, float _sfxVolume, float _shipVolume) {
		audioSource.volume = _sfxVolume;
		audioSource1.volume = _musicVolume;
		audioSource2.volume = _shipVolume;
		Debug.Log("SFX Volume is at:" + _sfxVolume);
		Debug.Log("Music Volume is at:" + _musicVolume);
		Debug.Log("Ship Volume is at:" + _shipVolume);
	}
}
