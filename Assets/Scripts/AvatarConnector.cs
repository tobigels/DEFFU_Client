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

    protected AvatarFactory avatarFactory;

    protected GameObject leftController;
    protected GameObject rightController;
    protected GameObject hmd;

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
            leftControllerEE.FireButtonEvents(buttonPushStatus, buttonTouchStatus, false);
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

    public void InitializeComponents(string givenName, int id) {

        avatarFactory = new AvatarFactory();

        GameObject player = new GameObject();
        player.name = givenName + " | " + id.ToString();

        //hmd = GameObject.CreatePrimitive(PrimitiveType.Cube);
        if (PlayerManager.instance.avatar_hmd != null) {
            hmd = avatarFactory.InstantiateObject(PlayerManager.instance.avatar_hmd);
            hmd.name = "hmd";
            hmd.transform.parent = player.transform;
        } else {
            Debug.Log("Error: HMD-Prefab is empty");
        }

        //leftController = GameObject.CreatePrimitive(PrimitiveType.Cube);
        if (PlayerManager.instance.avatar_leftController != null) {
            leftController = avatarFactory.InstantiateObject(PlayerManager.instance.avatar_leftController);
            leftController.name = "leftController";
            leftController.transform.parent = player.transform;

            leftControllerEE = leftController.AddComponent<ControllerEventsExtension>();
        } else {
            Debug.Log("Error: leftController-Prefab is empty");
        }

        //rightController = GameObject.CreatePrimitive(PrimitiveType.Cube);
        if (PlayerManager.instance.avatar_rightController != null) {
            rightController = avatarFactory.InstantiateObject(PlayerManager.instance.avatar_rightController);
            rightController.name = "rightController";
            rightController.transform.parent = player.transform;

            rightControllerEE = rightController.AddComponent<ControllerEventsExtension>();

        } else {
            Debug.Log("Error: rightController-Prefab is empty");
        }
    }
}

public class AvatarFactory : MonoBehaviour {

    public GameObject InstantiateObject(GameObject prefab) {
        return Instantiate(prefab);
    }

    public void DestroyObject(Transform go) {
        Destroy(go.gameObject);
    }

}