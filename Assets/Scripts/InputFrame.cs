﻿using System;
using UnityEngine;

[Serializable]
public class InputFrame {
    public int gameTurn;
    public Vector3 controller_right_pos;
    public Quaternion controller_right_rot;
    public Vector3 controller_left_pos;
    public Quaternion controller_left_rot;
    public Vector3 hmd_pos;
    public Quaternion hmd_rot;
}