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
    private ControllerEventsExtension rightControllerEE;
    private ControllerEventsExtension leftControllerEE;

    private SteamVR_TrackedObject leftController_tracked;
    private SteamVR_TrackedObject rightController_tracked;

    private SteamVR_Controller.Device leftController_device;
    private SteamVR_Controller.Device rightController_device;

    private InputFrame newestInputFrame;

    private bool[] buttonPushStatus = new bool[10];
    private bool[] buttonTouchStatus = new bool[10];

    private bool[] buttonPushSetStatus = new bool[10];
    private bool[] buttonTouchSetStatus = new bool[10];

    #endregion

    #region METHODS

    // --------------------------------------- Private methods ---------------------------------------

    // --------------------------------------- Public methods ---------------------------------------

    public AvatarConnector_OUT() {
        headsetTransform = VRTK_DeviceFinder.DeviceTransform(VRTK_DeviceFinder.Devices.Headset);
        leftController = VRTK_DeviceFinder.DeviceTransform(VRTK_DeviceFinder.Devices.LeftController);
        rightController = VRTK_DeviceFinder.DeviceTransform(VRTK_DeviceFinder.Devices.RightController);
        leftControllerEE = leftController.gameObject.AddComponent<ControllerEventsExtension>();
        rightControllerEE = rightController.gameObject.AddComponent<ControllerEventsExtension>();

        newestInputFrame = new InputFrame();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public InputFrame getInput() {


        if (headsetTransform != null) {
            newestInputFrame.hmd_pos = headsetTransform.position;
            newestInputFrame.hmd_rot = headsetTransform.rotation;
        } else {
            headsetTransform = VRTK_DeviceFinder.DeviceTransform(VRTK_DeviceFinder.Devices.Headset);
        }

        if (leftController != null) {
            newestInputFrame.controller_left_pos = leftController.position;
            newestInputFrame.controller_left_rot = leftController.rotation;

            leftController_tracked = leftController.GetComponentInParent<SteamVR_TrackedObject>();

            if (leftController_tracked != null) {
                leftController_device = SteamVR_Controller.Input((int)leftController_tracked.index);

                newestInputFrame.Button_push[(int)ButtonType.ButtonThree] = leftController_device.GetPress(Valve.VR.EVRButtonId.k_EButton_A);
                newestInputFrame.Button_touch[(int)ButtonType.ButtonThree] = leftController_device.GetTouch(Valve.VR.EVRButtonId.k_EButton_A);
                newestInputFrame.Button_push[(int)ButtonType.ButtonFour] = leftController_device.GetPress(Valve.VR.EVRButtonId.k_EButton_ApplicationMenu);
                newestInputFrame.Button_touch[(int)ButtonType.ButtonFour] = leftController_device.GetTouch(Valve.VR.EVRButtonId.k_EButton_ApplicationMenu);
                newestInputFrame.Button_push[(int)ButtonType.PrimaryIndexTrigger] = leftController_device.GetPress(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger);
                newestInputFrame.Button_touch[(int)ButtonType.PrimaryIndexTrigger] = leftController_device.GetTouch(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger);
                newestInputFrame.Button_push[(int)ButtonType.PrimaryHandTrigger] = leftController_device.GetPress(Valve.VR.EVRButtonId.k_EButton_Grip);
                newestInputFrame.Button_touch[(int)ButtonType.PrimaryHandTrigger] = leftController_device.GetTouch(Valve.VR.EVRButtonId.k_EButton_Grip);
                newestInputFrame.Button_push[(int)ButtonType.PrimaryThumbstick] = leftController_device.GetPress(Valve.VR.EVRButtonId.k_EButton_Axis0);
                newestInputFrame.Button_touch[(int)ButtonType.PrimaryThumbstick] = leftController_device.GetTouch(Valve.VR.EVRButtonId.k_EButton_Axis0);

                newestInputFrame.PrimaryThumbstick_directionX = leftController_device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0).x;
                newestInputFrame.PrimaryThumbstick_directionY = leftController_device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0).y;

            }
        } else {
            leftController = VRTK_DeviceFinder.DeviceTransform(VRTK_DeviceFinder.Devices.LeftController);
            leftControllerEE = leftController.gameObject.AddComponent<ControllerEventsExtension>();
        }

        if (rightController != null) {
            newestInputFrame.controller_right_pos = rightController.position;
            newestInputFrame.controller_right_rot = rightController.rotation;

            rightController_tracked = rightController.GetComponentInParent<SteamVR_TrackedObject>();

            if(rightController_tracked != null) {
                rightController_device = SteamVR_Controller.Input((int)rightController_tracked.index);

                newestInputFrame.Button_push[(int)ButtonType.ButtonOne] = rightController_device.GetPress(Valve.VR.EVRButtonId.k_EButton_A);
                newestInputFrame.Button_touch[(int)ButtonType.ButtonOne] = rightController_device.GetTouch(Valve.VR.EVRButtonId.k_EButton_A);
                newestInputFrame.Button_push[(int)ButtonType.ButtonTwo] = rightController_device.GetPress(Valve.VR.EVRButtonId.k_EButton_ApplicationMenu);
                newestInputFrame.Button_touch[(int)ButtonType.ButtonTwo] = rightController_device.GetTouch(Valve.VR.EVRButtonId.k_EButton_ApplicationMenu);
                newestInputFrame.Button_push[(int)ButtonType.SecondaryIndexTrigger] = rightController_device.GetPress(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger);
                newestInputFrame.Button_touch[(int)ButtonType.SecondaryIndexTrigger] = rightController_device.GetTouch(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger);
                newestInputFrame.Button_push[(int)ButtonType.SecondaryHandTrigger] = rightController_device.GetPress(Valve.VR.EVRButtonId.k_EButton_Grip);
                newestInputFrame.Button_touch[(int)ButtonType.SecondaryHandTrigger] = rightController_device.GetTouch(Valve.VR.EVRButtonId.k_EButton_Grip);
                newestInputFrame.Button_push[(int)ButtonType.SecondaryThumbstick] = rightController_device.GetPress(Valve.VR.EVRButtonId.k_EButton_Axis0);
                newestInputFrame.Button_touch[(int)ButtonType.SecondaryThumbstick] = rightController_device.GetTouch(Valve.VR.EVRButtonId.k_EButton_Axis0);

                newestInputFrame.SecondaryThumbstick_directionX = rightController_device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0).x;
                newestInputFrame.SecondaryThumbstick_directionY = rightController_device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0).y;
            }
        } else {
            rightController = VRTK_DeviceFinder.DeviceTransform(VRTK_DeviceFinder.Devices.RightController);
            rightControllerEE = rightController.gameObject.AddComponent<ControllerEventsExtension>();
        }

        for (int i = 0; i < buttonPushStatus.Length; i++) {
            if (!buttonPushSetStatus[i]) {
                buttonPushStatus[i] = newestInputFrame.Button_push[i];
                buttonPushSetStatus[i] = true;
            }
        }

        for (int i = 0; i < buttonTouchStatus.Length; i++) {
            if (!buttonTouchSetStatus[i]) {
                buttonTouchStatus[i] = newestInputFrame.Button_touch[i];
                buttonTouchSetStatus[i] = true;
            }
        }

        return newestInputFrame;
    }

    /// <summary>
    /// Fire ButtonEvents for each controller in corresponding ControllerEventsExtension
    /// </summary>
    public void FireButtonEventsOnGameTurn() {
        if(rightControllerEE != null) {
            rightControllerEE.FireButtonEvents(buttonPushStatus, buttonTouchStatus, true);
        }
        
        if(leftControllerEE != null) {
            leftControllerEE.FireButtonEvents(buttonPushStatus, buttonTouchStatus, true);
        }   

        for (int i = 0; i < buttonPushSetStatus.Length; i++) {
            buttonPushSetStatus[i] = false;
        }

        for (int i = 0; i < buttonTouchSetStatus.Length; i++) {
            buttonTouchSetStatus[i] = false;
        }

    }

    #endregion
}