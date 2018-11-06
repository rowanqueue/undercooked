﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingStation : MonoBehaviour {
    public Item itemHere;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(itemHere != null && itemHere.type == 1)//can be cut
        {
            if (Input.GetKey(KeyCode.Q))
            {
                itemHere.percentToNextLevel += Time.deltaTime * 0.5f;
            }
        }
        if (transform.childCount > 2 && itemHere == null)
        {
            Item item = GetComponentInChildren<Item>();
            if(item != null)
            {
                itemHere = item;
            }
        }
	}
}