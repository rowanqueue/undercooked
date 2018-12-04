using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order {

    public ItemStats[] contents;

    public Order() //creates order where all items are empty
    {
        contents = new ItemStats[4];
        for (int i = 0; i < contents.Length; i++)
        {
            contents[i] = new ItemStats();
        }
    }

    //Random burger generation for orders
    public static Order GenerateBurger(int orderNum) //orderNum integer 0-2, 0:basic burger, 1:lettuce burger, 2:lettuce tomato burger
    {
        Order order = new Order();
        order.Add(new ItemStats("bun", "base"));
        //UIOrder.numOrder = orderNum;
        if (orderNum > 0)
        {
            order.Add(new ItemStats("lettuce", "chopped"));
        }
        if (orderNum > 1)
        {
            order.Add(new ItemStats("tomato", "chopped"));
        }
        order.Add(new ItemStats("burger", "cooked"));
        return order;
    }

    public bool Add(ItemStats i) //Adds i in correct position in contents. Returns true if successful.
    {
        if (i.name.Equals("bun") && i.state.Equals("base") && contents[0].state.Equals("DNE"))
        {
            i.state = "combined";
            contents[0] = i;
            return true;
        }
        else if (i.state.Equals("chopped")) {
            if (i.name.Equals("lettuce") && contents[1].state.Equals("DNE"))
            {
                i.state = "combined";
                contents[1] = i;
                return true;
            }
            else if (i.name.Equals("tomato") && contents[2].state.Equals("DNE"))
            {
                i.state = "combined";
                contents[2] = i;
                return true;
            }
            
        }
        else if (i.state.Equals("cooked") && i.name.Equals("burger") && contents[3].state.Equals("DNE"))
        {
            i.state = "combined";
            contents[3] = i;
            return true;
        }
        return false;
    }

    public bool Equals(Order o)
    {
        for (int i = 0; i < o.contents.Length;  i++)
        {
            if (!o.contents[i].Equals(contents[i])) //Item comparator
            {
                return false;
            }
        }
        return true;
    }

    public override string ToString()
    {
        string s = "";
        for (int i = 0; i < 4; i++)
        {
            s = s + "[" + contents[i].name + ", " + contents[i].state + "] ";
        }
        return s;
    }
}
