using System;
using UnityEngine;

[Serializable]
public class InputFrame {
    public int gameTurn;
    public Vector3 controller_right_pos;
    public Quaternion controller_right_rot;
    public Vector3 controller_left_pos;
    public Quaternion controller_left_rot;
    public Vector3 hmd_pos;
    public Quaternion hmd_rot;

    public bool ButtonOne_pushed;
    public bool ButtonOne_touched;
    public bool ButtonTwo_pushed;
    public bool ButtonTwo_touched;
    public bool ButtonThree_pushed;
    public bool ButtonThree_touched;
    public bool ButtonFour_pushed;
    public bool ButtonFour_touched;

    //primary = left hand
    public bool PrimaryIndexTrigger_pushed;
    public bool PrimaryIndexTrigger_touched;
    public bool PrimaryHandTrigger_pushed;
    public bool PrimaryHandTrigger_touched;
    public bool PrimaryThumbstick_pushed;
    public bool PrimaryThumbstick_touched;

    //secondary = right hand
    public bool SecondaryIndexTrigger_pushed;
    public bool SecondaryIndexTrigger_touched;
    public bool SecondaryHandTrigger_pushed;
    public bool SecondaryHandTrigger_touched;
    public bool SecondaryThumbstick_pushed;
    public bool SecondaryThumbstick_touched;

    public float PrimaryThumbstick_directionX;
    public float PrimaryThumbstick_directionY;
    public float SecondaryThumbstick_directionX;
    public float SecondaryThumbstick_directionY;

    public InputFrame() {
        this.ButtonOne_pushed = false;
        this.ButtonOne_touched = false;
        this.ButtonTwo_pushed = false;
        this.ButtonTwo_touched = false;
        this.ButtonThree_pushed = false;
        this.ButtonThree_touched = false;
        this.ButtonFour_pushed = false;
        this.ButtonFour_touched = false;
        this.PrimaryIndexTrigger_pushed = false;
        this.PrimaryIndexTrigger_touched = false;
        this.PrimaryHandTrigger_pushed = false;
        this.PrimaryHandTrigger_touched = false;
        this.PrimaryThumbstick_pushed = false;
        this.PrimaryThumbstick_touched = false;
        this.SecondaryIndexTrigger_pushed = false;
        this.SecondaryIndexTrigger_touched = false;
        this.SecondaryHandTrigger_pushed = false;
        this.SecondaryHandTrigger_touched = false;
        this.SecondaryThumbstick_pushed = false;
        this.SecondaryThumbstick_touched = false;   
    }

    public InputFrame(InputFrame nFrame) {
        this.ButtonOne_pushed = nFrame.ButtonOne_pushed;
        this.ButtonOne_touched = nFrame.ButtonOne_touched;
        this.ButtonTwo_pushed = nFrame.ButtonTwo_pushed;
        this.ButtonTwo_touched = nFrame.ButtonTwo_touched;
        this.ButtonThree_pushed = nFrame.ButtonThree_pushed;
        this.ButtonThree_touched = nFrame.ButtonThree_touched;
        this.ButtonFour_pushed = nFrame.ButtonFour_pushed;
        this.ButtonFour_touched = nFrame.ButtonFour_touched;
        this.PrimaryIndexTrigger_pushed = nFrame.PrimaryIndexTrigger_pushed;
        this.PrimaryIndexTrigger_touched = nFrame.PrimaryIndexTrigger_touched;
        this.PrimaryHandTrigger_pushed = nFrame.PrimaryHandTrigger_pushed;
        this.PrimaryHandTrigger_touched = nFrame.PrimaryHandTrigger_touched;
        this.PrimaryThumbstick_pushed = nFrame.PrimaryThumbstick_pushed;
        this.PrimaryThumbstick_touched = nFrame.PrimaryThumbstick_touched;
        this.SecondaryIndexTrigger_pushed = nFrame.SecondaryIndexTrigger_pushed;
        this.SecondaryIndexTrigger_touched = nFrame.SecondaryIndexTrigger_touched;
        this.SecondaryHandTrigger_pushed = nFrame.SecondaryHandTrigger_pushed;
        this.SecondaryHandTrigger_touched = nFrame.SecondaryHandTrigger_touched;
        this.SecondaryThumbstick_pushed = nFrame.SecondaryThumbstick_pushed;
        this.SecondaryThumbstick_touched = nFrame.SecondaryThumbstick_touched;
    }
}