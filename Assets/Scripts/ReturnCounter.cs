using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnCounter : Counter {
    public static ReturnCounter me;//making a singleton so you can easily access and know there is only one
    public int numPlates;//how many plates here (will only show 1 plate rn)
	// Use this for initialization
	void Awake () {
        me = this;
	}
    public void ReturnPlate()//this should be called by servingcounter to place a new plate here
    {
        numPlates++;
        if(numPlates == 1)//first plate here
        {
            GameObject obj = (GameObject)Instantiate(Resources.Load("Items/Plate"), transform) as GameObject;
            obj.transform.position = counterPos.position;
            itemHere = obj.GetComponent<Item>();
        }
    }
    public bool GetPlate()
    {
        bool receivePlate = false;
        if(numPlates >= 1)//enough plates to get one
        {
            numPlates--;
            receivePlate = true;
        }
        return receivePlate;
    }
}
