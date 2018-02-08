using System;
using UnityEngine;

[Serializable]
public class SerializableQuaternion {

    #region FIELDS

    public float x;
    public float y;
    public float z;
    public float w;

    #endregion

    #region METHODS

    /// <summary>
    /// 
    /// </summary>
    /// <param name="nX"></param>
    /// <param name="nY"></param>
    /// <param name="nZ"></param>
    /// <param name="nW"></param>
    public SerializableQuaternion(float nX, float nY, float nZ, float nW) {
        x = nX;
        y = nY;
        z = nZ;
        w = nW;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override string ToString() {
        return string.Format("[{0}, {1}, {2}, {3}]", x, y, z, w);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="nSQuat"></param>
    public static implicit operator Quaternion(SerializableQuaternion nSQuat) {
        return new Quaternion(nSQuat.x, nSQuat.y, nSQuat.z, nSQuat.w);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="nQuat"></param>
    public static implicit operator SerializableQuaternion(Quaternion nQuat) {
        return new SerializableQuaternion(nQuat.x, nQuat.y, nQuat.z, nQuat.w);
    }
    #endregion
}