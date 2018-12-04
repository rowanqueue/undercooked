using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : Counter {

	public void DeleteItem(Item item)//destroys item, later will make it spawn in again
    {
        Destroy(item.gameObject);
    }
}
