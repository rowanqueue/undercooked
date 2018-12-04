using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingStation : Counter {
    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (itemHere != null && itemHere.state.Equals("raw"))//can be cut
        {
            if (Input.GetKey(KeyCode.Q))
            {
                itemHere.percentToNextLevel += Time.deltaTime * 0.5f;
            }
        }
        if (transform.childCount > 2 && itemHere == null)
        {
            Item item = GetComponentInChildren<Item>();
            if (item != null)
            {
                itemHere = item;
            }
        }
    }
}
