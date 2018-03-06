using UnityEngine;
using System.Collections;

public class AvatarConnector_IN {

    #region FIELDS

    private GameObject leftController;
    private GameObject rightController;
    private GameObject hmd;

    private AvatarFactory avatarFactory;

    private string playerName;
    private InputFrame lastInput;

    #endregion

    #region METHODS

    // --------------------------------------- Private methods ---------------------------------------

    private void Button_Push_Helper(bool buttonStatus, int buttonEventNumber) {
        if (buttonStatus != lastInput.Button_push[buttonEventNumber]) {
            Debug.Log("Button " + buttonEventNumber + " touched by " + playerName);
            if (buttonStatus) {
                //fire ButtonPushDown
            } else {
                //fire ButtonPushUp
            }
        }
    }

    private void Button_Touch_Helper(bool buttonStatus, int buttonEventNumber) {
        if (buttonStatus != lastInput.Button_touch[buttonEventNumber]) {
            Debug.Log("Button " + buttonEventNumber + " touched by " + playerName);
            if (buttonStatus) {
                //fire ButtonTouchDown
            } else {
                //fire ButtonTouchUp
            }
        }
    }

    // --------------------------------------- Public methods ---------------------------------------

    public string PlayerName {
        get {
            return playerName;
        }
        set {
            playerName = value;
        }

    }

    public AvatarConnector_IN(string givenName) {

        avatarFactory = new AvatarFactory();

        playerName = givenName;

        GameObject player = new GameObject();
        player.name = playerName;

        //hmd = GameObject.CreatePrimitive(PrimitiveType.Cube);
        if(PlayerManager.instance.avatar_hmd != null) {
            hmd = avatarFactory.InstantiateObject(PlayerManager.instance.avatar_hmd);
            hmd.name = "hmd";
            hmd.transform.parent = player.transform;
        } else {
            Debug.Log("Error: HMD-Prefab is empty");
        }

        //leftController = GameObject.CreatePrimitive(PrimitiveType.Cube);
        if(PlayerManager.instance.avatar_leftController != null) {
            leftController = avatarFactory.InstantiateObject(PlayerManager.instance.avatar_leftController);
            leftController.name = "leftController";
            leftController.transform.parent = player.transform;
        } else {
            Debug.Log("Error: leftController-Prefab is empty");
        }

        //rightController = GameObject.CreatePrimitive(PrimitiveType.Cube);
        if(PlayerManager.instance.avatar_rightController != null) {
            rightController = avatarFactory.InstantiateObject(PlayerManager.instance.avatar_rightController);
            rightController.name = "rightController";
            rightController.transform.parent = player.transform;
        } else {
            Debug.Log("Error: rightController-Prefab is empty");
        }

        lastInput = new InputFrame();
    }

    public void UpdateAvatarConnector(InputFrame inputFrame) {
        hmd.transform.position = inputFrame.hmd_pos;
        hmd.transform.rotation = inputFrame.hmd_rot;

        rightController.transform.position = inputFrame.controller_right_pos;
        rightController.transform.rotation = inputFrame.controller_right_rot;

        leftController.transform.position = inputFrame.controller_left_pos;
        leftController.transform.rotation = inputFrame.controller_left_rot;

        Button_Push_Helper(inputFrame.Button_push[(int)ButtonType.ButtonOne], (int)ButtonType.ButtonOne);
        Button_Push_Helper(inputFrame.Button_push[(int)ButtonType.ButtonTwo], (int)ButtonType.ButtonTwo);
        Button_Push_Helper(inputFrame.Button_push[(int)ButtonType.ButtonThree], (int)ButtonType.ButtonThree);
        Button_Push_Helper(inputFrame.Button_push[(int)ButtonType.ButtonFour], (int)ButtonType.ButtonFour);
        Button_Push_Helper(inputFrame.Button_push[(int)ButtonType.PrimaryIndexTrigger], (int)ButtonType.PrimaryIndexTrigger);
        Button_Push_Helper(inputFrame.Button_push[(int)ButtonType.PrimaryHandTrigger], (int)ButtonType.PrimaryHandTrigger);
        Button_Push_Helper(inputFrame.Button_push[(int)ButtonType.PrimaryThumbstick], (int)ButtonType.PrimaryThumbstick);
        Button_Push_Helper(inputFrame.Button_push[(int)ButtonType.SecondaryIndexTrigger], (int)ButtonType.SecondaryIndexTrigger);
        Button_Push_Helper(inputFrame.Button_push[(int)ButtonType.SecondaryHandTrigger], (int)ButtonType.SecondaryHandTrigger);
        Button_Push_Helper(inputFrame.Button_push[(int)ButtonType.SecondaryThumbstick], (int)ButtonType.SecondaryThumbstick);

        Button_Touch_Helper(inputFrame.Button_touch[(int)ButtonType.ButtonOne], (int)ButtonType.ButtonOne);
        Button_Touch_Helper(inputFrame.Button_touch[(int)ButtonType.ButtonTwo], (int)ButtonType.ButtonTwo);
        Button_Touch_Helper(inputFrame.Button_touch[(int)ButtonType.ButtonThree], (int)ButtonType.ButtonThree);
        Button_Touch_Helper(inputFrame.Button_touch[(int)ButtonType.ButtonFour], (int)ButtonType.ButtonFour);
        Button_Touch_Helper(inputFrame.Button_touch[(int)ButtonType.PrimaryIndexTrigger], (int)ButtonType.PrimaryIndexTrigger);
        Button_Touch_Helper(inputFrame.Button_touch[(int)ButtonType.PrimaryHandTrigger], (int)ButtonType.PrimaryHandTrigger);
        Button_Touch_Helper(inputFrame.Button_touch[(int)ButtonType.PrimaryThumbstick], (int)ButtonType.PrimaryThumbstick);
        Button_Touch_Helper(inputFrame.Button_touch[(int)ButtonType.SecondaryIndexTrigger], (int)ButtonType.SecondaryIndexTrigger);
        Button_Touch_Helper(inputFrame.Button_touch[(int)ButtonType.SecondaryHandTrigger], (int)ButtonType.SecondaryHandTrigger);
        Button_Touch_Helper(inputFrame.Button_touch[(int)ButtonType.SecondaryThumbstick], (int)ButtonType.SecondaryThumbstick);

        lastInput = inputFrame;
    }

    #endregion
}

public class AvatarFactory : MonoBehaviour{

    public GameObject InstantiateObject(GameObject prefab) {
        return Instantiate(prefab);
    }

}