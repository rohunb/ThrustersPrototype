using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class AudioEngine : MonoBehaviour {

	//Global Audio shit
	private static float _volume;
	private static float _beforeVolume;
	private static bool _mute;

	public Queue<AudioSource> queueASource;
	private AudioSource audioSource;
	private AudioSource audioSource1;
	private AudioSource audioSource2;
	private AudioSource newAudioSource;

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
		queueASource = new Queue<AudioSource>();
		AudioSource[] aSource = GetComponents<AudioSource>();
		audioSource = aSource[0];
		audioSource1 = aSource[1];
		audioSource2 = aSource[2];

		string[] audioTags = new string[] { 
			"Laser", "MissleLaunch", "Torpedo", "Railgun", "MiningLaser", "missionTerminalEnd", "missionTerminalEnter", "TerminalEnter", "TerminalExit", "TerminalBtnYes", "TerminalBtn", "MenuPlayBtn"
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

		//checkAudio();
		StartCoroutine("checkQueue");

		if(Application.loadedLevelName == "MainMenu") {
			if(!isSfxPlaying) {
				bgSFX = Resources.Load<AudioClip>("Audio/Background/IntroBG");
				audioSource1.loop = true;
				audioSource1.clip = bgSFX;
				audioSource1.Play();
				isSfxPlaying = true;
			}
		} else if(Application.loadedLevelName == "GameScene") {
			if(!isSfxPlaying1) {
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
				bgSFX = Resources.Load<AudioClip>("Audio/Background/DockBg");
				audioSource1.loop = true;
				audioSource1.clip = bgSFX;
				audioSource1.Play();
			//	audioSource1.Stop();
				audioSource2.Stop();
				isSfxPlaying2 = true;
			}
		} else if(Application.loadedLevelName == "FTLScene") {
			if(!isSfxPlaying3) {
				bgSFX = Resources.Load<AudioClip>("Audio/Background/FTLBg");
				audioSource1.loop = true;
				audioSource1.clip = bgSFX;
				audioSource1.Play();
			//	audioSource1.Stop();
				audioSource2.Stop();
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
			//audioSource.PlayOneShot(currentSFX);
			//Debug.Log("Current SFX name:" + currentSFX);
			newAudioSource = gameObject.AddComponent("AudioSource") as AudioSource;
			newAudioSource.playOnAwake = false;
			newAudioSource.loop = false;
			AudioClip newAudioClip = currentSFX;
			newAudioSource.clip = currentSFX;
			queueASource.Enqueue(newAudioSource);
			//Debug.Log("Shoot");
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

	public IEnumerator checkQueue() {
		Debug.Log("Running Queue");
		if (queueASource.Count != 0) {
			if(!queueASource.Peek().isPlaying){
				queueASource.Peek().Play();
				Debug.Log("Play");
				yield return new WaitForSeconds(queueASource.Peek().clip.length);
				Destroy(queueASource.Peek());
				queueASource.Dequeue();
				Debug.Log("Deqeue");
			}
		}

//		if (queueASource.Count != 0) {
//			queueASource.Peek().Play();
//			Debug.Log("Play");
//			yield return new WaitForSeconds(queueASource.Peek().clip.length);
//			queueASource.Dequeue();
//		}
//		foreach (AudioSource aS in queueASource) {
//			aS.Play();
//			Debug.Log("playing audio" + aS.clip.length);
//			yield return new WaitForSeconds(aS.clip.length);
//			queueASource.Remove(aS);
//		}
	}

//	public void checkAudio() {
//		if (queueASource.Count != 0) {
//			if(!queueASource.Peek().isPlaying){
//				queueASource.Peek().PlayOneShot(queueASource.Peek().clip);
//				Debug.Log("Play");
//				Debug.Log(queueASource.Peek());
//				audioPlay = true;
//				Destroy(queueASource.Peek());
//				queueASource.Dequeue();
//				Debug.Log("Deqeue");
//			} 
//
////			else if(queueASource.Peek().isPlaying && queueASource.Peek().clip.length == 0.0) {
////				Destroy(queueASource.Peek());
////				queueASource.Dequeue();
////				Debug.Log("Deqeue");
////			}
//		}
//	}
}
