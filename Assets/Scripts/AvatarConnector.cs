using UnityEngine;

public enum ButtonType {
    ButtonThree = 0,
    ButtonFour = 1,
    PrimaryIndexTrigger = 2,
    PrimaryHandTrigger = 3,
    PrimaryThumbstick = 4,
    ButtonOne = 5,
    ButtonTwo = 6,
    SecondaryIndexTrigger = 7,
    SecondaryHandTrigger = 8,
    SecondaryThumbstick = 9
}

public class AvatarConnector {
    protected ControllerEventsExtension rightControllerEE;
    protected ControllerEventsExtension leftControllerEE;

    protected bool[] buttonPushStatus = new bool[10];
    protected bool[] buttonTouchStatus = new bool[10];

    protected bool[] buttonPushSetStatus = new bool[10];
    protected bool[] buttonTouchSetStatus = new bool[10];

    /// <summary>
    /// Fire ButtonEvents for each controller in corresponding ControllerEventsExtension
    /// </summary>
    public void FireButtonEventsOnGameTurn() {

        if (rightControllerEE != null) {
            rightControllerEE.FireButtonEvents(buttonPushStatus, buttonTouchStatus, true);
        }

        if (leftControllerEE != null) {
            leftControllerEE.FireButtonEvents(buttonPushStatus, buttonTouchStatus, true);
        }

        //reset boolstatusset, for next gameturn-iteration 
        for (int i = 0; i < buttonPushSetStatus.Length; i++) {
            buttonPushSetStatus[i] = false;
        }

        for (int i = 0; i < buttonTouchSetStatus.Length; i++) {
            buttonTouchSetStatus[i] = false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="inputFrame"></param>
    public void UpdateDistantAvatarButtonEvents(InputFrame inputFrame) {

        //set button events, if they haven't been set in this gameturn before

        for (int i = 0; i < buttonPushStatus.Length; i++) {
            if (!buttonPushSetStatus[i]) {
                buttonPushStatus[i] = inputFrame.Button_push[i];
                buttonPushSetStatus[i] = true;
            }
        }

        for (int i = 0; i < buttonTouchStatus.Length; i++) {
            if (!buttonTouchSetStatus[i]) {
                buttonTouchStatus[i] = inputFrame.Button_touch[i];
                buttonTouchSetStatus[i] = true;
            }
        }
    }
}

public class AvatarFactory : MonoBehaviour {

    public GameObject InstantiateObject(GameObject prefab) {
        return Instantiate(prefab);
    }

    public void DestroyObject(GameObject go) {
        Destroy(go);
    }

}