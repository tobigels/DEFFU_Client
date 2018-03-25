using System;
using UnityEngine;
using Valve.VR;
using VRTK;

public class AvatarConnector_OUT : AvatarConnector {

    #region FIELDS

    private Transform headsetTransform;
    private Transform leftControllerTransform;
    private Transform rightControllerTransform;

    private SteamVR_TrackedObject leftController_tracked;
    private SteamVR_TrackedObject rightController_tracked;

    private SteamVR_Controller.Device leftController_device;
    private SteamVR_Controller.Device rightController_device;

    private InputFrame newestInputFrame;

    #endregion

    #region METHODS

    // --------------------------------------- Private methods ---------------------------------------

    private void CheckLeft() {
        leftControllerTransform = VRTK_DeviceFinder.DeviceTransform(VRTK_DeviceFinder.Devices.LeftController);

        if(leftControllerTransform.gameObject.GetComponent<VRTK_ControllerEvents>()) {
            UnityEngine.Object.DestroyObject(leftControllerTransform.gameObject.GetComponent<VRTK_ControllerEvents>());
        }

    }

    private void CheckRight() {
        rightControllerTransform = VRTK_DeviceFinder.DeviceTransform(VRTK_DeviceFinder.Devices.RightController);

        if (rightControllerTransform.gameObject.GetComponent<VRTK_ControllerEvents>()) {
            UnityEngine.Object.DestroyObject(rightControllerTransform.gameObject.GetComponent<VRTK_ControllerEvents>());
        }

    }

    // --------------------------------------- Public methods ---------------------------------------

    public AvatarConnector_OUT(string givenName, int id) {
        headsetTransform = VRTK_DeviceFinder.DeviceTransform(VRTK_DeviceFinder.Devices.Headset);
        CheckLeft();
        CheckRight();

        InitializeComponents(givenName, id);

        foreach(MeshRenderer mr in hmd.GetComponentsInChildren<MeshRenderer>()) {
            mr.enabled = false;
        }

        leftController.GetComponent<MeshRenderer>().enabled = false;
        rightController.GetComponent<MeshRenderer>().enabled = false;

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

        if (leftControllerTransform != null) {
            newestInputFrame.controller_left_pos = leftControllerTransform.position;
            newestInputFrame.controller_left_rot = leftControllerTransform.rotation;

            leftController_tracked = leftControllerTransform.GetComponentInParent<SteamVR_TrackedObject>();

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
            CheckLeft();
        }

        if (rightControllerTransform != null) {
            newestInputFrame.controller_right_pos = rightControllerTransform.position;
            newestInputFrame.controller_right_rot = rightControllerTransform.rotation;

            rightController_tracked = rightControllerTransform.GetComponentInParent<SteamVR_TrackedObject>();

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
            CheckRight();
        }

        UpdateDistantAvatarButtonEvents(newestInputFrame);
        return newestInputFrame;
    }

    #endregion
}