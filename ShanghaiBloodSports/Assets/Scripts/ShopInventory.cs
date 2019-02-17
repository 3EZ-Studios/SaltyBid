using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopInventory
{
    private List<List<Trinket>> shelves;
    private Dictionary<int, List<Trinket>> possibleTrinkets;

    const int TRINKETS_PER_SHELF = 4;

    public ShopInventory(int rounds)
    {
        // load all trinkets here
        var allTrinkets = new List<Trinket>();
        possibleTrinkets = new Dictionary<int, List<Trinket>>();

        foreach (Trinket trinket in allTrinkets)
        {
            if (!possibleTrinkets.ContainsKey(trinket.shelfNumber))
            {
                possibleTrinkets.Add(trinket.shelfNumber, new List<Trinket>());
            }
            possibleTrinkets[trinket.shelfNumber].Add(trinket);
        }

        Stock(rounds);
    }

    private void Stock(int rounds)
    {
        // construct shelves
        shelves = new List<List<Trinket>>();
        foreach (int i in Enumerable.Range(0, rounds))
        {
            if (possibleTrinkets.ContainsKey(i))
            {
                shelves.Add(possibleTrinkets[i].Shuffle().Take(TRINKETS_PER_SHELF).ToList());
            }
        }
    }

    public List<List<Trinket>> Get(int start, int count)
    {
        return shelves.GetRange(start, count);
    }
}
