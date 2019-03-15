using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;

public class MockInputBuffer : MonoBehaviour
{


 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool Match(KeyCode key)
    {
        return Input.GetKeyDown(key);
    }

    public bool Peek(KeyCode key)
    {
        return Input.GetKey(key);
    }

    
}

public class InputBuffer
{
    public const double thresholdFar = 0.75;
    public const double thresholdNear = 0.50;

    public ConcurrentQueue<Vector2> fifo = new ConcurrentQueue<Vector2>();
    public ConcurrentQueue<String> consumables = new ConcurrentQueue<String>();

    public Vector2 incomingVector =new Vector2(0, 0);

    public String VectorToConsumable(Vector2 v)
    {
        if (v.x >= thresholdFar & v.y <= Math.Abs(thresholdNear))
        {
            return "F";
        }
        else if (v.x <= -thresholdFar & v.y <= Math.Abs(thresholdNear))
        {
            return "B";
        }
        else if (v.y >= thresholdFar & v.x <= Math.Abs(thresholdNear))
        {
            return "U";
        }
        else if (v.x <= thresholdFar & v.y <= Math.Abs(thresholdNear))
        {
            return "D";
        }
        else if (v.x > thresholdNear & v.y > thresholdNear)
        {
            return "Uf";
        }

        else if (v.x < -thresholdNear & v.y > thresholdNear)
        {
            return "Ub";
        }

        else if (v.x < -thresholdNear & v.y < -thresholdNear)
        {
            return "Db";
        }

        else if (v.x > thresholdNear & v.y < -thresholdNear)
        {
            return "Df";
        }

        else
        {
            return "N"; //neutral
        }

    }

}

