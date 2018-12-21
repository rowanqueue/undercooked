using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServingCounter : Counter {
    public float timer;
    public int score;
    public List<Order> requested;
    public bool firstOrder;
	public List<Transform> OtherDisplayed= new List<Transform>();
	public GameObject UIBorders;
    public float returnPlateWaitTime;
    float waitForReturnPlate;

	// Use this for initialization
	void Start () {
        timer = 0;
        requested = new List<Order>();
        CreateOrder();
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
            score += Serve((Plate)itemHere);
            Destroy(itemHere.gameObject);
            itemHere = null;
        }
        if (timer > 15f)
        {
            CreateOrder();
        }
        if(waitForReturnPlate > 0)
        {
            waitForReturnPlate -= Time.deltaTime;
            if(waitForReturnPlate <= 0)
            {
                ReturnCounter.me.ReturnPlate();
            }
        }
        
    }
    public void CreateOrder()
    {
        int orderNum = Random.Range(0, 3);
        Order newOrder = Order.GenerateBurger(orderNum);
        UIOrders.me.InstantiateOrder(orderNum,newOrder);
        requested.Add(newOrder);
        timer = 0;
    }
    public int Serve(Plate served) //find order in requested and deliver it
    {
        if(waitForReturnPlate > 0)
        {
            ReturnCounter.me.ReturnPlate();
        }
        waitForReturnPlate = returnPlateWaitTime+Time.time;
        for (int i = 0; i < requested.Count; i++)
        {
            if (served.plated.Equals(requested[i]))
            {
                SoundController.me.PlaySound(SoundController.me.completeOrder,1);
                UIOrders.me.CompletedOrder(requested[i]);//whatever the order is;
                requested.RemoveAt(i);
                Debug.Log("Accepted");
                return 20;
            }

            if (!served.plated.Equals(requested[i]))
            {
                SoundController.me.PlaySound(SoundController.me.singleCountdownBeep,1);
            }
        }
        return 0;
    }
}
