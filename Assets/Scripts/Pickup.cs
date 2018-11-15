﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//use: pick stuff up! put it down!
//loc: on pick up cube child of player
public class Pickup : MonoBehaviour {
    public string pickUpAxis;
    public string myPlayerName;

    public Item itemHeld;//what item you're holding

    public Vector3 holdingPos;//where items are held in front of you
	// Use this for initialization
	void Start () {
	}

    // Update is called once per frame
    void Update() {
        if (itemHeld != null)//holding an item
        {
            itemHeld.transform.position = transform.position + transform.forward;
        }
        //this code just looks at what you're seeing
        Ray ray = new Ray(transform.position, transform.forward);
        DisplayRay(ray, 1.5f, 1.5f);
        RaycastHit[] hits = Physics.SphereCastAll(ray, 1.5f, 1.5f);
        Item potentialItem = null;//where we locally store an item we could pick up
        Transform potentialCounter = null;//storing a potentialCountertop
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider == null) { continue; }
            if (hit.collider.tag == "Item")
            {
                if (potentialItem != null)//there's multiple items, oh no! check which one is closer and keep that as the one you interact with
                {
                    Item newPotential = hit.collider.GetComponent<Item>();
                    if (newPotential != itemHeld)//no worries about this new guy being the guy you're holding
                    {
                        float newDistance = Vector3.Distance(transform.position, newPotential.transform.position);
                        float oldDistance = Vector3.Distance(transform.position, potentialItem.transform.position);
                        if (oldDistance > newDistance)//new item is closer? its now the main one you interact with
                        {
                            potentialItem = newPotential;
                        }
                    }
                }
                else
                {
                    potentialItem = hit.collider.GetComponent<Item>();
                    if (potentialItem == itemHeld)
                    {
                        potentialItem = null;
                    }
                }
            }
            if (hit.collider.tag == "Counter")
            {
                if(potentialCounter != null)
                {
                    Transform newPotential = hit.collider.transform;
                    float newDistance = Vector3.Distance(transform.position, newPotential.position);
                    float oldDistance = Vector3.Distance(transform.position, potentialCounter.position);
                    if(oldDistance > newDistance)
                    {
                        potentialCounter = newPotential;
                    }
                }
                else
                {
                    potentialCounter = hit.collider.transform;
                }
            }
        }
        if (Input.GetButtonDown(pickUpAxis + myPlayerName))
        {
            if(itemHeld != null)//you're holding an item, now gonna drop it
            {
                bool placed = false;
                if(potentialItem != null)//combine items
                {
                    //weird shit with combining
                    if(potentialItem is Plate)
                    {
                        Plate plate = (Plate)potentialItem;
                        placed = plate.plated.Add(itemHeld);
                        Destroy(itemHeld.gameObject);
                        itemHeld = null;
                        Debug.Log(placed);
                    }
                }
                if (placed == false && potentialCounter != null)//place item on counter
                {
                    itemHeld.transform.position = potentialCounter.position + Vector3.up * 0.75f;

                    //see if you're putting it on a stove
                    Stove stove = potentialCounter.GetComponent<Stove>();
                    if(stove != null)
                    {
                        stove.itemHere = itemHeld;
                    }
                    //see if you're putting it on the serving counter
                    ServingCounter servingCounter = potentialCounter.GetComponent<ServingCounter>();
                    if(servingCounter != null)
                    {
                        if(potentialItem is Plate)
                        {
                            servingCounter.Serve((Plate)potentialItem);
                        }
                    }
                    itemHeld = null;
                    placed = true;
                }
                if(placed == false)
                {
                    itemHeld.rb.isKinematic = false;
                    itemHeld = null;
                }
            }
            else//you're holding nothing
            {
                if (potentialItem != null)//you're looking at an item
                {
                    Debug.Log("picked up");
                    itemHeld = potentialItem;
                    itemHeld.rb.isKinematic = true;
                    itemHeld.transform.position = transform.position + transform.forward;
                }
            }
        }

    }
    void DisplayRay(Ray ray,float radius,float distance)
    {
        //Debug.DrawRay(ray.origin, ray.direction, Color.red);
        Debug.DrawLine(ray.origin, ray.origin+ray.direction*distance,Color.red);
    }

}
