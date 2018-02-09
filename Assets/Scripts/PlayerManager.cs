using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour {

    #region FIELDS

    private ConnectionManager connectionManager;
    private AvatarConnector_IN[] avatarConnectors_in;
    private AvatarConnector_OUT avatarConnector_OUT;
    private Player localPlayer;
    private Player[] allPlayers;
    private int playerCount;
    private int gameTurn;
    private float accumulatedTime;

    private const float FRAME_LENGTH = 0.02f;

    public static PlayerManager instance = null;

    #endregion

    #region METHODS

    // --------------------------------------- Private methods ---------------------------------------

    private void Update() {
        connectionManager.CheckForIncomingData();

        if (gameTurn > 0 && gameTurn < 199) {

            accumulatedTime += Time.deltaTime;

            if(accumulatedTime > FRAME_LENGTH) {
                CheckLocalAvatar();

                foreach (Player player in allPlayers) {
                    if (player.Id != 0 && player.Id != localPlayer.Id) {
                        CheckDistantAvatar(player);
                    }
                }
                accumulatedTime -= FRAME_LENGTH;
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
    private void ResetAllPlayers() {
        foreach(Player player in allPlayers) {
            player.InputSet = false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void CheckLocalAvatar() {

        
        if (avatarConnector_OUT == null) {
            avatarConnector_OUT = new AvatarConnector_OUT();
        }

        InputFrame inputFrame = avatarConnector_OUT.getInput();
        inputFrame.gameTurn = gameTurn;
        
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

        avatarConnectors_in[player.Id - 1].UpdataAvatarConnector(player.NewestInputFrame);


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