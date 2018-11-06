using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
	public int type;//0:nonfood,1:food to be chopped,2:food to be cooked,3:food to be boiled,4:food to be combined,5:base
                    //0 is pan, pot, fire extinguisher
                    //1 is raw lettuce, meat, tomatoes
                    //2 is chopped meat
                    //4 is cooked meat, chopped lettuce/meat/tomatoes
                    //5 is bun, plate
                    //recipe: pbmlt
    public string name;

    public Item(int t, string n)
    {
        type = t;
        name = n;
    }

    public Rigidbody rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool Equals(Item i)
    {
        return i.name.Equals(name) && i.type == type;
    }
}
