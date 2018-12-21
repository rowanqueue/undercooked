using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//this goes literally on items, their objects
public class Item : MonoBehaviour
{
    public Rigidbody rb;
    public Collider collider;
    public Vector3 uiDisplayPosition;
    public Image itemSprite;
    public new string name;
    public string state;
    public float percentToNextLevel;
    public GameObject turnsInto;

    protected GameObject canvas;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        if (name.Equals("lettuce") || name.Equals("bun") || name.Equals("tomato") || name.Equals("burger")) {
            itemSprite = Instantiate((GameObject)Resources.Load("Icons/" + name + "Icon"), canvas.transform).GetComponent<Image>();
            uiDisplayPosition = transform.position + (Vector3.up * .75f);
            itemSprite.transform.position = Camera.main.WorldToScreenPoint(uiDisplayPosition);
        }
        percentToNextLevel = 0;
    }

    public virtual void Update()
    {
        if (itemSprite != null) {
            uiDisplayPosition = transform.position + (Vector3.up * .75f);
            itemSprite.transform.position = Camera.main.WorldToScreenPoint(uiDisplayPosition);
        }
    }

    public virtual void OnDestroy()
    {
        if (itemSprite != null)
        {
            Destroy(itemSprite.gameObject);
        }
    }

    public bool Equals(Item i)
    {
        return i.name.Equals(name) && i.state.Equals(state);
    }

    public bool Equals(ItemStats i)
    {
        return i.name.Equals(name) && i.state.Equals(state);
    }
}
