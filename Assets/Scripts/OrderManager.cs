using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//just a script im using to debug orders for combining
public class OrderManager : MonoBehaviour {
    public static OrderManager me;
    public Order order;
	// Use this for initialization
	void Start () {
        me = this;
        order = new Order();
        Order.GenerateBurger(Random.Range(0, 3));
	}
	
	// Update is called once per frame
	void Update () {
	}
}
