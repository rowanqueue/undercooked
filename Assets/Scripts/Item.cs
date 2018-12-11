using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this goes literally on items, their objects
public class Item : MonoBehaviour
{
    public Rigidbody rb;
    public Collider collider;
    public new string name;
    public string state;
    public float percentToNextLevel;
    public GameObject turnsInto;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        percentToNextLevel = 0;
    }

    public bool Equals(Item i)
    {
        return i.name.Equals(name) && i.state.Equals(state);
    }

    public bool Equals(ItemStats i)
    {
        return i.name.Equals(name) && i.state.Equals(state);
    }
}
