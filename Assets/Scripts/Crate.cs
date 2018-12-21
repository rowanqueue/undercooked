using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : Counter {
    public string itemSpawn;//name of the item this spawns
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public Item SpawnItem()//spawns an item of its type on top of it
    {
        animator.ResetTrigger("Open");
        animator.SetTrigger("Open");
        GameObject obj = (GameObject)Instantiate(Resources.Load("Items/" + itemSpawn)) as GameObject;
        return obj.GetComponent<Item>();
    }
}
