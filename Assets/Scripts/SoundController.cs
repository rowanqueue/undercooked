using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {

	public static SoundController me;
	public GameObject audSource;
	public AudioSource[] audSources;
	[Header("AudioClips")] 
	public AudioClip singleCountdownBeep;
	public AudioClip tenSecondBeepCountdown;
	public AudioClip chopping;
	public AudioClip completeOrder;
	public AudioClip washingPlates;
	public AudioClip endOfRoundBells;
	public AudioClip grilling;
	public AudioClip introSound;
	public AudioClip burningMeatBeep;
	public AudioClip pickUpItem;
	public AudioClip dropitem;
	public AudioClip dropchoppeditem;

	void Update() {

		//FixSoundSpeeds ();

	}

	void Awake(){
		me = this;
	}
		


	void Start () {
		audSources = new AudioSource[32];

		for (int i = 0; i < audSources.Length; i++) {

			AudioSource aSource = (Instantiate (audSource, Vector3.zero, Quaternion.identity) as GameObject).GetComponent<AudioSource>();
			audSources [i] = aSource;
			aSource.gameObject.transform.parent = this.transform;


		}

	}

	public static SoundController Get() {
		if (me == null) {
			me = (SoundController)FindObjectOfType(typeof(SoundController));
		}

		return me;
	}


	public void PlaySound(AudioClip snd, float vol)

	{
		//		Debug.Log (snd);
		int sNum = GetSourceNum ();
		audSources [sNum].clip = snd;
		audSources [sNum].volume = vol;
		audSources [sNum].pitch = Time.timeScale;
		audSources [sNum].Play ();
	}

	public void PlaySound(AudioClip snd, float vol, float pitch)

	{
		//Debug.Log (snd);
		int sNum = GetSourceNum ();
		audSources [sNum].clip = snd;
		audSources [sNum].volume = vol;
		audSources [sNum].pitch = pitch * Time.timeScale;
		audSources [sNum].Play ();
	}


	public int GetSourceNum()
	{

		for (int i = 0; i < audSources.Length; i++)
		{
			if(!audSources[i].isPlaying)
				return i;
		}
		return 0;
	}

	public void FixSoundSpeeds() {

		for (int i = 0; i < audSources.Length; i++) {

			if (audSources [i].isPlaying) {

				audSources [i].pitch = Time.timeScale;
			}


		}




	}




}
