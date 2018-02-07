using UnityEngine;
using System;
using System.Collections;


[Serializable]
public class SerializableVector3 {

    #region FIELDS

    public float x;
    public float y;
    public float z;

    #endregion

    #region METHODS

    /// <summary>
    /// 
    /// </summary>
    /// <param name="nX"></param>
    /// <param name="nY"></param>
    /// <param name="nZ"></param>
    public SerializableVector3(float nX, float nY, float nZ) {
        x = nX;
        y = nY;
        z = nZ;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override string ToString() {
        return string.Format("[{0}, {1}, {2}]", x, y, z);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="nSVector"></param>
    public static implicit operator Vector3(SerializableVector3 nSVector) {
        return new Vector3(nSVector.x, nSVector.y, nSVector.z);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="nVector"></param>
    public static implicit operator SerializableVector3(Vector3 nVector) {
        return new SerializableVector3(nVector.x, nVector.y, nVector.z);
    }
    #endregion
}