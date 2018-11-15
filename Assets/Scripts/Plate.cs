using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : Item {

    public Order plated; //Items on plate, to be compared at ServingCounter

	// Use this for initialization
	void Start () {
        type = 0;
        name = "plate";
        rb = GetComponent<Rigidbody>();
        plated = new Order();
	}
}
