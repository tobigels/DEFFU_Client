using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SerializationUnit {

    #region FIELDS

    private BinaryFormatter binaryFormatter;
    private SurrogateSelector ss;

    #endregion

    #region METHODS

    // --------------------------------------- Private methods ---------------------------------------

    // --------------------------------------- Public methods ---------------------------------------

    public SerializationUnit() {
        binaryFormatter = new BinaryFormatter();
        ss = new SurrogateSelector();

        Vector3SerializationSurrogate vectorSS = new Vector3SerializationSurrogate();
        QuaternionSerializationSurrogate quatSS = new QuaternionSerializationSurrogate();

        ss.AddSurrogate(typeof(Vector3), new StreamingContext(StreamingContextStates.All), vectorSS);
        ss.AddSurrogate(typeof(Quaternion), new StreamingContext(StreamingContextStates.All), quatSS);

        binaryFormatter.SurrogateSelector = ss;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="toSerialize"></param>
    /// <returns></returns>
    public byte[] SerializeHelper(object toSerialize) {
        var memoryStream = new MemoryStream();
        byte[] returnValue;
        try {

            binaryFormatter.Serialize(memoryStream, toSerialize);
            returnValue = memoryStream.ToArray();
        } catch (SerializationException e) {
            Debug.Log("Error: failed to deserialize with reason " + e);
            throw;
        } finally {
            memoryStream.Close();
        }

        return returnValue;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="bufferLength"></param>
    /// <returns></returns>
    public object DeserializeHelper(byte[] buffer, int bufferLength) {
        var memoryStream = new MemoryStream(buffer, 0, bufferLength);
        object returnValue;
        try {

            returnValue = binaryFormatter.Deserialize(memoryStream);
        } catch (SerializationException e) {
            Debug.Log("Error: failed to deserialize with reason " + e);
            throw;
        } finally {
            memoryStream.Close();
        }

        return returnValue;
    }

    #endregion
}