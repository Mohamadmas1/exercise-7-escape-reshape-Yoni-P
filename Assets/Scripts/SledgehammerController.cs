using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SledgehammerController : MonoBehaviour
{
    [SerializeField] private XRGrabInteractable sledghammerGrabInteractable;
    [SerializeField] private float speedThreshold = 1f;
    
    private float _currentSpeed;
    private Vector3 _previousPosition;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _previousPosition = transform.position;
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.Sleep();
    }

    private void Update()
    {
        _currentSpeed = (transform.position - _previousPosition).magnitude / Time.deltaTime;
        _previousPosition = transform.position;
    }

    private void OnEnable()
    {
        sledghammerGrabInteractable.selectEntered.AddListener(OnSelectEntered);
        sledghammerGrabInteractable.selectExited.AddListener(OnSelectExited);
    }

    private void OnSelectEntered(SelectEnterEventArgs arg0)
    {
        _rigidbody.WakeUp();
    }

    private void OnDisable()
    {
        sledghammerGrabInteractable.selectEntered.RemoveListener(OnSelectEntered);
        sledghammerGrabInteractable.selectExited.RemoveListener(OnSelectExited);
    }

    private void OnSelectExited(SelectExitEventArgs arg0)
    {
        _rigidbody.Sleep();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("LargeRock"))
        {
            // only hit the rock if the sledgehammer is being held
            if (!sledghammerGrabInteractable.isSelected) return;
            
            // only hit the rock if the sledgehammer is moving fast enough
            if (_currentSpeed < speedThreshold) return;
            
            other.gameObject.GetComponent<LargeRockController>().Hit();
        }
    }
}
