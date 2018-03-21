using System;
using UnityEngine;
using VRTK;

public class ControllerEventsExtension : VRTK_ControllerEvents {

    #region FIELDS

    private AvatarConnector_IN avatarConnector;
    private bool[] buttonPush = new bool[5];
    private bool[] buttonTouch = new bool[5];
    private bool[] buttonPush_previous = new bool[5];
    private bool[] buttonTouch_previous = new bool[5];


    #endregion

    #region METHODS

    // --------------------------------------- Private methods ---------------------------------------

    // --------------------------------------- Public methods ---------------------------------------

    public AvatarConnector_IN AvatarConnector {
        get {
            return avatarConnector; }
        set {
            avatarConnector = value;
        }
    }

    public void FireButtonEvents(bool[] buttonPushStatus, bool[] buttonTouchStatus, bool isRight) {

        //Extract buttonStatus for either left or right controller 
        if (isRight) {
            Array.Copy(buttonPushStatus, 5, buttonPush, 0, 5);
            Array.Copy(buttonTouchStatus, 5, buttonTouch, 0, 5);
        } else {
            Array.Copy(buttonPushStatus, 0, buttonPush, 0, 5);
            Array.Copy(buttonTouchStatus, 0, buttonTouch, 0, 5);
        }

        
        /*
         * 0    =   Button One/Three 
         * 1    =   Button Two/Four
         * 2    =   IndexTrigger Secondary/Primary
         * 3    =   HandTrigger Secondary/Primary
         * 4    =   Thumbstick Secondary/Primary
         * */


        if (buttonPush[0] != buttonPush_previous[0]) {
            if(buttonPush[0]) {
                //ButtonOne Pressed
                Debug.Log("Button One pressed" + PlayerManager.instance.globalTime);
                OnButtonOnePressed(SetControllerEvent(ref buttonOnePressed, true, 1f));
                EmitAlias(ButtonAlias.ButtonOnePress, true, 1f, ref buttonOnePressed);
            } else {
                //ButtonOne Released
                Debug.Log("Button One released" + PlayerManager.instance.globalTime);
                OnButtonOneReleased(SetControllerEvent(ref buttonOnePressed, false, 0f));
                EmitAlias(ButtonAlias.ButtonOnePress, false, 0f, ref buttonOnePressed);
            }
        }

        if (buttonPush[1] != buttonPush_previous[1]) {
            if (buttonPush[1]) {
                //ButtonTwo Pressed
                Debug.Log("Button Two pressed" + PlayerManager.instance.globalTime);
                OnButtonTwoPressed(SetControllerEvent(ref buttonTwoPressed, true, 1f));
                EmitAlias(ButtonAlias.ButtonTwoPress, true, 1f, ref buttonTwoPressed);
            } else {
                //ButtonTwo Released
                Debug.Log("Button Two released" + PlayerManager.instance.globalTime);
                OnButtonTwoReleased(SetControllerEvent(ref buttonTwoPressed, false, 0f));
                EmitAlias(ButtonAlias.ButtonTwoPress, false, 0f, ref buttonTwoPressed);
            }
        }

        if (buttonPush[2] != buttonPush_previous[2]) {
            if (buttonPush[2]) {
                //Trigger Pressed
                Debug.Log("Trigger pressed" + PlayerManager.instance.globalTime);
                OnTriggerPressed(SetControllerEvent(ref triggerPressed, true, 0.0f));
                EmitAlias(ButtonAlias.TriggerPress, true, 0.0f, ref triggerPressed);
            } else {
                // Trigger Pressed end
                Debug.Log("Trigger released" + PlayerManager.instance.globalTime);
                OnTriggerReleased(SetControllerEvent(ref triggerPressed, false, 0f));
                EmitAlias(ButtonAlias.TriggerPress, false, 0f, ref triggerPressed);
            }
        }

        if (buttonPush[3] != buttonPush_previous[3]) {
            if (buttonPush[3]) {
                //Grip Pressed
                Debug.Log("Grip pressed" + PlayerManager.instance.globalTime);
                OnGripPressed(SetControllerEvent(ref gripPressed, true, 0.0f));
                EmitAlias(ButtonAlias.GripPress, true, 0.0f, ref gripPressed);
            } else {
                //Grip Pressed End
                Debug.Log("Grip released" + PlayerManager.instance.globalTime);
                OnGripReleased(SetControllerEvent(ref gripPressed, false, 0f));
                EmitAlias(ButtonAlias.GripPress, false, 0f, ref gripPressed);
            }
        }

        if (buttonPush[4] != buttonPush_previous[4]) {
            if (buttonPush[4]) {
                //Touchpad Pressed
                Debug.Log("Thumbstick pressed" + PlayerManager.instance.globalTime);
                OnTouchpadPressed(SetControllerEvent(ref touchpadPressed, true, 1f));
                EmitAlias(ButtonAlias.TouchpadPress, true, 1f, ref touchpadPressed);
            } else {
                //Touchpad Released
                Debug.Log("Thumbstick released" + PlayerManager.instance.globalTime);
                OnTouchpadReleased(SetControllerEvent(ref touchpadPressed, false, 0f));
                EmitAlias(ButtonAlias.TouchpadPress, false, 0f, ref touchpadPressed);
            }
        }

        if(buttonTouch[0] != buttonTouch_previous[0]) {
            if(buttonTouch[0]) {
                //ButtonOne Touched
                OnButtonOneTouchStart(SetControllerEvent(ref buttonOneTouched, true, 1f));
                EmitAlias(ButtonAlias.ButtonOneTouch, true, 1f, ref buttonOneTouched);
            } else {
                //ButtonOne Touched End
                OnButtonOneTouchEnd(SetControllerEvent(ref buttonOneTouched, false, 0f));
                EmitAlias(ButtonAlias.ButtonOneTouch, false, 0f, ref buttonOneTouched);
            }
        }

        if (buttonTouch[1] != buttonTouch_previous[1]) {
            if (buttonTouch[1]) {
                //ButtonTwo Touched
                OnButtonTwoTouchStart(SetControllerEvent(ref buttonTwoTouched, true, 1f));
                EmitAlias(ButtonAlias.ButtonTwoTouch, true, 1f, ref buttonTwoTouched);
            } else {
                //ButtonTwo Touched End
                OnButtonTwoTouchEnd(SetControllerEvent(ref buttonTwoTouched, false, 0f));
                EmitAlias(ButtonAlias.ButtonTwoTouch, false, 0f, ref buttonTwoTouched);
            }
        }

        if (buttonTouch[2] != buttonTouch_previous[2]) {
            if (buttonTouch[2]) {
                //Trigger Touched
                OnTriggerTouchStart(SetControllerEvent(ref triggerTouched, true, 0.0f));
                EmitAlias(ButtonAlias.TriggerTouch, true, 0.0f, ref triggerTouched);
            } else {
                //Trigger Touch End
                OnTriggerTouchEnd(SetControllerEvent(ref triggerTouched, false, 0f));
                EmitAlias(ButtonAlias.TriggerTouch, false, 0f, ref triggerTouched);
            }
        }

        if (buttonTouch[3] != buttonTouch_previous[3]) {
            if (buttonTouch[3]) {
                //Grip Touched
                OnGripTouchStart(SetControllerEvent(ref gripTouched, true, 0.0f));
                EmitAlias(ButtonAlias.GripTouch, true, 0.0f, ref gripTouched);
            } else {
                // Grip Touch End
                OnGripTouchEnd(SetControllerEvent(ref gripTouched, false, 0f));
                EmitAlias(ButtonAlias.GripTouch, false, 0f, ref gripTouched);
            }
        }

        if (buttonTouch[4] != buttonTouch_previous[4]) {
            if (buttonTouch[4]) {
                //Touchpad Touched
                OnTouchpadTouchStart(SetControllerEvent(ref touchpadTouched, true, 1f));
                EmitAlias(ButtonAlias.TouchpadTouch, true, 1f, ref touchpadTouched);
            } else {
                //Touchpad Untouched
                OnTouchpadTouchEnd(SetControllerEvent(ref touchpadTouched, false, 0f));
                EmitAlias(ButtonAlias.TouchpadTouch, false, 0f, ref touchpadTouched);
                touchpadAxis = Vector2.zero;
            }
        }

        //save current buttonStatus for next GameTurn
        Array.Copy(buttonPush, buttonPush_previous, 5);
        Array.Copy(buttonTouch, buttonTouch_previous, 5);

    }

    protected override void Update() {
        
    }

    #endregion
}

