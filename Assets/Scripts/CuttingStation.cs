using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingStation : Counter {
    public bool canBeUsed;
    public bool isCutting;
    private AudioSource chopsound;
    public AudioClip chop;

    private void Start()
    {
        chopsound = GetComponent<AudioSource>();
    }
    public override void Update()
    {
        base.Update();
        if (itemHere != null && itemHere.state.Equals("raw"))//can be cut
        {
            canBeUsed = true;
        }
        else
        {
            canBeUsed = false;
            isCutting = false;
        }
        if (isCutting)
        {
            
            itemHere.percentToNextLevel += Time.deltaTime;
            if (chopsound.isPlaying==false)
            {
                chopsound.Play();
                chopsound.volume = .35f;
            }
            
            if(itemHere.percentToNextLevel >= 1)
            {
                GameObject chopped = Instantiate(itemHere.turnsInto, itemHere.transform.position, itemHere.transform.rotation);
                Destroy(itemHere.gameObject);
                itemHere = chopped.GetComponent<Item>();
                isCutting = false;
            }
        }
    }
}
