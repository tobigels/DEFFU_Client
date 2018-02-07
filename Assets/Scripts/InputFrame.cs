﻿using System;
using UnityEngine;

[Serializable]
public class InputFrame {
    public int gameTurn;
    public SerializableVector3 controller_right_pos;
    public SerializableVector3 controller_right_rot;
    public SerializableVector3 controller_left_pos;
    public SerializableVector3 controller_left_rot;
    public SerializableVector3 hmd_pos;
    public SerializableVector3 hmd_rot;
}