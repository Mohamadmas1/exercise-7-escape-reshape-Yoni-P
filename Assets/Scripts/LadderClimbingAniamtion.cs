using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class LadderClimbingAniamtion : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera ladderCamera;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private bool test = false;
    
    private IEnumerator Start()
    {
        if (test)
        {
            yield return new WaitForSeconds(5);
            PlayCameraAnimation();
        }
        LadderManager.OnAllLaddersPlaced += PlayCameraAnimation;
    }
    
    
    public void PlayCameraAnimation()
    {
        Debug.Log("Playing camera animation");
        
        Camera.main.GetComponent<TrackedPoseDriver>().trackingType = TrackedPoseDriver.TrackingType.RotationOnly;
        ladderCamera.Priority = 20;
        playerAnimator.SetTrigger("Climb");
    }
}
