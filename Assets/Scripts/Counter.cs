using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour {

    public Item itemHere;//item sitting on this counter
    public Vector3 counterPos;//where item sits on the counter
	
	public virtual void Update () {
        if (itemHere != null && itemHere.gameObject.transform.position != counterPos)
        {
            itemHere.gameObject.transform.position = counterPos;
        }
	}
}
