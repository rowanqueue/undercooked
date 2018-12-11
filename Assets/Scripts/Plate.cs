using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : Item {

    public Order plated;
    public Transform platedPos;
    public PlatedBun bun;
    public PlatedItem tomato, lettuce, burger;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        plated = new Order(); //Items on plate, to be compared at ServingCounter
    }

    private void Update()
    {
        print("Plated:" + plated.ToString());
    }

    public bool Add(ItemStats i)
    {
        bool added = plated.Add(i);
        if (added)
        {
            UpdateVisuals(i.name);
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
            bun = ((GameObject)Instantiate(Resources.Load("Items/PlatedBun"), transform)).GetComponent<PlatedBun>();
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
            burger = ((GameObject)Instantiate(Resources.Load("Items/PlatedBurger"), transform)).GetComponent<PlatedItem>();
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
                lettuce = ((GameObject)Instantiate(Resources.Load("Items/BurgerLettuce"), transform)).GetComponent<PlatedItem>();
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
                lettuce = ((GameObject)Instantiate(Resources.Load("Items/LettucePlated"), transform)).GetComponent<PlatedItem>();
            }
            lettuce.transform.localPosition = platedPos.localPosition + (Vector3.up * (lettuce.offset + currentOffset));
        }
        else if(added.Equals("tomato"))
        {
            if (bun != null || burger != null)
            {
                tomato = ((GameObject)Instantiate(Resources.Load("Items/BurgerTomato"), transform)).GetComponent<PlatedItem>();
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
                tomato = ((GameObject)Instantiate(Resources.Load("Items/TomatoPlated"), transform)).GetComponent<PlatedItem>();
            }
            tomato.transform.localPosition = platedPos.localPosition + (Vector3.up * (tomato.offset + currentOffset));
        }
    }
}
