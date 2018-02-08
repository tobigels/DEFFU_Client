using UnityEngine;

public class AvatarConnector_IN {

    #region FIELDS

    #endregion

    #region METHODS

    // --------------------------------------- Private methods ---------------------------------------

    // --------------------------------------- Public methods ---------------------------------------

    public AvatarConnector_IN(string playerName) {

        //Instantiate Avatar with player

        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.name = playerName;
    }

    public void UpdataAvatarConnector(InputFrame inputFrame) {

    }

    #endregion
}