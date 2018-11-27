using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour {

    public Item itemHere;
    public Vector3 counterPos;
	
	void Update () {
        if (itemHere != null && itemHere.gameObject.transform.position != counterPos)
        {
            itemHere.gameObject.transform.position = counterPos;
        }
	}
}
