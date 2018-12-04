using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour {

    public Item itemHere;//item sitting on this counter
    public Transform counterPos;//where item sits on the counter
	
	public virtual void Update () {
        if (itemHere != null && itemHere.gameObject.transform.position != counterPos.position)
        {
            itemHere.gameObject.transform.position = counterPos.position;
        }
	}
}
