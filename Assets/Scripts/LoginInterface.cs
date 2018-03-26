using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginInterface : MonoBehaviour {

    #region FIELDS

    private InputField[] inputField_IP;
    private PlayerManager playerManager;

    public InputField inputField_name;
    public GameObject inputField_IP_wrapper;

    #endregion

    #region METHODS

    // --------------------------------------- Private methods ---------------------------------------

    /// <summary>
    /// check if inputFields are set
    /// </summary>
    /// <returns></returns>
    private bool CheckInputFields() {
        if (inputField_name.text == "") {
            Debug.Log("ERROR: please enter name");
            return false;
        }

        inputField_IP = inputField_IP_wrapper.GetComponentsInChildren<InputField>();

        foreach (InputField field in inputField_IP) {
            if (field.text == "") {
                Debug.Log("ERROR: please enter IP");
                return false;
            }
        }
        return true;
    }

    private void Start() {
        playerManager = PlayerManager.instance;
    }

    // --------------------------------------- Public methods ---------------------------------------

    /// <summary>
    /// connect to server
    /// </summary>
    public void Connect() {
        if(CheckInputFields() && playerManager != null) {
            string ipAddress = inputField_IP[0].text + "." + inputField_IP[1].text + "." + inputField_IP[2].text + "." + inputField_IP[3].text;
            playerManager.InitializeLocalPlayer(inputField_name.text, ipAddress);
        }
    }

    #endregion
}