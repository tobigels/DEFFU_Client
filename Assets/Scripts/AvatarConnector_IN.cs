using UnityEngine;
using System.Collections;
using System;

public class AvatarConnector_IN : AvatarConnector {

    #region FIELDS

    #endregion

    #region METHODS

    // --------------------------------------- Private methods ---------------------------------------


    // --------------------------------------- Public methods ---------------------------------------


    public AvatarConnector_IN(string givenName, int id) {
        InitializeComponents(givenName, id);
    }

    

    /// <summary>
    /// Delete component, if distant player has left
    /// </summary>
    public void DestroyGameObjects() {
        avatarFactory.DestroyObject(hmd.transform.parent);
    }

    #endregion
}

