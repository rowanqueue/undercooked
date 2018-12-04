using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this goes literally on items, their objects
public class ItemStats
{
    public string name;//burger, bun, etc.
    public string state;//replacement for type hierarchy ex: raw, chopped, cooked, combined, base

    public ItemStats() { state = "DNE"; name = ""; } //empty item with no name and negative type
    public ItemStats(string n, string s)
    {
        name = n;
        state = s;
    }

    public bool Equals(ItemStats i)
    {
        return name.Equals(i.name) && state.Equals(state);
    }

    public bool Equals(Item i)
    {
        return name.Equals(i.name) && state.Equals(state);
    }
}