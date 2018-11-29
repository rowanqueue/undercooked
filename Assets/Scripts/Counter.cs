using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour {

    public Item itemHere;
    public Transform counterPos;
	
	void Update () {
        if (itemHere != null && itemHere.gameObject.transform.position != counterPos.position)
        {
            itemHere.gameObject.transform.position = counterPos.position;
        }
	}
}
