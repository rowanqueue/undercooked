using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//use: turn meat into COOKED MEAT
//loc: on stove object
public class Stove : Counter {

	public override void Update () {
        base.Update();
        if (itemHere is Pan)
        {
            Pan pan = (Pan)itemHere;
            if (pan.cooking != null)
            {
                pan.cooking.percentToNextLevel += Time.deltaTime;
            }
        }
	}
}
