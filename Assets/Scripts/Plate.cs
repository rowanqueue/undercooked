using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : Item {

    Order plated;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        plated = new Order();
	}
}
