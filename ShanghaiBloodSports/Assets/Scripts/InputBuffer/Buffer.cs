using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;

public class Buffer : MonoBehaviour
{
    private const double thresholdFar = 0.75;
    private const double thresholdNear = 0.50;

    private int maxCapacity = 300;

    public ConcurrentQueue<Vector2> fifo = new ConcurrentQueue<Vector2>();
    public ConcurrentQueue<String> consumables = new ConcurrentQueue<String>();

    public String VectorToConsumable(Vector2 v)
    {
        if (v.x >= thresholdFar & v.y <= Math.Abs(thresholdNear))           {return "F";}
        else if (v.x <= -thresholdFar & v.y <= Math.Abs(thresholdNear))     { return "B";}
        else if (v.y >= thresholdFar & v.x <= Math.Abs(thresholdNear))      {return "U";}
        else if (v.y <= -thresholdFar & v.x <= Math.Abs(thresholdNear))     {return "D";}
        else if (v.x > thresholdNear & v.y > thresholdNear)                 { return "Uf";}
        else if (v.x < -thresholdNear & v.y > thresholdNear)                { return "Ub";}
        else if (v.x < -thresholdNear & v.y < -thresholdNear)               { return "Db";}
        else if (v.x > thresholdNear & v.y < -thresholdNear)                { return "Df"; }
        else { return "N"; }
    }

    public void SetCapacity(int max)
    {
        maxCapacity = max;
    }

    public void pushConsumable(String c)
    {
        if (consumables.Count > maxCapacity)
        {
            String throwaway = null;
            consumables.TryDequeue(out throwaway);
            consumables.Enqueue(c);
        }
    }
}
