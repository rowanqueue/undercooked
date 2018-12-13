using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : Counter {

	public void DeleteItem(Item item)//destroys item, later will make it spawn in again
    {
        if (item is Plate)
        {
            Plate plate = (Plate)item;
            plate.plated = new Order();
        }
        else if (item is Pan)
        {
            Pan pan = (Pan)item;
            Destroy(pan.cooking.gameObject);
            pan.cooking = null;
        }
        else
        {
            Destroy(item.gameObject);
        }
    }
}
