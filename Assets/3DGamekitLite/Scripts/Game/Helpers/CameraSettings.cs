using System;
using Cinemachine;
using UnityEngine;

namespace Gamekit3D
{
    public class CameraSettings : MonoBehaviour
    {
        public enum InputChoice
        {
            KeyboardAndMouse, Controller,
        }

        [Serializable]
        public struct InvertSettings
        {
            public bool invertX;
            public bool invertY;
        }


        public Transform follow;
        public Transform lookAt;
        public CinemachineFreeLook keyboardAndMouseCamera;
        public CinemachineFreeLook controllerCamera;
        public InputChoice inputChoice;
        public InvertSettings keyboardAndMouseInvertSettings;
        public InvertSettings controllerInvertSettings;
        public bool allowRuntimeCameraSettingsChanges;

        string keyboardandmousefreelookrig = "KeyboardAndMouseFreeLookRig";
        string ellen = "Ellen";
        string controllerfreelookrig = "ControllerFreeLookRig";
        string headtarget = "HeadTarget";

        public CinemachineFreeLook Current
        {
            get { return inputChoice == InputChoice.KeyboardAndMouse ? keyboardAndMouseCamera : controllerCamera; }
        }

        void Reset()
        {

            Transform keyboardAndMouseCameraTransform = transform.Find(keyboardandmousefreelookrig);
            if (keyboardAndMouseCameraTransform != null)
                keyboardAndMouseCamera = keyboardAndMouseCameraTransform.GetComponent<CinemachineFreeLook>();


            Transform controllerCameraTransform = transform.Find(controllerfreelookrig);
            if (controllerCameraTransform != null)
                controllerCamera = controllerCameraTransform.GetComponent<CinemachineFreeLook>();

            PlayerController playerController = FindObjectOfType<PlayerController>();

            if (playerController != null && playerController.name == ellen)
            {
                follow = playerController.transform;


                lookAt = follow.Find(headtarget);

                if (playerController.cameraSettings == null)
                    playerController.cameraSettings = this;
            }
        }

        void Awake()
        {
            UpdateCameraSettings();
        }

        void Update()
        {
            if (allowRuntimeCameraSettingsChanges)
            {
                UpdateCameraSettings();
            }
        }

        void UpdateCameraSettings()
        {
            keyboardAndMouseCamera.Follow = follow;
            keyboardAndMouseCamera.LookAt = lookAt;
            keyboardAndMouseCamera.m_XAxis.m_InvertInput = keyboardAndMouseInvertSettings.invertX;
            keyboardAndMouseCamera.m_YAxis.m_InvertInput = keyboardAndMouseInvertSettings.invertY;

            controllerCamera.m_XAxis.m_InvertInput = controllerInvertSettings.invertX;
            controllerCamera.m_YAxis.m_InvertInput = controllerInvertSettings.invertY;
            controllerCamera.Follow = follow;
            controllerCamera.LookAt = lookAt;

            keyboardAndMouseCamera.Priority = inputChoice == InputChoice.KeyboardAndMouse ? 1 : 0;
            controllerCamera.Priority = inputChoice == InputChoice.Controller ? 1 : 0;
        }
    }
}
