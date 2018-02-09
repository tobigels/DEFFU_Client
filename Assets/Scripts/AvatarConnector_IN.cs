using UnityEngine;
using System.Collections;

public class AvatarConnector_IN : MonoBehaviour {

    #region FIELDS

    private GameObject leftController;
    private GameObject rightController;
    private GameObject hmd;

    public GameObject _leftController;
    public GameObject _rightController;
    public GameObject _hmd;

    #endregion

    #region METHODS

    // --------------------------------------- Private methods ---------------------------------------

    // --------------------------------------- Public methods ---------------------------------------

    public AvatarConnector_IN(string playerName) {

        //Instantiate Avatar with player

        GameObject player = new GameObject();
        player.name = playerName;

        hmd = (GameObject) Instantiate(_hmd);
        hmd.name = "hmd";
        hmd.transform.parent = player.transform;

        leftController = (GameObject)Instantiate(_leftController);
        leftController.name = "leftController";
        leftController.transform.parent = player.transform;

        rightController = (GameObject)Instantiate(_rightController);
        rightController.name = "rightController";
        rightController.transform.parent = player.transform;

    }

    public void UpdataAvatarConnector(InputFrame inputFrame) {
        hmd.transform.position = inputFrame.hmd_pos;
        hmd.transform.rotation = inputFrame.hmd_rot;

        rightController.transform.position = inputFrame.controller_right_pos;
        rightController.transform.rotation = inputFrame.controller_right_rot;

        leftController.transform.position = inputFrame.controller_left_pos;
        leftController.transform.rotation = inputFrame.controller_left_rot;
    }

    #endregion
}