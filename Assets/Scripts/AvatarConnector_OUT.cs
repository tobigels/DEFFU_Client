using System;
using UnityEngine;
using VRTK;

internal class AvatarConnector_OUT {

    #region FIELDS

    private Transform headsetTransform;
    private Transform leftControllerTransform;
    private Transform rightControllerTransform;

    #endregion

    #region METHODS

    // --------------------------------------- Private methods ---------------------------------------

    // --------------------------------------- Public methods ---------------------------------------

    public AvatarConnector_OUT() {
        headsetTransform = VRTK_DeviceFinder.DeviceTransform(VRTK_DeviceFinder.Devices.Headset);
        leftControllerTransform = VRTK_DeviceFinder.DeviceTransform(VRTK_DeviceFinder.Devices.LeftController);
        rightControllerTransform = VRTK_DeviceFinder.DeviceTransform(VRTK_DeviceFinder.Devices.RightController);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public InputFrame getInput() {
        InputFrame inputFrame = new InputFrame();

        if (headsetTransform != null) {
            inputFrame.hmd_pos = headsetTransform.position;
            //inputFrame.hmd_rot = headsetTransform.rotation;
        } else {
            headsetTransform = VRTK_DeviceFinder.DeviceTransform(VRTK_DeviceFinder.Devices.Headset);
        }

        if (leftControllerTransform != null) {
            inputFrame.controller_left_pos = leftControllerTransform.position;
            //inputFrame.controller_left_pos = leftControllerTransform.rotation;
        } else {
            leftControllerTransform = VRTK_DeviceFinder.DeviceTransform(VRTK_DeviceFinder.Devices.LeftController);
        }

        if (rightControllerTransform != null) {
            inputFrame.controller_right_pos = rightControllerTransform.position;
            //inputFrame.controller_right_pos = rightControllerTransform.rotation;
        } else {
            rightControllerTransform = VRTK_DeviceFinder.DeviceTransform(VRTK_DeviceFinder.Devices.RightController);
        }

        return inputFrame;
    }

    #endregion
}