using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class SoundController : MonoBehaviour {

	public static SoundController me;
	public GameObject audSource;
	public AudioSource[] audSources;
 	[Header("AudioClips")] 
	public AudioClip introSound;
	
	public AudioClip pickUpItem;//1
	public AudioClip dropitem;//2
	public AudioClip dropchoppeditem;//3
	public AudioClip grilling;//4
	public AudioClip burningMeatBeep;//5
	public AudioClip completeOrder;//6
	public AudioClip washingPlates;//7
	public AudioClip tenSecondBeepCountdown;//8
	public AudioClip endOfRoundBells;//9
	public AudioClip chopping;//10
	public AudioClip singleCountdownBeep;//11
	public AudioClip song;
	

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
		PlaySound(introSound,1);
		PlaySound(song,1);
		

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
		//audSources [sNum].pitch =// Time.timeScale;
		audSources [sNum].PlayOneShot(snd, vol);
	}

	public void PlaySound(AudioClip snd, float vol, float pitch)

	{
		//Debug.Log (snd);
		int sNum = GetSourceNum ();
		audSources [sNum].clip = snd;
		audSources [sNum].volume = vol;
		audSources[sNum].pitch = pitch; //* Time.timeScale;
		audSources [sNum].PlayOneShot(snd,vol);
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

				//audSources [i].pitch = Time.timeScale;
			}


		}

	}


	
	




}
