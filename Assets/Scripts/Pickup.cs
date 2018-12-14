using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//use: pick stuff up! put it down!
//loc: on pick up cube child of player
public class Pickup : MonoBehaviour {
    public string pickUpAxis;//button to pick stuff up
    public string interactAxis;//button to interact with stuff
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
            itemHeld.transform.position = transform.position + (transform.forward * .75f);
        }
        //this code just looks at what you're seeing
        Ray ray = new Ray(transform.position, transform.forward);
        DisplayRay(ray, 1.5f, 1.5f);
        RaycastHit[] hits = Physics.SphereCastAll(ray, .30f, .75f);
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
                    Counter newPotential = hit.collider.GetComponent<Counter>();
                    float newDistance = Vector3.Distance(transform.position, newPotential.counterPos.position);
                    float oldDistance = Vector3.Distance(transform.position, potentialCounter.counterPos.transform.position);
                    if(oldDistance > newDistance)//new counter is closer, you're looking at it instead
                    {
                        potentialCounter = newPotential;
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
                    if (potentialCounter is ServingCounter)//SERVING!!
                    {
                        if (itemHeld is Plate)
                        {
                            potentialCounter.itemHere = itemHeld;
                            itemHeld = null;
                            //add completed order sound
                        }
                        else
                        {
                            NeedsPlate();
                        }
                    }
                    else if(potentialCounter is TrashCan)//you're throwing it away!!
                    {
                        if (itemHeld.state.Equals("chopped"))
                        {
                            SoundController.me.PlaySound(SoundController.me.dropchoppeditem,.6f);
                        }

                        if (!itemHeld.state.Equals("chopped"))
                        {
                            SoundController.me.PlaySound(SoundController.me.dropitem,.6f);
                        }
                        TrashCan trashCan = (TrashCan)potentialCounter;
                        trashCan.DeleteItem(itemHeld);
                        itemHeld = null;
                    }
                    else if (potentialCounter.itemHere == null)
                    {
                        if (itemHeld.state.Equals("chopped"))
                        {
                            SoundController.me.PlaySound(SoundController.me.dropchoppeditem,.6f);
                        }

                        if (!itemHeld.state.Equals("chopped"))
                        {
                            SoundController.me.PlaySound(SoundController.me.dropitem,.6f);
                        }
                        potentialCounter.itemHere = itemHeld;
                        itemHeld.collider.enabled = false;
                        itemHeld = null;
                    }
                    else if (potentialCounter.itemHere is Plate)
                    {
                        Plate plate = (Plate)potentialCounter.itemHere;
                        if (!(itemHeld is Pan))
                        {
                            if (plate.Add(new ItemStats(itemHeld.name, itemHeld.state)))
                            {
                                SoundController.me.PlaySound(SoundController.me.dropchoppeditem,.6f);
                                Destroy(itemHeld.gameObject);
                            }
                        }
                        else
                        {
                            Pan pan = (Pan)itemHeld;
                            if (plate.Add(new ItemStats(pan.cooking.name, pan.cooking.state)))
                            {
                                SoundController.me.PlaySound(SoundController.me.dropchoppeditem,.6f);
                                Destroy(pan.cooking.gameObject);
                                pan.cooking = null;
                            }
                        }
                    }
                    else if (potentialCounter.itemHere is Pan && itemHeld.name.Equals("burger") && itemHeld.state.Equals("chopped"))
                    {
                        Pan pan = (Pan)potentialCounter.itemHere;
                        pan.cooking = itemHeld;
                        SoundController.me.PlaySound(SoundController.me.dropchoppeditem,.6f);
                        pan.SpawnProgBar();
                        pan.cooking.collider.enabled = false;
                        itemHeld = null;
                        if (pan.cooking!=null)
                        {
                           SoundController.me.PlaySound(SoundController.me.grilling,.6f);
                        }
                    }
                }
                else if (potentialItem != null)//combine items
                {
                    //weird shit with combining
                    if(potentialItem is Plate)
                    {
                        Plate plate = (Plate)potentialItem;
                        if (plate.Add(new ItemStats(itemHeld.name, itemHeld.state)))
                        {
                           SoundController.me.PlaySound(SoundController.me.dropchoppeditem,.6f);
                            Destroy(itemHeld.gameObject);
                        }
                    }
                }
                else
                {
                    if (itemHeld.state.Equals("chopped"))
                    {
                      SoundController.me.PlaySound(SoundController.me.dropchoppeditem,.6f);
                    }

                    if (!itemHeld.state.Equals("chopped"))
                    {
                        SoundController.me.PlaySound(SoundController.me.dropitem,.6f);
                    }
                    itemHeld.rb.isKinematic = false;
                    itemHeld.collider.enabled = true;
                    itemHeld = null;
                   
                }
            }
            else//you're holding nothing
            {
                if (potentialCounter != null)//you're looking at a counter
                {
                    if (potentialCounter.itemHere == null && potentialCounter is Crate)
                    {
                        SoundController.me.PlaySound(SoundController.me.pickUpItem,.6f);
                        Crate crate = (Crate)potentialCounter;
                        crate.SpawnItem();
                        itemHeld = crate.itemHere;
                        crate.itemHere = null;
                        itemHeld.rb.isKinematic = true;
                        itemHeld.collider.enabled = false;
                    }
                    else if (potentialCounter is ReturnCounter)//getting a plate from return
                    {
                        ReturnCounter rc = (ReturnCounter)potentialCounter;
                        if (rc.GetPlate())//you can grab a plate
                        {
                            SoundController.me.PlaySound(SoundController.me.pickUpItem,.6f);
                            GameObject obj = (GameObject)Instantiate(Resources.Load("Items/Plate"), transform);
                            itemHeld.collider.enabled = false;
                            itemHeld = obj.GetComponent<Item>();
                        }
                    }
                    else if (potentialCounter.itemHere != null)
                    {
                        SoundController.me.PlaySound(SoundController.me.pickUpItem,.6f);
                        itemHeld = potentialCounter.itemHere;
                        potentialCounter.itemHere = null;
                        itemHeld.rb.isKinematic = true;
                        itemHeld.collider.enabled = false;
                        itemHeld.transform.position = transform.position + transform.forward;
                        itemHeld.transform.rotation = Quaternion.Euler(0, 0, 0);
                        
                    }
                }
                else
                {
                    if (potentialItem != null)//you are looking at an item
                    {
                        SoundController.me.PlaySound(SoundController.me.pickUpItem,.6f);
                        itemHeld = potentialItem;
                        itemHeld.rb.isKinematic = true;
                        itemHeld.collider.enabled = false;
                        itemHeld.transform.position = transform.position + transform.forward;
                        itemHeld.transform.rotation = Quaternion.Euler(0, 0, 0);
                        
                    }
                }
            }
        }
        if(Input.GetButton(interactAxis + myPlayerName))
        {
            if(itemHeld == null)//not holding an item
            {
                if(potentialCounter != null)//looking at a counter
                {
                    if(potentialCounter is CuttingStation)
                    {
                        CuttingStation cuttingStation = (CuttingStation)potentialCounter;
                        if (cuttingStation.canBeUsed)
                        {
                            if (cuttingStation.isCutting == false)
                            {
                                SoundController.me.PlaySound(SoundController.me.chopping,.65f);
                            }
                            cuttingStation.isCutting = true;
                           

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
