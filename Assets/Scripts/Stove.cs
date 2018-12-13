using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//use: turn meat into COOKED MEAT
//loc: on stove object
public class Stove : Counter
{
    public AudioSource grillSoundSource;
    public AudioClip grilling;

    private void Start()
    {
        grillSoundSource = GetComponent<AudioSource>();
    }

    public override void Update () {
        base.Update();
        if (itemHere is Pan)
        {
            Pan pan = (Pan)itemHere;
            if (pan.cooking != null)
            {
                
                pan.cooking.percentToNextLevel += Time.deltaTime;
                if (grillSoundSource.isPlaying==false)
                {
                    grillSoundSource.Play();
                    grillSoundSource.volume = .5f;
                }
            }

            if (pan.cooking == null)
            {
                grillSoundSource.Stop();
                pan.cooking.percentToNextLevel += Time.deltaTime/10f;
            }
        }
	}
}
