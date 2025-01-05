using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class ChestLockController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private XRSocketInteractor socketInteractor;
    
    [SerializeField] private Transform attachedKeyTransform;
    [SerializeField] private InputActionProperty controllerRotationAction;
    [SerializeField] private Transform controllerTransform;
    
    private int openProperty = Animator.StringToHash("Open");
    private int closeProperty = Animator.StringToHash("Close");
    
    private bool _keyHeldInSocket = false;
    private Quaternion _defaultAttachedKeyRotation;
    
    private Quaternion _previousControllerRotation;
    private bool _chestLocked = true;

    private void Start()
    {
        _defaultAttachedKeyRotation = attachedKeyTransform.localRotation;
        _previousControllerRotation = controllerRotationAction.action.ReadValue<Quaternion>();
        _previousControllerRotation = controllerTransform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Key"))
        {
            socketInteractor.enabled = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Key"))
        {
            socketInteractor.enabled = true;
        }
    }
    
    public void OnHoverEnter()
    {
        _keyHeldInSocket = true;
    }
    
    public void OnHoverExit()
    {
        _keyHeldInSocket = false;
    }
    
    private void Update()
    {
        // var currentControllerRotation = controllerRotationAction.action.ReadValue<Quaternion>();
        // var controllerRotationDelta = currentControllerRotation * Quaternion.Inverse(_previousControllerRotation);
        // _previousControllerRotation = currentControllerRotation;
        
        
        
        // Debug.Log(controllerRotationDelta.eulerAngles.z);
        if (_keyHeldInSocket && _chestLocked)
        {
            // // Calculate the rotation delta as an angle with direction
            // var angleDelta = Quaternion.Angle(Quaternion.identity, controllerRotationDelta);
            // var deltaEuler = controllerRotationDelta.eulerAngles;
            // deltaEuler = TranslateToRange(deltaEuler, -180, 180);
            // if (deltaEuler.z < 0)
            // {
            //     angleDelta *= -1;
            // }
            //
            // if (angleDelta > 180)
            // {
            //     angleDelta -= 360;
            // }
            
            var currentControllerRotation = controllerTransform.rotation;
            var rotationDelta = TranslateToRange(currentControllerRotation.eulerAngles, -180, 180) -
                                TranslateToRange(_previousControllerRotation.eulerAngles, -180, 180);
            rotationDelta = TranslateToRange(rotationDelta, -180, 180);

            // Get the current key rotation and translate it to the desired range
            var newRotation = attachedKeyTransform.localRotation.eulerAngles;
            newRotation = TranslateToRange(newRotation, -180, 180);

            // Update and clamp the X rotation
            newRotation.x = Mathf.Clamp(newRotation.x - rotationDelta.z, -90, 0);
            attachedKeyTransform.localRotation = Quaternion.Euler(newRotation);
            
            // if the key is rotated enough, open the chest
            if (attachedKeyTransform.localRotation.x >= 0)
            {
                animator.SetTrigger(openProperty);
                _defaultAttachedKeyRotation.x = 0;
                attachedKeyTransform.localRotation = _defaultAttachedKeyRotation;
                _chestLocked = false;
                socketInteractor.socketActive = false;
                socketInteractor.GetOldestInteractableHovered().transform.GetComponent<XRBaseInteractable>().enabled = false;
            }
        }
        
        _previousControllerRotation = controllerTransform.rotation;
    }

    private Vector3 TranslateToRange(Vector3 newRotation, int min, int max)
    {
        // move range from 0 to 360 to -180 to 180
        if (newRotation.x > 180)
        {
            newRotation.x -= 360;
        }
        if (newRotation.y > 180)
        {
            newRotation.y -= 360;
        }
        if (newRotation.z > 180)
        {
            newRotation.z -= 360;
        }
        
        if (newRotation.x < -180)
        {
            newRotation.x += 360;
        }
        if (newRotation.y < -180)
        {
            newRotation.y += 360;
        }
        if (newRotation.z < -180)
        {
            newRotation.z += 360;
        }
        
        return newRotation;
    }
}
