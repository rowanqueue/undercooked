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
		if(cooking == false)
        {
            if (transform.childCount > 1 && itemHere == null)
            {
                Item item = GetComponentInChildren<Item>();
                if (item != null)
                {
                    itemHere = item;
                }
            }
            if (itemHere != null)
            {
                startTime = Time.time;
                doneTime = Time.time + 2;
                burnTime = Time.time + 3;
                cooking = true;
            }
        }
        else
        {
            if(itemHere == null)
            {
                cooking = false;
            }
            if(Time.time > doneTime)
            {
                itemHere.percentToNextLevel += Time.deltaTime;
            }
        }

	}
}
