using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour {

    #region FIELDS

    private ConnectionManager connectionManager;
    private AvatarConnector_OUT avatarConnector_OUT;
    private Player localPlayer;
    private Player[] allPlayers;
    private int playerCount;
    private int gameTurn;

    public static PlayerManager instance = null;

    #endregion

    #region METHODS

    // --------------------------------------- Private methods ---------------------------------------

    private void Update() {
        connectionManager.CheckForIncomingData();

        if (gameTurn > 0 && gameTurn < 199) {

            if(avatarConnector_OUT == null) {
                avatarConnector_OUT = new AvatarConnector_OUT();
            }

            InputFrame inputFrame = avatarConnector_OUT.getInput();
            inputFrame.gameTurn = gameTurn;


            //TODO get INPUT from Main

            connectionManager.SendData(inputFrame);

        }
    }

    private void Awake() {

        connectionManager = new ConnectionManager(this);
        avatarConnector_OUT = null;
        localPlayer = null;
        allPlayers = new Player[connectionManager.MAX_CONNECTIONS];
        playerCount = 0;
        gameTurn = 0;

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
    private void ResetAllPlayers() {
        foreach(Player player in allPlayers) {
            player.InputSet = false;
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
        allPlayers[id - 1].NewestInputFrame = input;
        allPlayers[id - 1].InputSet = true;
    }

    /// <summary>
    /// -save other clients with name and id
    /// -execute NAMERESPONSE
    /// </summary>
    /// <param name="names"></param>
    public void AskNameEvent(Player[] players) {
        bool localPlayerIdSet = false;

        allPlayers = players;

        for (int i = 0; i < players.Length; i++) {
            if (players[i].Id == 0 && !localPlayerIdSet) {
                players[i] = localPlayer;
                players[i].Id = i + 1;
                playerCount = i + 1;
                localPlayerIdSet = true;
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
    /// <param name="gameTurn"></param>
    public void GameTurnEvent(int nGameTurn) {
        if(gameTurn == 0) {
            SceneManager.LoadScene("Main");
        }
        ResetAllPlayers();
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