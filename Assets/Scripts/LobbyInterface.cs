using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyInterface : MonoBehaviour {

    #region FIELDS

    private PlayerManager playerManager;
    private Text[] text_clients;

    public Text text_playerName;
    public GameObject clients_wrapper;

    #endregion

    #region METHODS

    // --------------------------------------- Private methods ---------------------------------------

    /// <summary>
    /// 
    /// </summary>
    private void Start() {
        playerManager = PlayerManager.instance;
        text_clients = clients_wrapper.GetComponentsInChildren<Text>();
        text_playerName.text = playerManager.LocalPlayer.Name;
    }

    /// <summary>
    /// 
    /// </summary>
    private void Update() {
        for (int i = 0; i < playerManager.ConnectionManager.MAX_CONNECTIONS; i++) {
            text_clients[i].text = playerManager.AllPlayers[i].Name;
        }
    }

    // --------------------------------------- Public methods ---------------------------------------

    /// <summary>
    /// 
    /// </summary>
    public void StartSession() {
        playerManager.StartGame();
    }

    #endregion
}
