using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pan : Item {

    public Item cooking;
    public float cookingOffset;
    public Transform progBarBack;
    public Image progBarContents;

    void Start()
    {
        cooking = null;
    }

    public override void Update()
    {
        if (cooking)
        {
            cooking.transform.position = transform.position + (Vector3.up * cookingOffset);
            if (cooking.state.Equals("chopped") && cooking.percentToNextLevel > 1)
            {
                Item temp = Instantiate(cooking.turnsInto).GetComponent<Item>();
                Destroy(cooking.gameObject);
                Destroy(progBarBack.gameObject);
                cooking = temp;
            }
        }
        if (progBarBack != null)
        {
            progBarBack.transform.position = Camera.main.WorldToScreenPoint(transform.position + (Vector3.down * .75f));
            if (progBarContents != null)
            {
                progBarContents.fillAmount = cooking.percentToNextLevel;
            }
        }
    }

    public void SpawnProgBar()
    {
        progBarBack = Instantiate((GameObject)Resources.Load("Icons/ProgressBar"), canvas.transform).transform;
        progBarContents = progBarBack.GetChild(0).GetComponent<Image>();
        progBarBack.transform.position = Camera.main.WorldToScreenPoint(transform.position + (Vector3.down * .75f));
    }
}
