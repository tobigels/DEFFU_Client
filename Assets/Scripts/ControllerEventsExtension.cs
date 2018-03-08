using System;
using VRTK;

public class ControllerEventsExtension : VRTK_ControllerEvents {

    #region FIELDS

    private AvatarConnector_IN avatarConnector;
    private bool[] buttonPush = new bool[5];
    private bool[] buttonTouch = new bool[5];


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
        if (isRight) {
            Array.Copy(buttonPushStatus, 5, buttonPush, 0, 5);
            Array.Copy(buttonTouchStatus, 5, buttonTouch, 0, 5);
        } else {
            Array.Copy(buttonPushStatus, 0, buttonPush, 0, 5);
            Array.Copy(buttonTouchStatus, 0, buttonTouch, 0, 5);
        }
    }

    #endregion
}