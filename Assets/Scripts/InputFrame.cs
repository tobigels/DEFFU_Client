using System;
using UnityEngine;

[Serializable]
public class InputFrame {
    public int frameNumber;

    public Vector3 controller_right_pos;
    public Quaternion controller_right_rot;
    public Vector3 controller_left_pos;
    public Quaternion controller_left_rot;
    public Vector3 hmd_pos;
    public Quaternion hmd_rot;

    public bool[] Button_push;
    public bool[] Button_touch;

    /*
    public bool ButtonOne_pushed;       0
    public bool ButtonOne_touched;      0
    public bool ButtonTwo_pushed;       1
    public bool ButtonTwo_touched;      1
    public bool ButtonThree_pushed;     2
    public bool ButtonThree_touched;    2
    public bool ButtonFour_pushed;      3
    public bool ButtonFour_touched;     3
    

    //primary = left hand
    public bool PrimaryIndexTrigger_pushed;     4
    public bool PrimaryIndexTrigger_touched;    4
    public bool PrimaryHandTrigger_pushed;      5
    public bool PrimaryHandTrigger_touched;     5
    public bool PrimaryThumbstick_pushed;       6
    public bool PrimaryThumbstick_touched;      6

    //secondary = right hand
    
    public bool SecondaryIndexTrigger_pushed;       7
    public bool SecondaryIndexTrigger_touched;      7
    public bool SecondaryHandTrigger_pushed;        8
    public bool SecondaryHandTrigger_touched;       8
    public bool SecondaryThumbstick_pushed;         9
    public bool SecondaryThumbstick_touched;        9
    
    */
    public float PrimaryThumbstick_directionX;  
    public float PrimaryThumbstick_directionY;
    public float SecondaryThumbstick_directionX;
    public float SecondaryThumbstick_directionY;

    public InputFrame() {
        Button_push = new bool[10];
        Button_touch = new bool[10];
    }
}