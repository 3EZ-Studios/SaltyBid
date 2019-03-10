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
    public ConcurrentQueue<Vector2> fifo = new ConcurrentQueue<Vector2>();
}
