using System.Collections;
using System.Collections.Generic;
using Gamekit3D;
using UnityEngine;

public class GameKitPatchBehaviour : MonoBehaviour
{
    public Transform playerTransform;
    public Transform headTarget;
    public PlayerController playerController;
    public void SetFollowAndLookAt()
    {
        var cameraSettings = FindObjectOfType<CameraSettings>();
        cameraSettings.follow = playerTransform;
        cameraSettings.lookAt = headTarget;
    }

    public void SetCameraSettings(CameraSettings cameraSettings)
    {
        playerController.cameraSettings = cameraSettings;
    }
}
