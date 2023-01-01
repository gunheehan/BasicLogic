using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class PlayerInputManager : Singleton<PlayerInputManager>
{
    public delegate void KeyBoardEventHandler();

    private KeyBoardEventHandler _keyboardPhysicsInputEvent = null;
    
    public event KeyBoardEventHandler KeyboardPhysicsInputEvent
    {
        add     { _keyboardPhysicsInputEvent += value; }
        remove  { _keyboardPhysicsInputEvent -= value; }
    }

    private KeyBoardEventHandler _keyboardNormalInputEvent = null;
    
    public event KeyBoardEventHandler KeyboradNormalInputEvent
    {
        add     { _keyboardNormalInputEvent += value; }
        remove  { _keyboardNormalInputEvent -= value; }
    }
    private void Update()
    {
        if (Input.anyKey && _keyboardNormalInputEvent != null)
        {
            _keyboardNormalInputEvent.Invoke();
        }
    }

    private void FixedUpdate()
    {
        if (Input.anyKey && _keyboardPhysicsInputEvent != null)
        {
            _keyboardPhysicsInputEvent.Invoke();
        }
    }
}
