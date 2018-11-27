﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServingCounter : Counter {

    public float timer;
    public List<Order> requested;

	// Use this for initialization
	void Start () {
        timer = 0;
        requested = new List<Order>();
        requested.Add(Order.GenerateBurger(0));
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (itemHere != null && itemHere.gameObject.transform.position != counterPos)
        {
            itemHere.gameObject.transform.position = counterPos;
        }
    }

    public int Serve(Plate served) //find order in requested and deliver it
    {
        if (served.plated.Equals(requested[0]))
        {
            Debug.Log("Accepted");
            return 20;
        }
        return 0;
    }
}
