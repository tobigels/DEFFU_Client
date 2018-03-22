using UnityEngine;
using System.Collections;
using System;

public class AvatarConnector_IN : AvatarConnector {

    #region FIELDS

    private GameObject leftController;
    private GameObject rightController;
    private GameObject hmd;

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

    public AvatarConnector_IN(string givenName, int id) {

        avatarFactory = new AvatarFactory();

        playerName = givenName +" | " + id.ToString();

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
    public void DestroyGameObjects() {
        avatarFactory.DestroyObject(leftController);
        avatarFactory.DestroyObject(rightController);
        avatarFactory.DestroyObject(hmd);
    }

    #endregion
}

