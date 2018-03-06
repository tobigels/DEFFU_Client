using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ConnectionManager {

    private enum DataEventType_Server {
        DATA = 1,
        NAMERESPONSE = 2,
        ASKGAMESTART = 3
    }

    private enum DataEventType_Client {
        DATA = 1,
        ASKNAME = 2,
        NEWCLIENT = 3,
        DISCONNECT = 4,
        GAMETURN = 5
    }

    #region FIELDS

    private PlayerManager playerManager;
    private SerializationUnit serializationUnit;
    private int port;
    private int hostId;
    private int reliableChannel;
    private int unreliableChannel;
    private int connectionId;
    private bool isConnected;
    private byte error;

    public readonly int MAX_CONNECTIONS = 4;

    #endregion

    #region METHODS

    // --------------------------------------- Private methods ---------------------------------------

    /// <summary>
    /// 
    /// </summary>
    /// <param name="recHostId"></param>
    /// <param name="m"></param>
    private void OnDataEvent(int recHostId, ServerMessage m) {
        DataEventType_Client eventType = (DataEventType_Client)m.Type;
        //var deflateStream = new DeflateStream(memoryStream, CompressionMode.Decompress, true);

        switch (eventType) {
            case DataEventType_Client.DATA:
                Debug.Log("- " + m.Origin + " - DATA received");
                InputFrame input = (InputFrame)serializationUnit.DeserializeHelper(m.Content, m.Content.Length);
                playerManager.DataEvent(m.Origin, input);
                break;
            case DataEventType_Client.ASKNAME:
                Player[] players = (Player[])serializationUnit.DeserializeHelper(m.Content, m.Content.Length);
                Debug.Log("- " + m.Origin + " - ASKNAME " + players);
                playerManager.AskNameEvent(players);
                break;
            case DataEventType_Client.NEWCLIENT:
                Debug.Log("- " + m.Origin + " - NEWCLIENT");
                string name = (string)serializationUnit.DeserializeHelper(m.Content, m.Content.Length);
                playerManager.NewClientEvent(m.Origin, name);
                break;
            case DataEventType_Client.DISCONNECT:
                Debug.Log("- " + m.Origin + " - DISCONNECT");
                playerManager.DisconnectEvent(m.Origin);
                break;
            case DataEventType_Client.GAMETURN:

                //m.Origin holds gameFrame of server
                Debug.Log("GAMETURN");
                playerManager.GameTurn = m.Origin;
                playerManager.GameTurnEvent();
                break;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="m"></param>
    /// <param name="reliable"></param>
    private void SendMessage(Message m, bool reliable) {
        byte error;
        byte[] recBuffer = new byte[1024];
        int bufferSize = 1024;
        /*if(m.Content != null) {
            Debug.Log("Length of sent Data " + m.Content.Length);
        } */   

        recBuffer = serializationUnit.SerializeHelper(m);

        if (reliable) {
            NetworkTransport.Send(hostId, connectionId, reliableChannel, recBuffer, bufferSize, out error);
        } else {
            NetworkTransport.Send(hostId, connectionId, unreliableChannel, recBuffer, recBuffer.Length, out error);
        }
    }

    // --------------------------------------- Public methods ---------------------------------------

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pm"></param>
    public ConnectionManager(PlayerManager pm) {
        playerManager = pm;
        serializationUnit = new SerializationUnit();
        port = 5701;
        isConnected = false;

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ipAddress"></param>
    public void Connect(string ipAddress) {

        try {
            NetworkTransport.Init();
            ConnectionConfig cc = new ConnectionConfig();

            reliableChannel = cc.AddChannel(QosType.Reliable);
            unreliableChannel = cc.AddChannel(QosType.Unreliable);

            HostTopology ht = new HostTopology(cc, MAX_CONNECTIONS);

            hostId = NetworkTransport.AddHost(ht, 0);

            connectionId = NetworkTransport.Connect(hostId, ipAddress, port, 0, out error);

            isConnected = true;
        } catch(Exception error) {
            Debug.Log(error);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void CheckForIncomingData() {

        bool noEventsLeft = false;

        if(isConnected) {
            while (!noEventsLeft) {
                int recHostId;
                int connectionId;
                int channelId;
                byte[] recBuffer = new byte[1024];
                int bufferSize = 1024;
                int dataSize;
                byte error;
                NetworkEventType recData = NetworkTransport.Receive(out recHostId, out connectionId, out channelId, recBuffer, bufferSize, out dataSize, out error);

                switch (recData) {
                    case NetworkEventType.Nothing:
                        noEventsLeft = true;
                        break;
                    case NetworkEventType.DataEvent:
                        ServerMessage m = (ServerMessage)serializationUnit.DeserializeHelper(recBuffer, dataSize);
                        //var memoryStream = new MemoryStream(recBuffer,0,dataSize);
                        //var deflateStream = new DeflateStream(memoryStream, CompressionMode.Decompress, true);
                        OnDataEvent(recHostId, m);
                        break;
                }
            }
        } 
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    public void SendNameResponse(string name) {
        Message m = new Message();
        m.Content = serializationUnit.SerializeHelper(name);
        m.Type = (int)DataEventType_Server.NAMERESPONSE;
        SendMessage(m, true);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="input"></param>
    public void SendData(InputFrame input) {
        Message m = new Message();
        m.Content = serializationUnit.SerializeHelper(input);
        m.Type = (int)DataEventType_Server.DATA;
        SendMessage(m, false);
    }

    /// <summary>
    /// 
    /// </summary>
    public void SendGameStart() {
        Message m = new Message();
        m.Content = null;
        m.Type = (int)DataEventType_Server.ASKGAMESTART;
        SendMessage(m, true);
    }

    #endregion
}
