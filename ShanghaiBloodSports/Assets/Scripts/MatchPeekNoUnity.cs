using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
//using UnityEngine;

public class MatchPeekNoUnity 
{

    // float pollingFreq = 1; //1s
    // TimeSpan duration; //for a thread timer
    private ConcurrentQueue<Queue<char>> fifo = new ConcurrentQueue<Queue<char>>();
    //Perhaps timestamped items in the fifo may be more efficient (no need to unpack)
    //TODO: have to make this multithreaded st polling still occurs during match/peek 

    public bool Match(String inputSequence, uint t)
    {
        Queue<char>[] copiedFifo = fifo.ToArray();

        if (t == 0) { return false; }
        if (t > copiedFifo.Length) { return false; }


        //unpack
        List<char> l = new List<char>();
        for (int i = 0; i < t; i++)
        {
            Queue<char> temp = copiedFifo[i];

            int count = temp.Count;
            for (int j = 0; j < count; j++)
            {
                l.Add(temp.Dequeue());
            }
        }

        //search 
        char[] workingList = inputSequence.ToCharArray();
        for (int k = 0; k < inputSequence.Length; k++)
        {
            if (l[k] == inputSequence[k])
            {
                continue;
            }
            else
            {
                return false;
            }
        }

        //remove matching items not yet implemented
        return true;
    }

    public void push(Queue<char> q)
    {
        fifo.Enqueue(q);
    }

    public bool Peek(String inputSequence, uint t)
    {

        Queue<char>[] copiedFifo = fifo.ToArray();


        if (t == 0) { return false; }
        if (t > copiedFifo.Length) { return false; }


        //unpack
        List<char> l = new List<char>();
        for (int i = 0; i < t; i++)
        {
            Queue<char> temp = copiedFifo[i];

            int count = temp.Count;
            for (int j = 0; j < count; j++)
            {
                l.Add(temp.Dequeue());
            }
        }

        //search 
        char[] workingList = inputSequence.ToCharArray();
        for (int k = 0; k < inputSequence.Length; k++)
        {
            if (l[k] == inputSequence[k])
            {
                continue;
            }
            else
            {
                return false;
            }
        }

        return true;
    }


}


/*
  static void Main(string[] args)
        {

            InputBuffer mybuff = new InputBuffer();

            Queue<char> first = new Queue<char>();
            first.Enqueue('a');
            first.Enqueue('b');
            first.Enqueue('c');
            first.Enqueue('d');

            mybuff.push(first);

            bool result = mybuff.Peek("ac", 1);
            Console.WriteLine(result);

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
     
     */
