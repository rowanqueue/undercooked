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
        requested.Add(Order.GenerateBurger(1));
	}
	
	// Update is called once per frame
	public override void Update () {
        timer += Time.deltaTime;
		OtherDisplayed = UIBorders.GetComponent<UIOrders>().DisplayedOrders;
        if (itemHere != null && itemHere.gameObject.transform.position != transform.position + counterPos)
        {
            itemHere.gameObject.transform.position = transform.position + counterPos;
        }
        if (itemHere != null && itemHere is Plate)
        {
            Serve((Plate)itemHere);
            Destroy(itemHere.gameObject);
            itemHere = null;
        }
    }

    public int Serve(Plate served) //find order in requested and deliver it
    {
        if (served.plated.Equals(requested[0]))
        {
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
	        Debug.Log("Accepted");
            return 20;
        }
        return 0;
    }
}
