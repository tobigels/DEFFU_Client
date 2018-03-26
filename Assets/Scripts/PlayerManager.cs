﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class PlayerManager : MonoBehaviour {

    #region FIELDS

    private ConnectionManager connectionManager;
    private AvatarConnector_IN[] avatarConnectors_in;
    private AvatarConnector_OUT avatarConnector_OUT;
    private Player localPlayer;
    private Player[] allPlayers;
    private int playerCount;
    private int gameTurn;
    private int frameNumber;
    private float accumulatedTime;
    private bool inputFramesSwitch;
    private float frame_length;
    private bool gameStarted;
    public int frameCount = 5;
    public float gameTurn_length = 0.1f;

    public static PlayerManager instance = null;

    public GameObject avatar_leftController;
    public GameObject avatar_rightController;
    public GameObject avatar_hmd;

    public float globalTime = 0.0f;

    #endregion

    #region METHODS

    // --------------------------------------- Private methods ---------------------------------------

    /// <summary>
    /// check for incoming data and process Inputframes
    /// </summary>
    private void Update() {
        //if (globalTime < 60.0f) {

            if(gameTurn == 1) {
                globalTime = 0.0f;
            }
            connectionManager.CheckForIncomingData();

            accumulatedTime += Time.deltaTime;
            globalTime += Time.deltaTime;

            if (accumulatedTime > frame_length && frameNumber < frameCount) {
                CheckLocalAvatar();

                foreach (Player player in allPlayers) {
                    if (player.Id != 0 && player.Id != localPlayer.Id) {
                        CheckDistantAvatar(player);
                    }
                }
                accumulatedTime -= frame_length;
                frameNumber++;
            }

    //}
    }

    private void Awake() {

        if (instance == null) {
            instance = this;
        } else {
            if (instance != this) {
                Destroy(this);
            }
        }

        connectionManager = new ConnectionManager(this);
        avatarConnector_OUT = null;
        localPlayer = null;
        allPlayers = new Player[connectionManager.MAX_CONNECTIONS];
        avatarConnectors_in = new AvatarConnector_IN[connectionManager.MAX_CONNECTIONS];
        playerCount = 0;
        gameTurn = 0;
        frameNumber = 0;
        accumulatedTime = 0f;
        inputFramesSwitch = false;
        frame_length = gameTurn_length / frameCount;
        gameStarted = false;

        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// buffer inputFrames from local user
    /// </summary>
    private void CheckLocalAvatar() {

        
        if (avatarConnector_OUT == null) {
            avatarConnector_OUT = new AvatarConnector_OUT(localPlayer.Name, localPlayer.Id);
        }

        InputFrame inputFrame = avatarConnector_OUT.getInput();
        inputFrame.frameNumber = frameNumber;

        if(inputFramesSwitch) {
            localPlayer.InputFrames_alpha[frameNumber] = inputFrame;
            avatarConnector_OUT.UpdateDistantAvatarMovement(localPlayer.InputFrames_beta[frameNumber]);
        } else {
            localPlayer.InputFrames_beta[frameNumber] = inputFrame;
            avatarConnector_OUT.UpdateDistantAvatarMovement(localPlayer.InputFrames_alpha[frameNumber]);
        }

        connectionManager.SendData(inputFrame);
    }

    /// <summary>
    /// buffer inputFrames, which have been sent
    /// </summary>
    /// <param name="player"></param>
    private void CheckDistantAvatar(Player player) {

        if(avatarConnectors_in[player.Id - 1] == null) {
            avatarConnectors_in[player.Id - 1] = new AvatarConnector_IN(player.Name, player.Id);
        }

        if(inputFramesSwitch) {
            if (player.InputFrames_beta[frameNumber] != null) {
                avatarConnectors_in[player.Id - 1].UpdateDistantAvatarMovement(player.InputFrames_beta[frameNumber]);
                //avatarConnectors_in[player.Id - 1].UpdateDistantAvatarButtonEvents(player.InputFrames_alpha[frameNumber]);
                player.InputFrames_beta[frameNumber] = null;
            }
        } else {
            if (player.InputFrames_alpha[frameNumber] != null) {
                avatarConnectors_in[player.Id - 1].UpdateDistantAvatarMovement(player.InputFrames_alpha[frameNumber]);
                //avatarConnectors_in[player.Id - 1].UpdateDistantAvatarButtonEvents(player.InputFrames_beta[frameNumber]);
                player.InputFrames_alpha[frameNumber] = null;
            }
        }
    }

    // --------------------------------------- Public methods ---------------------------------------

    public Player[] AllPlayers {
        get {
            return allPlayers;
        }
    }


    public Player LocalPlayer {
        get {
            return localPlayer;
        }
    }


    public ConnectionManager ConnectionManager {
        get {
            return connectionManager;
        }
    }

    public int GameTurn {
        get {
            return gameTurn;
        }
        set {
            gameTurn = value;
        }
    }

    /// <summary>
    /// process incoming DATA-Message
    /// </summary>
    /// <param name="id"></param>
    /// <param name="input"></param>
    public void DataEvent(int id, InputFrame input) {
        avatarConnectors_in[id - 1].UpdateDistantAvatarButtonEvents(input);

        if (inputFramesSwitch) {
            allPlayers[id - 1].InputFrames_alpha[input.frameNumber] = input;
        } else {
            allPlayers[id - 1].InputFrames_beta[input.frameNumber] = input;
        }

    }

    /// <summary>
    /// -save other clients with name and id
    /// -execute NAMERESPONSE
    /// </summary>
    /// <param name="names"></param>
    public void AskNameEvent(Player[] players) {
        bool localPlayerIdSet = false;

        for(int i = 0; i < allPlayers.Length; i++) {
            if(players[i].Id == 0) {
                if(!localPlayerIdSet) {
                    allPlayers[i] = localPlayer;
                    allPlayers[i].Id = i + 1;
                    playerCount = i + 1;
                    localPlayerIdSet = true;
                } else {
                    allPlayers[i] = new Player(0,""); 
                }
            } else {
                allPlayers[i] = new Player(players[i].Id, players[i].Name);
            }
        }

        if(!localPlayerIdSet) {
            Debug.Log("ERROR: local player's id not set");
        } else {
            connectionManager.SendNameResponse(localPlayer.Name);
            SceneManager.LoadScene("Lobby");
        }
    }

    /// <summary>
    /// process NEWCLIENT-Event
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    public void NewClientEvent(int id, string name) {
        if(allPlayers[id - 1].Id == 0) {
            allPlayers[id - 1] = new Player(id, name);
            playerCount++;
        } else {
            Debug.Log("ERROR: Id already set");
        }
    }

    /// <summary>
    /// process DISCONNECT-Event
    /// </summary>
    /// <param name="id"></param>
    public void DisconnectEvent(int id) {
        allPlayers[id - 1].Id = 0;
        allPlayers[id - 1].Name = "";
        avatarConnectors_in[id - 1].DestroyGameObjects();
        avatarConnectors_in[id - 1] = null;
        playerCount--;
    }

    /// <summary>
    /// incoming Lockstep
    /// </summary>
    public void GameTurnEvent() {
        if(!gameStarted) {
            SceneManager.LoadScene("Main");
            gameStarted = true;
        }

        //Reset InputFrame-Buffers
        if(inputFramesSwitch) {
            foreach (Player player in AllPlayers) {
                if (player.Id != 0 && player.Id != localPlayer.Id) {
                    Array.Clear(player.InputFrames_beta, 0, player.InputFrames_beta.Length);
                }
            }
        } else {
            foreach (Player player in AllPlayers) {
                if (player.Id != 0 && player.Id != localPlayer.Id) {
                    Array.Clear(player.InputFrames_alpha, 0, player.InputFrames_alpha.Length);
                }
            }
        }

        //Fire cummulated ButtonEvents in AvatarConnectors
        foreach(AvatarConnector_IN ac_in in avatarConnectors_in) {
            if(ac_in != null) {
                ac_in.FireButtonEventsOnGameTurn();
            }
        }

        avatarConnector_OUT.FireButtonEventsOnGameTurn();


        inputFramesSwitch = !inputFramesSwitch;
        frameNumber = 0;

    }

    /// <summary>
    /// initialize local player and connect to server
    /// </summary>
    /// <param name="name"></param>
    public void InitializeLocalPlayer(string name, string ipAddress) {

        //give player temporary id 99
        localPlayer = new Player(99, name);
        connectionManager.Connect(ipAddress);
    }

    /// <summary>
    /// send ASKGAMESTART
    /// </summary>
    public void StartGame() {
        connectionManager.SendGameStart(gameTurn_length);
    }

    #endregion
}