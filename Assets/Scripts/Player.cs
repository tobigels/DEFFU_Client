using System;

[Serializable]
public class Player {

    #region FIELDS

    private int id;
    private string name;
    
    [NonSerialized]
    private InputFrame[] newestInputFrames;
    private InputFrame[] lastInputFrames;

    
    #endregion

    #region METHODS

    // --------------------------------------- Private methods ---------------------------------------

    // --------------------------------------- Public methods ---------------------------------------

    public Player() {
        newestInputFrames = new InputFrame[10];
        lastInputFrames = new InputFrame[10];
    }

    public Player(int nId, string nName) {
        id = nId;
        name = nName;
        newestInputFrames = new InputFrame[10];
        lastInputFrames = new InputFrame[10];
    }

    public int Id {
        get {
            return id;
        }
        set {
            id = value;
        }
    }

    public string Name {
        get {
            return name;
        }
        set {
            name = value;
        }
    }

    public InputFrame[] NewestInputFrames {
        get {
            return newestInputFrames;
        }
        set {
            newestInputFrames = value;
        }
    }

    public InputFrame[] LastInputFrames {
        get {
            return lastInputFrames;
        }
        set {
            lastInputFrames = value;
        }
    }


    #endregion
}
