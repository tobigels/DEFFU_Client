using UnityEngine;

public class AvatarConnector_IN {

    #region FIELDS

    private GameObject leftController;
    private GameObject rightController;
    private GameObject hmd;

    #endregion

    #region METHODS

    // --------------------------------------- Private methods ---------------------------------------

    // --------------------------------------- Public methods ---------------------------------------

    public AvatarConnector_IN(string playerName) {

        //Instantiate Avatar with player

        hmd = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        hmd.name = playerName;
    }

    public void UpdataAvatarConnector(InputFrame inputFrame) {
        hmd.transform.position = inputFrame.hmd_pos;
    }

    #endregion
}