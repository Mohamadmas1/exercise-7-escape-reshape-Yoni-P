using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LadderManager : MonoBehaviour
{
    [SerializeField] private XRSocketInteractor ladderSocketInteractor1;
    [SerializeField] private XRSocketInteractor ladderSocketInteractor2;

    private void Start()
    {
        // Disable the second socket initially
        ladderSocketInteractor2.socketActive = false;
    }

    private void OnEnable()
    {
        ladderSocketInteractor1.selectEntered.AddListener(OnLadder1Placed);
        ladderSocketInteractor2.selectEntered.AddListener(OnLadder2Placed);
    }
    
    private void OnDisable()
    {
        ladderSocketInteractor1.selectEntered.RemoveListener(OnLadder1Placed);
        ladderSocketInteractor2.selectEntered.RemoveListener(OnLadder2Placed);
    }

    private void OnLadder1Placed(SelectEnterEventArgs arg0)
    {
        HandleLadderPlacement(ladderSocketInteractor1, ladderSocketInteractor2, arg0);
    }
    
    private void OnLadder2Placed(SelectEnterEventArgs arg0)
    {
        HandleLadderPlacement(ladderSocketInteractor2, null, arg0);
    }

    private void HandleLadderPlacement(XRSocketInteractor currentSocket, XRSocketInteractor nextSocket, SelectEnterEventArgs arg0)
    {
        // Disable the current socket
        // currentSocket.interactionLayers = LayerMask.GetMask("Nothing");
        currentSocket.GetComponent<Collider>().enabled = false;
        
        // Enable the next socket if available
        if (nextSocket != null)
            nextSocket.socketActive = true;

        var ladder = arg0.interactableObject.transform.gameObject;

        // Disable physics and ensure the ladder stays in place
        var rb = ladder.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        // Align the ladder with the socket
        ladder.transform.position = currentSocket.transform.position;
        ladder.transform.rotation = currentSocket.transform.rotation;

        // Disable interaction layers for the ladder to prevent further grabbing
        var grabInteractable = ladder.GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
            grabInteractable.interactionLayers = LayerMask.GetMask("Nothing");

        // Enable climb interaction
        var climbInteractable = ladder.GetComponent<ClimbInteractable>();
        if (climbInteractable != null)
            climbInteractable.enabled = true;

        Debug.Log($"Ladder placed on {currentSocket.name}");
    }
}