using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatedBun : PlatedItem {
    public GameObject topBun; //top part of this bun
    private Vector3 topStartPos; //starting local pos of top bun
    public float topOffset; //y offset from original y position, changed when items are added

    private void Start()
    {
        topStartPos = topBun.transform.localPosition;
    }

    private void Update()
    {
        topBun.transform.localPosition = topStartPos + new Vector3(0, topOffset, 0);
    }
}
