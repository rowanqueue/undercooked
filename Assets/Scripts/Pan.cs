using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pan : Item {

    public Item cooking;
    public float cookingOffset;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        cooking = null;
    }

    private void Update()
    {
        if (cooking)
        {
            cooking.transform.position = transform.position + (Vector3.up * cookingOffset);
            if (cooking.percentToNextLevel > 1)
            {
                Item temp = Instantiate(cooking.turnsInto).GetComponent<Item>();
                Destroy(cooking.gameObject);
                cooking = temp;
            }
        }
    }
}
