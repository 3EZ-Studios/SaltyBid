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
        LoadAll();
        Stock(rounds);
    }

    private void LoadAll()
    {
        possibleTrinkets = Resources.LoadAll<Trinket>("")
            .ToList()
            .GroupBy(trinket => trinket.Shelf)
            .ToDictionary(
                group => group.Key,
                group => group.ToList()
            );
    }

    private void Stock(int rounds)
    {
        // construct shelves
        shelves = new List<List<Trinket>>();
        foreach (int i in Enumerable.Range(0, rounds))
        {
            if (possibleTrinkets.ContainsKey(i))
            {
                var shelf = possibleTrinkets[i].Shuffle().Take(TRINKETS_PER_SHELF).ToList();
                while (shelf.Count < TRINKETS_PER_SHELF)
                {
                    shelf.Add(null);
                }

                shelves.Add(shelf);
            }
            else
            {
                var shelf = new List<Trinket>();
                while (shelf.Count < TRINKETS_PER_SHELF)
                {
                    shelf.Add(null);
                }
                shelves.Add(shelf);
            }
        }
    }

    public List<List<Trinket>> Get(int start, int count)
    {
        return shelves.GetRange(start, count);
    }

    public Trinket Buy(int shelf, int index)
    {
        Trinket trinket = shelves[shelf][index];
        if (trinket != null)
        {
            shelves[shelf][index] = null;
            return trinket;
        }
        return null;
    }
}
