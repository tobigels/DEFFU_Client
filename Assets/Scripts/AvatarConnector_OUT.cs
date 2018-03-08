using System;
using UnityEngine;
using Valve.VR;
using VRTK;

public enum ButtonType {
    ButtonThree = 0,
    ButtonFour = 1,
    PrimaryIndexTrigger = 2,
    PrimaryHandTrigger = 3,
    PrimaryThumbstick = 4,
    ButtonOne = 5,
    ButtonTwo = 6,
    SecondaryIndexTrigger = 7,
    SecondaryHandTrigger = 8,
    SecondaryThumbstick = 9
}

internal class AvatarConnector_OUT {

    #region FIELDS

    private Transform headsetTransform;
    private Transform leftController;
    private Transform rightController;

    private SteamVR_TrackedObject leftController_tracked;
    private SteamVR_TrackedObject rightController_tracked;

    private SteamVR_Controller.Device leftController_device;
    private SteamVR_Controller.Device rightController_device;

    #endregion

    #region METHODS

    // --------------------------------------- Private methods ---------------------------------------

    // --------------------------------------- Public methods ---------------------------------------

    public AvatarConnector_OUT() {
        headsetTransform = VRTK_DeviceFinder.DeviceTransform(VRTK_DeviceFinder.Devices.Headset);
        leftController = VRTK_DeviceFinder.DeviceTransform(VRTK_DeviceFinder.Devices.LeftController);
        rightController = VRTK_DeviceFinder.DeviceTransform(VRTK_DeviceFinder.Devices.RightController);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public InputFrame getInput() {


        InputFrame inputFrame = new InputFrame();

        if (headsetTransform != null) {
            inputFrame.hmd_pos = headsetTransform.position;
            inputFrame.hmd_rot = headsetTransform.rotation;
        } else {
            headsetTransform = VRTK_DeviceFinder.DeviceTransform(VRTK_DeviceFinder.Devices.Headset);
        }

        if (leftController != null) {
            inputFrame.controller_left_pos = leftController.position;
            inputFrame.controller_left_rot = leftController.rotation;

            leftController_tracked = leftController.GetComponentInParent<SteamVR_TrackedObject>();

            if (leftController_tracked != null) {
                leftController_device = SteamVR_Controller.Input((int)leftController_tracked.index);

                inputFrame.Button_push[(int)ButtonType.ButtonThree] = leftController_device.GetPress(Valve.VR.EVRButtonId.k_EButton_A);
                inputFrame.Button_touch[(int)ButtonType.ButtonThree] = leftController_device.GetTouch(Valve.VR.EVRButtonId.k_EButton_A);
                inputFrame.Button_push[(int)ButtonType.ButtonFour] = leftController_device.GetPress(Valve.VR.EVRButtonId.k_EButton_ApplicationMenu);
                inputFrame.Button_touch[(int)ButtonType.ButtonFour] = leftController_device.GetTouch(Valve.VR.EVRButtonId.k_EButton_ApplicationMenu);
                inputFrame.Button_push[(int)ButtonType.PrimaryIndexTrigger] = leftController_device.GetPress(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger);
                inputFrame.Button_touch[(int)ButtonType.PrimaryIndexTrigger] = leftController_device.GetTouch(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger);
                inputFrame.Button_push[(int)ButtonType.PrimaryHandTrigger] = leftController_device.GetPress(Valve.VR.EVRButtonId.k_EButton_Grip);
                inputFrame.Button_touch[(int)ButtonType.PrimaryHandTrigger] = leftController_device.GetTouch(Valve.VR.EVRButtonId.k_EButton_Grip);
                inputFrame.Button_push[(int)ButtonType.PrimaryThumbstick] = leftController_device.GetPress(Valve.VR.EVRButtonId.k_EButton_Axis0);
                inputFrame.Button_touch[(int)ButtonType.PrimaryThumbstick] = leftController_device.GetTouch(Valve.VR.EVRButtonId.k_EButton_Axis0);

                inputFrame.PrimaryThumbstick_directionX = leftController_device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0).x;
                inputFrame.PrimaryThumbstick_directionY = leftController_device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0).y;

            }
        } else {
            leftController = VRTK_DeviceFinder.DeviceTransform(VRTK_DeviceFinder.Devices.LeftController);
        }

        if (rightController != null) {
            inputFrame.controller_right_pos = rightController.position;
            inputFrame.controller_right_rot = rightController.rotation;

            rightController_tracked = rightController.GetComponentInParent<SteamVR_TrackedObject>();

            if(rightController_tracked != null) {
                rightController_device = SteamVR_Controller.Input((int)rightController_tracked.index);

                inputFrame.Button_push[(int)ButtonType.ButtonOne] = rightController_device.GetPress(Valve.VR.EVRButtonId.k_EButton_A);
                inputFrame.Button_touch[(int)ButtonType.ButtonOne] = rightController_device.GetTouch(Valve.VR.EVRButtonId.k_EButton_A);
                inputFrame.Button_push[(int)ButtonType.ButtonTwo] = rightController_device.GetPress(Valve.VR.EVRButtonId.k_EButton_ApplicationMenu);
                inputFrame.Button_touch[(int)ButtonType.ButtonTwo] = rightController_device.GetTouch(Valve.VR.EVRButtonId.k_EButton_ApplicationMenu);
                inputFrame.Button_push[(int)ButtonType.SecondaryIndexTrigger] = rightController_device.GetPress(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger);
                inputFrame.Button_touch[(int)ButtonType.SecondaryIndexTrigger] = rightController_device.GetTouch(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger);
                inputFrame.Button_push[(int)ButtonType.SecondaryHandTrigger] = rightController_device.GetPress(Valve.VR.EVRButtonId.k_EButton_Grip);
                inputFrame.Button_touch[(int)ButtonType.SecondaryHandTrigger] = rightController_device.GetTouch(Valve.VR.EVRButtonId.k_EButton_Grip);
                inputFrame.Button_push[(int)ButtonType.SecondaryThumbstick] = rightController_device.GetPress(Valve.VR.EVRButtonId.k_EButton_Axis0);
                inputFrame.Button_touch[(int)ButtonType.SecondaryThumbstick] = rightController_device.GetTouch(Valve.VR.EVRButtonId.k_EButton_Axis0);

                inputFrame.SecondaryThumbstick_directionX = rightController_device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0).x;
                inputFrame.SecondaryThumbstick_directionY = rightController_device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0).y;
            }
        } else {
            rightController = VRTK_DeviceFinder.DeviceTransform(VRTK_DeviceFinder.Devices.RightController);
        }

        return inputFrame;
    }

    #endregion
}