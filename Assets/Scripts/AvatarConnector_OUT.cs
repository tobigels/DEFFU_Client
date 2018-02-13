using System;
using UnityEngine;
using VRTK;

internal class AvatarConnector_OUT {

    #region FIELDS

    private Transform headsetTransform;

    private GameObject leftController;
    private GameObject rightController;

    private SteamVR_TrackedObject leftController_tracked;
    private SteamVR_TrackedObject rightController_tracked;

    private SteamVR_Controller.Device leftController_device;
    private SteamVR_Controller.Device rightController_device;

    private InputFrame previousFrame;

    #endregion

    #region METHODS

    // --------------------------------------- Private methods ---------------------------------------

    // --------------------------------------- Public methods ---------------------------------------

    public AvatarConnector_OUT() {
        headsetTransform = VRTK_DeviceFinder.DeviceTransform(VRTK_DeviceFinder.Devices.Headset);
        leftController = VRTK_DeviceFinder.GetControllerLeftHand();
        rightController = VRTK_DeviceFinder.GetControllerRightHand();
        previousFrame = new InputFrame();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public InputFrame getInput() {
        InputFrame inputFrame = new InputFrame(previousFrame);

        if (headsetTransform != null) {
            inputFrame.hmd_pos = headsetTransform.position;
            inputFrame.hmd_rot = headsetTransform.rotation;
        } else {
            headsetTransform = VRTK_DeviceFinder.DeviceTransform(VRTK_DeviceFinder.Devices.Headset);
        }

        if (leftController != null) {
            inputFrame.controller_left_pos = leftController.transform.position;
            inputFrame.controller_left_rot = leftController.transform.rotation;

            leftController_tracked = leftController.GetComponent<SteamVR_TrackedObject>();
            leftController_device = SteamVR_Controller.Input((int)leftController_tracked.index);

            //
            inputFrame.ButtonThree_pushed = leftController_device.GetPress(Valve.VR.EVRButtonId.k_EButton_A);
            inputFrame.ButtonThree_touched = (inputFrame.ButtonThree_pushed ? true : leftController_device.GetTouch(Valve.VR.EVRButtonId.k_EButton_A));
            inputFrame.ButtonFour_pushed = leftController_device.GetPress(Valve.VR.EVRButtonId.k_EButton_ApplicationMenu);
            inputFrame.ButtonFour_touched = (inputFrame.ButtonFour_pushed ? true : leftController_device.GetTouch(Valve.VR.EVRButtonId.k_EButton_ApplicationMenu));
            inputFrame.PrimaryIndexTrigger_pushed = leftController_device.GetPress(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger);
            inputFrame.PrimaryIndexTrigger_touched = (inputFrame.PrimaryIndexTrigger_pushed ? true : leftController_device.GetTouch(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger));
            inputFrame.PrimaryHandTrigger_pushed = leftController_device.GetPress(Valve.VR.EVRButtonId.k_EButton_Axis0);
            inputFrame.PrimaryHandTrigger_touched = (inputFrame.PrimaryHandTrigger_pushed ? true : leftController_device.GetTouch(Valve.VR.EVRButtonId.k_EButton_Axis0));
            inputFrame.PrimaryThumbstick_pushed = leftController_device.GetPress(Valve.VR.EVRButtonId.k_EButton_Axis0);
            inputFrame.PrimaryThumbstick_touched = (inputFrame.PrimaryThumbstick_pushed ? true : leftController_device.GetTouch(Valve.VR.EVRButtonId.k_EButton_Axis0));
            //

            inputFrame.PrimaryThumbstick_directionX = leftController_device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0).x;
            inputFrame.PrimaryThumbstick_directionY = leftController_device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0).y;
        } else {
            leftController = VRTK_DeviceFinder.GetControllerLeftHand();
        }

        if (rightController != null) {
            inputFrame.controller_right_pos = rightController.transform.position;
            inputFrame.controller_right_rot = leftController.transform.rotation;

            rightController_tracked = rightController.GetComponent<SteamVR_TrackedObject>();
            rightController_device = SteamVR_Controller.Input((int)rightController_tracked.index);

            inputFrame.ButtonOne_pushed = rightController_device.GetPressDown(Valve.VR.EVRButtonId.k_EButton_A);
            inputFrame.ButtonOne_touched = (inputFrame.ButtonOne_pushed ? true : rightController_device.GetTouch(Valve.VR.EVRButtonId.k_EButton_A));
            inputFrame.ButtonTwo_pushed = rightController_device.GetPress(Valve.VR.EVRButtonId.k_EButton_ApplicationMenu);
            inputFrame.ButtonTwo_touched = (inputFrame.ButtonTwo_pushed ? true : rightController_device.GetTouch(Valve.VR.EVRButtonId.k_EButton_ApplicationMenu));
            inputFrame.SecondaryIndexTrigger_pushed = rightController_device.GetPress(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger);
            inputFrame.SecondaryIndexTrigger_touched = (inputFrame.SecondaryIndexTrigger_pushed ? true : rightController_device.GetTouch(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger));
            inputFrame.SecondaryHandTrigger_pushed = rightController_device.GetPress(Valve.VR.EVRButtonId.k_EButton_Axis0);
            inputFrame.SecondaryHandTrigger_touched = (inputFrame.SecondaryHandTrigger_pushed ? true : rightController_device.GetTouch(Valve.VR.EVRButtonId.k_EButton_Axis0));
            inputFrame.SecondaryThumbstick_pushed = rightController_device.GetPress(Valve.VR.EVRButtonId.k_EButton_Axis0);
            inputFrame.SecondaryThumbstick_touched = (inputFrame.SecondaryThumbstick_pushed ? true : rightController_device.GetTouch(Valve.VR.EVRButtonId.k_EButton_Axis0));

            inputFrame.SecondaryThumbstick_directionX = rightController_device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0).x;
            inputFrame.SecondaryThumbstick_directionY = rightController_device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0).y;
        } else {
            rightController = VRTK_DeviceFinder.GetControllerRightHand();
        }

        return inputFrame;
    }

    #endregion
}