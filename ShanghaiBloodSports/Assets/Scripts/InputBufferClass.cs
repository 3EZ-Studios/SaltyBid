using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Input;

//https://github.com/Unity-Technologies/InputSystem/blob/develop/Packages/com.unity.inputsystem/Documentation~/HowDoI.md

public class InputBuffer
{
    public ConcurrentQueue<Consumable> fifoBuff = new ConcurrentQueue<Consumable>();
    public Keyboard device = null;

   // public InputBuffer() { }

   public bool pushInput(Consumable input)
    {
        fifoBuff.Enqueue(input);
        return fifoBuff.TryPeek(out input);
    }

    public void pollKeys() //keyboard is hardcoded right now 
    {
        var keyboard = Keyboard.current;
        if (keyboard == null)
            return; // No keyboard connected.

        if (keyboard.wKey.wasPressedThisFrame)
        {
            pushInput(new UpConsumable());
        }

        if (keyboard.dKey.wasPressedThisFrame)
        {
            pushInput(new ForwardConsumable());
        }

        if (keyboard.aKey.wasPressedThisFrame)
        {
            pushInput(new BackwardConsumable());
        }

        //--verify if this is necessary---

        if (keyboard.jKey.wasPressedThisFrame)
        {
            pushInput(new PunchConsumable());
        }

        if (keyboard.kKey.wasPressedThisFrame)
        {
            pushInput(new Low_KickConsumable());
        }

        if (keyboard.iKey.wasPressedThisFrame)
        {
            pushInput(new High_KickConsumable());
        }

        if (keyboard.oKey.wasPressedThisFrame)
        {
            pushInput(new High_BlockConsumable());
        }

        if (keyboard.lKey.wasPressedThisFrame)
        {
            pushInput(new Low_BlockConsumable());
        }

    }

}

public class Consumable
{
    //what the fifo buffer will hold.
    //put custom info for the producer's use here
}

public class UpConsumable : Consumable { }
//public class DownAction : Consumable { }
public class ForwardConsumable : Consumable { }
public class BackwardConsumable : Consumable { }

//verify if this is necessary
public class PunchConsumable : Consumable { }
public class Low_KickConsumable : Consumable { }
public class High_KickConsumable : Consumable { }
public class Low_BlockConsumable : Consumable { }
public class High_BlockConsumable : Consumable { }

public class Walk
{
    //indicated by holding the forward or backward input for more than 1 frame (might be better measured in time?)
    //see modifiers (could not get this to work)
}



