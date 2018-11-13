using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServingCounter : MonoBehaviour {

    public float timer;
    public List<Order> requested;
    public bool firstOrder;

	// Use this for initialization
	void Start () {
        timer = 0;
        requested.Add(Order.GenerateBurger(1));
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
	}

    public int Serve(Plate served)
    {
        if (served.plated.Equals(requested[0]))
        {
            Debug.Log("Accepted");
            return 20;
        }
        return 0;
    }
}
