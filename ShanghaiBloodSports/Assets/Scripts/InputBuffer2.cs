using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Input;
using System;
using UnityEngine.Experimental.Input.Plugins.UI;

public class InputBuffer2 : MonoBehaviour
{

  //  UIKeyboardMouseInputModule keyboardMouseIM = new UIKeyboardMouseInputModule();

    // Start is called before the first frame update
    void Start()
    {
        test(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void test() {

        InputDevice[] allDevices = InputSystem.devices.ToArray();

        Array.ForEach(allDevices, x => Debug.Log(x.description));

    }


}
