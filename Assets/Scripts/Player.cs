using System;

[Serializable]
public class Player {

    #region FIELDS

    private int id;
    private string name;

    [NonSerialized]
    private InputFrame newestInputFrame;

    [NonSerialized]
    private bool inputSet;

    #endregion

    #region METHODS

    // --------------------------------------- Private methods ---------------------------------------

    // --------------------------------------- Public methods ---------------------------------------

    public Player() {
    }

    public Player(int nId, string nName) {
        id = nId;
        name = nName;
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

    public InputFrame NewestInputFrame {
        get {
            return newestInputFrame;
        }
        set {
            newestInputFrame = value;
        }
    }

    public bool InputSet {
        get {
            return inputSet;
        }
        set {
            inputSet = value;
        }
    }

    #endregion
}
