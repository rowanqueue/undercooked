using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order {

    List<Item> contents;

    public Order() { contents = new List<Item>(); }
    public Order(int l) { contents = new List<Item>(l); }
    public Order(List<Item> o) { contents = new List<Item>(o); }

    public Order GenerateBurger()
    {
        int randOrder = Random.Range(2, 4);
        List<Item> order = new List<Item>(randOrder);
        order[0] = new Item(5, "bun");
        order[randOrder-1] = new Item(4, "burger");
        if (randOrder > 2)
        {
            order[1] = new Item(4, "lettuce");
        }
        else
        {
            return new Order(order);
        }
        if (randOrder > 3)
        {
            order[2] = new Item(4, "tomato");
        }
        return new Order(order);
    }

    public void Add(Item i)
    {
        if (i.name.Equals("bun") && !contents.Contains(i))
        {

        }
        else if (i.name.Equals("bun") && !contents.Contains(i))
        {

        }
        else if (i.name.Equals("lettuce"))
        {

        }
        else if (i.name.Equals("tomato"))
        {

        }
    }

    public bool Equals(Order o)
    {
        bool equals = true;
        for (int i = 0; i <= o.contents.Count;  i++)
        {
            if (!o.contents[i].Equals(contents[i]))
            {
                return false;
            }
        }
        return equals;
    }
}
