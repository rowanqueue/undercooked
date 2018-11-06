using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//use: pick stuff up! put it down!
//loc: on pick up cube child of player
public class Pickup : MonoBehaviour {
    public string pickUpAxis;
    public bool holdingItem;
    public Item itemHeld;
    public bool counterHere;

    List<Item> itemsHere;
    Transform itemHoldingPos;
    Transform counterPos;
	// Use this for initialization
	void Start () {
        itemsHere = new List<Item>();
        itemHoldingPos = transform.GetChild(0);
	}
	
	// Update is called once per frame
	void Update () {
        //raycast to put items down on table
        Ray ray = new Ray(transform.position, transform.forward+Vector3.down*0.25f);
        Debug.DrawRay(ray.origin,ray.direction,Color.yellow);
        //pick up items
		if(itemsHere.Count > 0)
        {
            if (Input.GetButton(pickUpAxis))
            {
                holdingItem = !holdingItem;
                if (holdingItem)//pickup!
                {
                    itemHeld = itemsHere[0];
                }
                else
                {
                    if (counterHere)
                    {
                        itemHeld.transform.position = counterPos.position;
                        itemHeld.transform.parent = counterPos.parent;
                    }
                    itemHeld = null;
                }
            }
        }
        //keep item on you
        if (holdingItem)
        {
            itemHeld.transform.position = itemHoldingPos.position;
            itemHeld.rb.velocity = Vector3.zero;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Item"))
        {
            itemsHere.Add(other.transform.GetComponent<Item>());
        }
        if (other.tag.Equals("Counter"))
        {
            counterPos = other.transform.GetChild(0);
            counterHere = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Item"))
        {
            int i = 0;
            foreach(Item item in itemsHere)
            {
                if(item == itemHeld)
                {
                    continue;
                }
                if(item.gameObject == other.transform.gameObject)
                {
                    break;
                }
                i++;
            }
            itemsHere.RemoveAt(i);
        }
        if (other.tag.Equals("Counter"))
        {
            if(other.transform.GetChild(0) == counterPos)
            {
                counterPos = null;
                counterHere = false;
            }
        }
    }
}
