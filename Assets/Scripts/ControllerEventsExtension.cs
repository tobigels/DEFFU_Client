using System;
using UnityEngine;
using VRTK;

public class ControllerEventsExtension : VRTK_ControllerEvents {

    #region FIELDS

    private bool[] buttonPush = new bool[5];
    private bool[] buttonTouch = new bool[5];
    private bool[] buttonPush_previous = new bool[5];
    private bool[] buttonTouch_previous = new bool[5];

    private string gameturns = "";


    #endregion

    #region METHODS

    // --------------------------------------- Private methods ---------------------------------------
    
    private void TestHelper(string name) {
        //Debug.Log(name + PlayerManager.instance.globalTime + " I " + PlayerManager.instance.GameTurn + " I " + gameObject.name);
        //gameturns += " I " + PlayerManager.instance.GameTurn;
        //Debug.Log(gameturns);
    }
        
    // --------------------------------------- Public methods ---------------------------------------

    //Fire Button events to vrtk-components
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
                TestHelper("Button One/Three pressed ");
                OnButtonOnePressed(SetControllerEvent(ref buttonOnePressed, true, 1f));
                EmitAlias(ButtonAlias.ButtonOnePress, true, 1f, ref buttonOnePressed);
            } else {
                //ButtonOne Released
                TestHelper("Button One/Three released ");
                OnButtonOneReleased(SetControllerEvent(ref buttonOnePressed, false, 0f));
                EmitAlias(ButtonAlias.ButtonOnePress, false, 0f, ref buttonOnePressed);
            }
        }

        if (buttonPush[1] != buttonPush_previous[1]) {
            if (buttonPush[1]) {
                //ButtonTwo Pressed
                TestHelper("Button Two/Four pressed ");
                OnButtonTwoPressed(SetControllerEvent(ref buttonTwoPressed, true, 1f));
                EmitAlias(ButtonAlias.ButtonTwoPress, true, 1f, ref buttonTwoPressed);
            } else {
                //ButtonTwo Released
                TestHelper("Button Two/Four released ");
                OnButtonTwoReleased(SetControllerEvent(ref buttonTwoPressed, false, 0f));
                EmitAlias(ButtonAlias.ButtonTwoPress, false, 0f, ref buttonTwoPressed);
            }
        }

        if (buttonPush[2] != buttonPush_previous[2]) {
            if (buttonPush[2]) {
                //Trigger Pressed
                TestHelper("Trigger pressed ");
                OnTriggerPressed(SetControllerEvent(ref triggerPressed, true, 0.0f));
                EmitAlias(ButtonAlias.TriggerPress, true, 0.0f, ref triggerPressed);
            } else {
                // Trigger Pressed end
                TestHelper("Trigger released ");
                OnTriggerReleased(SetControllerEvent(ref triggerPressed, false, 0f));
                EmitAlias(ButtonAlias.TriggerPress, false, 0f, ref triggerPressed);
            }
        }

        if (buttonPush[3] != buttonPush_previous[3]) {
            if (buttonPush[3]) {
                //Grip Pressed
                TestHelper("HandTrigger pressed ");
                OnGripPressed(SetControllerEvent(ref gripPressed, true, 0.0f));
                EmitAlias(ButtonAlias.GripPress, true, 0.0f, ref gripPressed);
            } else {
                //Grip Pressed End
                TestHelper("HandTrigger released ");
                OnGripReleased(SetControllerEvent(ref gripPressed, false, 0f));
                EmitAlias(ButtonAlias.GripPress, false, 0f, ref gripPressed);
            }
        }

        if (buttonPush[4] != buttonPush_previous[4]) {
            if (buttonPush[4]) {
                //Touchpad Pressed
                TestHelper("Thumbstick pressed ");
                OnTouchpadPressed(SetControllerEvent(ref touchpadPressed, true, 1f));
                EmitAlias(ButtonAlias.TouchpadPress, true, 1f, ref touchpadPressed);
            } else {
                //Touchpad Released
                TestHelper("Thumbstick released ");
                OnTouchpadReleased(SetControllerEvent(ref touchpadPressed, false, 0f));
                EmitAlias(ButtonAlias.TouchpadPress, false, 0f, ref touchpadPressed);
            }
        }

        if(buttonTouch[0] != buttonTouch_previous[0]) {
            if(buttonTouch[0]) {
                //ButtonOne Touched
                TestHelper("Button One/Three touched");
                OnButtonOneTouchStart(SetControllerEvent(ref buttonOneTouched, true, 1f));
                EmitAlias(ButtonAlias.ButtonOneTouch, true, 1f, ref buttonOneTouched);
            } else {
                //ButtonOne Touched End
                TestHelper("Button One/Three untouched");
                OnButtonOneTouchEnd(SetControllerEvent(ref buttonOneTouched, false, 0f));
                EmitAlias(ButtonAlias.ButtonOneTouch, false, 0f, ref buttonOneTouched);
            }
        }

        if (buttonTouch[1] != buttonTouch_previous[1]) {
            if (buttonTouch[1]) {
                //ButtonTwo Touched
                TestHelper("Button Two/Four touched");
                OnButtonTwoTouchStart(SetControllerEvent(ref buttonTwoTouched, true, 1f));
                EmitAlias(ButtonAlias.ButtonTwoTouch, true, 1f, ref buttonTwoTouched);
            } else {
                //ButtonTwo Touched End
                TestHelper("Button Two/Four untouched");
                OnButtonTwoTouchEnd(SetControllerEvent(ref buttonTwoTouched, false, 0f));
                EmitAlias(ButtonAlias.ButtonTwoTouch, false, 0f, ref buttonTwoTouched);
            }
        }

        if (buttonTouch[2] != buttonTouch_previous[2]) {
            if (buttonTouch[2]) {
                //Trigger Touched
                TestHelper("Trigger touched");
                OnTriggerTouchStart(SetControllerEvent(ref triggerTouched, true, 0.0f));
                EmitAlias(ButtonAlias.TriggerTouch, true, 0.0f, ref triggerTouched);
            } else {
                //Trigger Touch End
                TestHelper("Trigger untouched");
                OnTriggerTouchEnd(SetControllerEvent(ref triggerTouched, false, 0f));
                EmitAlias(ButtonAlias.TriggerTouch, false, 0f, ref triggerTouched);
            }
        }

        if (buttonTouch[3] != buttonTouch_previous[3]) {
            if (buttonTouch[3]) {
                //Grip Touched
                TestHelper("HandTrigger touched");
                OnGripTouchStart(SetControllerEvent(ref gripTouched, true, 0.0f));
                EmitAlias(ButtonAlias.GripTouch, true, 0.0f, ref gripTouched);
            } else {
                // Grip Touch End
                TestHelper("HandTrigger untouched");
                OnGripTouchEnd(SetControllerEvent(ref gripTouched, false, 0f));
                EmitAlias(ButtonAlias.GripTouch, false, 0f, ref gripTouched);
            }
        }

        if (buttonTouch[4] != buttonTouch_previous[4]) {
            if (buttonTouch[4]) {
                //Touchpad Touched
                TestHelper("Thumbstick touched");
                OnTouchpadTouchStart(SetControllerEvent(ref touchpadTouched, true, 1f));
                EmitAlias(ButtonAlias.TouchpadTouch, true, 1f, ref touchpadTouched);
            } else {
                //Touchpad Untouched
                TestHelper("Thumbstick untouched");
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

