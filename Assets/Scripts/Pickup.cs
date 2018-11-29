using System.Collections;
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
        RaycastHit[] hits = Physics.SphereCastAll(ray, .75f, .75f);
        Item potentialItem = null;//where we locally store an item we could pick up
        Counter potentialCounter = null;//storing a potentialCountertop
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
                    if (potentialItem == itemHeld)//if the item you're looking at is the one you're holding, FORGET IT
                    {
                        potentialItem = null;
                    }
                }
            }
            if (hit.collider.tag == "Counter")
            {
                if(potentialCounter != null)//you're already looking at a counter
                {
                    Transform newPotential = hit.collider.transform;
                    float newDistance = Vector3.Distance(transform.position, newPotential.position);
                    float oldDistance = Vector3.Distance(transform.position, potentialCounter.transform.position);
                    if(oldDistance > newDistance)//new counter is closer, you're looking at it instead
                    {
                        potentialCounter = newPotential.GetComponent<Counter>();
                    }
                }
                else//you're not already looking at a counter
                {
                    potentialCounter = hit.collider.transform.GetComponent<Counter>();
                }
            }
        }
        if (Input.GetButtonDown(pickUpAxis + myPlayerName))
        {
            if(itemHeld != null)//you're holding an item, now gonna drop it
            {
                if (potentialCounter != null)//place item on counter
                {
                    if (potentialCounter is ServingCounter)
                    {
                        if (itemHeld is Plate)
                        {
                            potentialCounter.itemHere = itemHeld;
                            Destroy(itemHeld.gameObject);
                        }
                        else
                        {
                            NeedsPlate();
                        }
                    }
                    else if (potentialCounter.itemHere == null)
                    {
                        potentialCounter.itemHere = itemHeld;
                        itemHeld = null;
                    }
                }
                else if (potentialItem != null)//combine items
                {
                    //weird shit with combining
                    if(potentialItem is Plate)
                    {
                        Plate plate = (Plate)potentialItem;
                        if (plate.plated.Add(itemHeld.stats))
                        {
                            itemHeld.stats.state = "combined";
                            Destroy(itemHeld.gameObject);
                            itemHeld = null;
                        }
                    }
                }
                else
                {
                    itemHeld.rb.isKinematic = false;
                    itemHeld = null;
                }
            }
            else//you're holding nothing
            {
                if (potentialItem != null)//you're looking at an item
                {
                    itemHeld = potentialItem;
                    itemHeld.rb.isKinematic = true;
                    itemHeld.transform.position = transform.position + transform.forward;
                    itemHeld.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                else
                {
                    if (potentialCounter != null)//you are looking at a counter
                    {
                        if (potentialCounter.tag == "Box")
                        {
                            Instantiate(Resources.Load("Item"), potentialCounter.transform);
                        }
                    }
                }
            }
        }

    }

    void DisplayRay(Ray ray,float radius,float distance)
    {
        //Debug.DrawRay(ray.origin, ray.direction, Color.red);
        Debug.DrawLine(ray.origin, ray.origin+ray.direction*distance,Color.red);
    }

    void NeedsPlate()
    {
        Debug.Log("needs plate!");
        //TODO spawn red-outlined text saying needs plate that floats up and disappears
    }
}
