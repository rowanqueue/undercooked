using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
	public int type;//0:nonfood,1:food to be chopped,2:food to be cooked,3:food to be boiled,4:food to be combined,5:done
                    //so like a burger with just meat is 0(plate)+1-2-4(meat)+4(bread)=5 or 0+1-2-4+4=5
    public Rigidbody rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
