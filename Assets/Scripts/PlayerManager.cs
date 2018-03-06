using UnityEngine;
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

    private const float FRAME_LENGTH = 0.2f;

    public static PlayerManager instance = null;

    public GameObject avatar_leftController;
    public GameObject avatar_rightController;
    public GameObject avatar_hmd;

    #endregion

    #region METHODS

    // --------------------------------------- Private methods ---------------------------------------

    private void Update() {
        connectionManager.CheckForIncomingData();

        if (gameTurn > 0) {
            accumulatedTime += Time.deltaTime;

            if(accumulatedTime > FRAME_LENGTH) {
                Debug.Log(frameNumber);
                CheckLocalAvatar();

                foreach (Player player in allPlayers) {
                    if (player.Id != 0 && player.Id != localPlayer.Id) {
                        CheckDistantAvatar(player);
                    }
                }
                accumulatedTime -= FRAME_LENGTH;
                frameNumber++;
            }
        } 
    }

    private void Awake() {

        connectionManager = new ConnectionManager(this);
        avatarConnector_OUT = null;
        localPlayer = null;
        allPlayers = new Player[connectionManager.MAX_CONNECTIONS];
        avatarConnectors_in = new AvatarConnector_IN[connectionManager.MAX_CONNECTIONS];
        playerCount = 0;
        gameTurn = 0;
        frameNumber = 0;
        accumulatedTime = 0f;

        if (instance == null) {
            instance = this;
        } else {
            if (instance != this) {
                Destroy(this);
            }
        }

        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// 
    /// </summary>
    private void CheckLocalAvatar() {

        
        if (avatarConnector_OUT == null) {
            avatarConnector_OUT = new AvatarConnector_OUT();
        }

        InputFrame inputFrame = avatarConnector_OUT.getInput();
        inputFrame.frameNumber = frameNumber;

        connectionManager.SendData(inputFrame);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="player"></param>
    private void CheckDistantAvatar(Player player) {

        if(avatarConnectors_in[player.Id - 1] == null) {
            avatarConnectors_in[player.Id - 1] = new AvatarConnector_IN(player.Name);
        }

        if(player.LastInputFrames[frameNumber] != null) {
            avatarConnectors_in[player.Id - 1].UpdataAvatarConnector(player.LastInputFrames[frameNumber]);
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
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="input"></param>
    public void DataEvent(int id, InputFrame input) {
        allPlayers[id - 1].NewestInputFrames[input.frameNumber] = input;
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
    /// 
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
    /// 
    /// </summary>
    /// <param name="id"></param>
    public void DisconnectEvent(int id) {
        allPlayers[id - 1].Id = 0;
        allPlayers[id - 1].Name = "";
        playerCount--;
    }

    /// <summary>
    /// 
    /// </summary>
    public void GameTurnEvent() {
        if(gameTurn == 0) {
            SceneManager.LoadScene("Main");
        }

        foreach(Player player in AllPlayers) {
            if (player.Id != 0 && player.Id != localPlayer.Id) {

                //Array.Clear(player.LastInputFrames, 0, player.LastInputFrames.Length);
                player.NewestInputFrames.CopyTo(player.LastInputFrames, 0);
                //Array.Clear(player.NewestInputFrames, 0, player.NewestInputFrames.Length);
            }
        }
        frameNumber = 0;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    public void InitializeLocalPlayer(string name, string ipAddress) {

        //give player temporary id 99
        localPlayer = new Player(99, name);
        connectionManager.Connect(ipAddress);
    }

    /// <summary>
    /// 
    /// </summary>
    public void StartGame() {
        connectionManager.SendGameStart();
    }

    #endregion
}