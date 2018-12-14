using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServingCounter : Counter {
    public float timer;
    public List<Order> requested;
    public bool firstOrder;
	public List<Transform> OtherDisplayed= new List<Transform>();
	public GameObject UIBorders;

	// Use this for initialization
	void Start () {
        timer = 0;
        requested = new List<Order>();
        requested.Add(Order.GenerateBurger(Random.Range(0, 3)));
	}
	
	// Update is called once per frame
	public override void Update () {
        timer += Time.deltaTime;
		OtherDisplayed = UIBorders.GetComponent<UIOrders>().DisplayedOrders;
        if (itemHere != null && itemHere.gameObject.transform.position != counterPos.position)
        {
            itemHere.gameObject.transform.position = counterPos.position;
        }
        if (itemHere != null && itemHere is Plate)
        {
            Score.me.score += Serve((Plate)itemHere);
            Destroy(itemHere.gameObject);
            itemHere = null;
        }
        if (timer > 15f)
        {
            Order newOrder = Order.GenerateBurger(Random.Range(0, 3));
            UIOrders.me.InstantiateOrder(newOrder);
            requested.Add(newOrder);
            timer = 0;
            Debug.Log("why do you happen twice");
        }
        print("Requested:" + requested[0]);
    }

    public int Serve(Plate served) //find order in requested and deliver it
    {
	  
        if (served.plated.Equals(requested[0]))
        {
            /*
	        Debug.Log("MoveOver");
	        for (int i = 0; i < OtherDisplayed.Count; i++)
	        {
		        foreach (Transform t in OtherDisplayed)
		        {
			        if (t.transform.CompareTag("SmallOrder") && served.plated.Equals(requested[0]) )
			        {
				        UIOrders.CompletedOrderNum = 0;
				        UIOrders.PositionInList = OtherDisplayed.IndexOf(t);
				        GetComponent<UIOrders>().DisplayedOrders.Remove(t);
				        OtherDisplayed.Remove(t);
				        Destroy(t.gameObject);
				        UIOrders.Completed = true;	
				    
				        break;

			        }


		        }
	        }
            */
            UIOrders.me.CompletedOrder(requested[0]);//whatever the order is;
            requested.RemoveAt(0);
	        Debug.Log("Accepted");
            return 20;
        }
        return 0;
    }
}
