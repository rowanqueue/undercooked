using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Sounds : MonoBehaviour
{
	public static Sounds Me;
	[FormerlySerializedAs("CliptoPlay")] public int cliptoPlay;
	public bool chop;

	// Use this for initialization
	private void Awake()
	{
		Me = this;
	}

	
	
	// Update is called once per frame
	void Update () {
		NumPlaySounds();

		
	}
	
	void NumPlaySounds()
	{
		if (cliptoPlay == 1)
		{
			SoundController.Get().PlaySound(SoundController.Get().pickUpItem,.75f);
			cliptoPlay = 0;
		}
		if (cliptoPlay == 2)
		{
			SoundController.Get().PlaySound(SoundController.Get().dropitem,.75f);
			cliptoPlay = 0;
		}
		if (cliptoPlay == 3)
		{
			SoundController.Get().PlaySound(SoundController.Get().dropchoppeditem,1);
			cliptoPlay = 0;
		}
		if (cliptoPlay == 4)
		{
			SoundController.Get().PlaySound(SoundController.Get().grilling,.5f);
			cliptoPlay = 0;
		}
		if (cliptoPlay == 5)
		{
			SoundController.Get().PlaySound(SoundController.Get().burningMeatBeep,1);
			cliptoPlay = 0;
		}
		if (cliptoPlay == 6)
		{
			SoundController.Get().PlaySound(SoundController.Get().completeOrder,2);
			cliptoPlay = 0;
		}
		if (cliptoPlay == 7)
		{
			SoundController.Get().PlaySound(SoundController.Get().washingPlates,1);
			cliptoPlay = 0;
		}
		if (cliptoPlay == 8)
		{
			SoundController.Get().PlaySound(SoundController.Get().tenSecondBeepCountdown,1);
			cliptoPlay = 0;
		}
		if (cliptoPlay == 9)
		{
			SoundController.Get().PlaySound(SoundController.Get().endOfRoundBells,1);
			cliptoPlay = 0;
		}
		if (cliptoPlay == 10)
		{
			SoundController.Get().PlaySound(SoundController.Get().chopping,1);
			cliptoPlay = 0;
		}
	}
}
