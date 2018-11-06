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
    public string name;//meat, bun, etc.
    public float percentToNextLevel;//what percent chopped? what percent cooked?

    public Rigidbody rb;
    public string turnInto;//what type it turns into, so lettuce is 14, meat is 124
    public Item(int t, string s)
    {
        this.type = t;
        this.name = s;
    }
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		if(percentToNextLevel >= 1)
        {
            int index = turnInto.IndexOf(type.ToString());
            if(index+1 < turnInto.Length)
            {
                type = turnInto[index + 1];
            }
        }
	}

    public bool Equals(Item i)
    {
        return name.Equals(i.name) && type == i.type;
    }
}
