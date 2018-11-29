using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : Item {

    public Order plated;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        stats = new ItemStats(name, "base");
        plated = new Order(); //Items on plate, to be compared at ServingCounter
    }

    private void Update()
    {
        print(plated.contents[0].name);
    }
}
