using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : Counter {
    public string itemSpawn;//name of the item this spawns

    public void SpawnItem()//spawns an item of its type on top of it
    {
        if(itemHere == null)
        {
            GameObject obj = (GameObject)Instantiate(Resources.Load("Items/" + itemSpawn), transform) as GameObject;
            obj.transform.position = transform.position + counterPos;
            itemHere = obj.GetComponent<Item>();
        }
    }
}
