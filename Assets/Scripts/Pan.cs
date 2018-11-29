using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pan : Item {

    public ItemStats cooking;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        stats = new ItemStats(name, "base");
        cooking = null;
    }
}
