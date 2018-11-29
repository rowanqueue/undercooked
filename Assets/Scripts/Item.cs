using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this goes literally on items, their objects
public class Item : MonoBehaviour
{
    public Rigidbody rb;
    public string name;
    public ItemStats stats;
    public GameObject turnsInto;

    private Dictionary<string, IEnumerable> types;

    MeshRenderer mr;

    void Start () {
        rb = GetComponent<Rigidbody>();
        mr = GetComponentInChildren<MeshRenderer>();
        if (name.Equals("bun"))
        {
            stats = new ItemStats(name, "base");
        }
        else
        {
            stats = new ItemStats(name, "chopped");
        }
	}
}
