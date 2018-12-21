using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plate : Item {

    public Order plated;
    public Transform platedPos;
    public PlatedBun bun;
    public PlatedItem tomato, lettuce, burger;
    public Image[] itemSprites;
    public Vector3[] itemUIPositions;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        plated = new Order(); //Items on plate, to be compared at ServingCounter
        itemSprites = new Image[4];
    }

    public override void Update()
    {
        for (int index = 0; index < itemSprites.Length; index++)
        {
            if (itemSprites[index] != null)
            {
                itemSprites[index].transform.position = Camera.main.WorldToScreenPoint(transform.position + (itemUIPositions[index]));
            }
        }
    }

    public bool Add(ItemStats i)
    {
        bool added = plated.Add(i);
        if (added)
        {
            UpdateVisuals(i.name);
            for (int index = 0; index < itemSprites.Length; index++)
            {
                if (itemSprites[index] == null)
                {
                    itemSprites[index] = Instantiate((GameObject)Resources.Load("Icons/" + i.name + "Icon"), canvas.transform).GetComponent<Image>();
                    itemSprites[index].transform.position = Camera.main.WorldToScreenPoint(transform.position + (itemUIPositions[index]));
                    break;
                }
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    public void UpdateVisuals(string added)
    {
        float currentOffset = 0;
        if (added.Equals("bun"))
        {
            bun = Instantiate((GameObject)Resources.Load("Items/PlatedBun"), transform).GetComponent<PlatedBun>();
            if (burger != null)
            {
                bun.topOffset += burger.offset;
                burger.transform.localPosition = burger.transform.localPosition + (Vector3.up * (bun.offset));
                if (lettuce != null)
                {
                    bun.topOffset += lettuce.offset;
                    lettuce.transform.localPosition = lettuce.transform.localPosition + (Vector3.up * (bun.offset));
                }
                if (tomato != null)
                {
                    bun.topOffset += tomato.offset;
                    tomato.transform.localPosition = tomato.transform.localPosition + (Vector3.up * (bun.offset));
                }
            }
            else
            {
                if (lettuce != null)
                {
                    bun.topOffset += lettuce.offset;
                    Destroy(lettuce.gameObject);
                    UpdateVisuals("lettuce");
                }
                if (tomato != null)
                {
                    bun.topOffset += tomato.offset;
                    Destroy(tomato.gameObject);
                    UpdateVisuals("tomato");
                }
            }
            bun.transform.localPosition = platedPos.localPosition + (Vector3.up * (bun.offset + currentOffset));
        }
        else if (added.Equals("burger"))
        {
            burger = Instantiate((GameObject)Resources.Load("Items/PlatedBurger"), transform).GetComponent<PlatedItem>();
            if (bun != null)
            {
                bun.topOffset += burger.offset;
                currentOffset += bun.offset;
                if (lettuce != null)
                {
                    lettuce.transform.localPosition = lettuce.transform.localPosition + (Vector3.up * (burger.offset));
                }
                if (tomato != null)
                {
                    tomato.transform.localPosition = tomato.transform.localPosition + (Vector3.up * (burger.offset));
                }
            }
            else
            {
                if (lettuce != null)
                {
                    Destroy(lettuce.gameObject);
                    UpdateVisuals("lettuce");
                }
                if (tomato != null)
                {
                    Destroy(tomato.gameObject);
                    UpdateVisuals("tomato");
                }
            }
            burger.transform.localPosition = platedPos.localPosition + (Vector3.up * (burger.offset + currentOffset));
        }
        else if (added.Equals("lettuce"))
        {
            if (bun != null || burger != null)
            {
                lettuce = Instantiate((GameObject)Resources.Load("Items/BurgerLettuce"), transform).GetComponent<PlatedItem>();
                if (bun != null)
                {
                    currentOffset += bun.offset;
                    bun.topOffset += lettuce.offset;
                }
                if (burger != null)
                {
                    currentOffset += burger.offset;
                }
                if (tomato != null)
                {
                    tomato.transform.localPosition = tomato.transform.localPosition + (Vector3.up * (lettuce.offset));
                }
            }
            else
            {
                lettuce = Instantiate((GameObject)Resources.Load("Items/LettucePlated"), transform).GetComponent<PlatedItem>();
            }
            lettuce.transform.localPosition = platedPos.localPosition + (Vector3.up * (lettuce.offset + currentOffset));
        }
        else if(added.Equals("tomato"))
        {
            if (bun != null || burger != null)
            {
                tomato = Instantiate((GameObject)Resources.Load("Items/BurgerTomato"), transform).GetComponent<PlatedItem>();
                if (bun != null)
                {
                    currentOffset += bun.offset;
                    bun.topOffset += tomato.offset;
                }
                if (burger != null)
                {
                    currentOffset += burger.offset;
                }
                if (lettuce != null)
                {
                    currentOffset += lettuce.offset;
                }
            }
            else
            {
                tomato = Instantiate((GameObject)Resources.Load("Items/TomatoPlated"), transform).GetComponent<PlatedItem>();
            }
            tomato.transform.localPosition = platedPos.localPosition + (Vector3.up * (tomato.offset + currentOffset));
        }
    }

    public override void OnDestroy()
    {
        foreach (Image i in itemSprites)
        {
            if (i != null)
            {
                Destroy(i.gameObject);
            }
        }
    }
}
