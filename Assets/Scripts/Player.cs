using System;

[Serializable]
public class Player {

    #region FIELDS

    private int id;
    private string name;
    
    [NonSerialized]
    private InputFrame[] inputFrames_alpha;
    private InputFrame[] inputFrames_beta;

    
    #endregion

    #region METHODS

    // --------------------------------------- Private methods ---------------------------------------

    // --------------------------------------- Public methods ---------------------------------------

    public Player() {
        inputFrames_alpha = new InputFrame[10];
        inputFrames_beta = new InputFrame[10];
    }

    public Player(int nId, string nName) {
        id = nId;
        name = nName;
        inputFrames_alpha = new InputFrame[10];
        inputFrames_beta = new InputFrame[10];
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

    public InputFrame[] InputFrames_alpha {
        get {
            return inputFrames_alpha;
        }
        set {
            inputFrames_alpha = value;
        }
    }

    public InputFrame[] InputFrames_beta {
        get {
            return inputFrames_beta;
        }
        set {
            inputFrames_beta = value;
        }
    }


    #endregion
}
