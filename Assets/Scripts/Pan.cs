using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pan : Item {

    public Item cooking;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cooking = null;
    }
}
