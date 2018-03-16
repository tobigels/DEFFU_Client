using UnityEngine;
using System.Collections;
using System;

public class AvatarConnector_IN {

    #region FIELDS

    private GameObject leftController;
    private GameObject rightController;
    private GameObject hmd;
    private ControllerEventsExtension rightControllerEE;
    private ControllerEventsExtension leftControllerEE;

    private bool[] buttonPushStatus = new bool[10];
    private bool[] buttonTouchStatus = new bool[10];

    private bool[] buttonPushSetStatus = new bool[10];
    private bool[] buttonTouchSetStatus = new bool[10];

    private AvatarFactory avatarFactory;

    private string playerName;

    #endregion

    #region METHODS

    // --------------------------------------- Private methods ---------------------------------------


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

            leftControllerEE = leftController.AddComponent<ControllerEventsExtension>();
            leftControllerEE.AvatarConnector = this;
        } else {
            Debug.Log("Error: leftController-Prefab is empty");
        }

        //rightController = GameObject.CreatePrimitive(PrimitiveType.Cube);
        if(PlayerManager.instance.avatar_rightController != null) {
            rightController = avatarFactory.InstantiateObject(PlayerManager.instance.avatar_rightController);
            rightController.name = "rightController";
            rightController.transform.parent = player.transform;

            rightControllerEE = rightController.AddComponent<ControllerEventsExtension>();
            rightControllerEE.AvatarConnector = this;
            
        } else {
            Debug.Log("Error: rightController-Prefab is empty");
        }
    }

    /// <summary>
    /// update buttonstatus for each controller in correponding ControllerEventsExtension
    /// </summary>
    /// <param name="inputFrame"></param>
    public void UpdateDistantAvatarMovement(InputFrame inputFrame) {

        hmd.transform.position = inputFrame.hmd_pos;
        hmd.transform.rotation = inputFrame.hmd_rot;

        rightController.transform.position = inputFrame.controller_right_pos;
        rightController.transform.rotation = inputFrame.controller_right_rot * Quaternion.Euler(39.4f, 0, 0);

        leftController.transform.position = inputFrame.controller_left_pos;
        leftController.transform.rotation = inputFrame.controller_left_rot * Quaternion.Euler(39.4f, 0, 0);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="inputFrame"></param>
    public void UpdateDistantAvatarButtonEvents(InputFrame inputFrame) {

        //set button events, if they haven't been set in this gameturn before

        for (int i = 0; i < buttonPushStatus.Length; i++) {
            Debug.Log(" - in UDAP - ");
            if (!buttonPushSetStatus[i]) {
                buttonPushStatus[i] = inputFrame.Button_push[i];
                if(inputFrame.Button_push[i]) {
                    Debug.Log("TRUE");
                }
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

    /// <summary>
    /// Fire ButtonEvents for each controller in corresponding ControllerEventsExtension
    /// </summary>
    public void FireButtonEventsOnGameTurn() {
        rightControllerEE.FireButtonEvents(buttonPushStatus, buttonTouchStatus, true);
        leftControllerEE.FireButtonEvents(buttonPushStatus, buttonTouchStatus, true);

        //reset boolstatusset, for next gameturn-iteration 
        for(int i = 0; i < buttonPushSetStatus.Length; i++) {
            buttonPushSetStatus[i] = false;
        }

        for (int i = 0; i < buttonTouchSetStatus.Length; i++) {
            buttonTouchSetStatus[i] = false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void DestroyGameObjects() {
        avatarFactory.DestroyObject(leftController);
        avatarFactory.DestroyObject(rightController);
        avatarFactory.DestroyObject(hmd);
    }

    #endregion
}

public class AvatarFactory : MonoBehaviour{

    public GameObject InstantiateObject(GameObject prefab) {
        return Instantiate(prefab);
    }

    public void DestroyObject(GameObject go) {
        Destroy(go);
    }

}