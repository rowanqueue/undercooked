using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//use: turn meat into COOKED MEAT
//loc: on stove object
public class Stove : Counter {
    public float burnTime;//when item is gonna BURN
    
    public bool cooking;
	// Update is called once per frame
	public override void Update () {
        base.Update();
        if (cooking == false)//not cooking
        {
            if (itemHere != null)//woah we should start cooking
            {
                burnTime = Time.time + 3;
                cooking = true;
            }
        }
        else//we're cooking now!!
        {
            if (itemHere != null && itemHere.state.Equals("chopped")) //Item is here and cooking  (also where add pan check)
            {
                itemHere.percentToNextLevel += Time.deltaTime;
            }
            else //hey item isn't here anymore
            {
                cooking = false;
            }
            if (Time.time > burnTime)
            {
                //fire would happen here
            }
        }
        if (itemHere != null && itemHere.percentToNextLevel > 1)
        {
            GameObject cooked = Instantiate(itemHere.turnsInto, itemHere.transform.position, itemHere.transform.rotation);
            Destroy(itemHere.gameObject);
            itemHere = cooked.GetComponent<Item>();
        }
	}
}
