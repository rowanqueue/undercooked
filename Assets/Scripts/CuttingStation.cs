using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingStation : Counter {
    public bool canBeUsed;
    public bool isCutting;
    public override void Update()
    {
        base.Update();
        if (itemHere != null && itemHere.stats.state.Equals("raw"))//can be cut
        {
            canBeUsed = true;
        }
        else
        {
            canBeUsed = false;
            isCutting = false;
        }
        if (isCutting)
        {
            itemHere.stats.percentToNextLevel += Time.deltaTime;
            if(itemHere.stats.percentToNextLevel >= 1)
            {
                isCutting = false;
            }
        }
    }
}
