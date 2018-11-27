using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order {

    Item[] contents;

    public Order() //creates order where all items are empty
    {
        contents = new Item[4];
        for (int i = 0; i < contents.Length; i++)
        {
            contents[i] = new Item();
        }
    }

    //Random burger generation for orders
    public static Order GenerateBurger(int orderNum) //orderNum integer 0-2, 0:basic burger, 1:lettuce burger, 2:lettuce tomato burger
    {
        Order order = new Order();
        order.Add(new Item(5, "bun"));
        UI_Display.ordertoDisplay = orderNum;
        if (orderNum > 0)
        {
            order.Add(new Item(4, "lettuce"));
        }
        if (orderNum > 1)
        {
            order.Add(new Item(4, "tomato"));
        }
        order.Add(new Item(4, "burger"));
        Debug.Log(order.contents);
        return order;
    }

    public bool Add(Item i) //Adds i in correct position in contents. Returns true if successful.
    {
        if (i.name.Equals("bun") && contents[0].type < 0)
        {
            contents[0] = i;
            return true;
        }
        else if (i.type == 4) {
            if (i.name.Equals("lettuce") && contents[1].type < 0)
            {
                contents[1] = i;
                return true;
            }
            else if (i.name.Equals("tomato") && contents[2].type < 0)
            {
                contents[2] = i;
                return true;
            }
            else if (i.name.Equals("burger") && contents[3].type < 0)
            {
                contents[3] = i;
                return true;
            }
        }
        return false;
    }

    public bool Equals(Order o)
    {
        for (int i = 0; i <= o.contents.Length;  i++)
        {
            if (!o.contents[i].Equals(contents[i]))
            {
                return false;
            }
        }
        return true;
    }
}
