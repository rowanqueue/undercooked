using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//use: turn meat into COOKED MEAT
//loc: on stove object
public class Stove : MonoBehaviour {
    public Item itemHere;
    public float doneTime;//when item is done cooking
    public float burnTime;//when item is gonna BURN

    float startTime;
    bool cooking;
	// Update is called once per frame
	void Update () {
        if (cooking == false)//not cooking
        {
            if (itemHere != null)//woah we should start cooking
            {
                startTime = Time.time;
                doneTime = Time.time + 2;
                burnTime = Time.time + 3;
                cooking = true;
            }
        }
        else
        {
            if (itemHere != null) //Item is here and cooking
            {
                itemHere.percentToNextLevel += Time.deltaTime;
            }
            else //hey item isn't here anymore
            {
                cooking = false;
            }
            if (Time.time > doneTime)
            {
                itemHere = null;
            }
        }
	}
}
