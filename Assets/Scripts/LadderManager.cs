using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LadderManager : MonoBehaviour
{
    [SerializeField] private XRSocketInteractor ladderSocketInteractor1;
    [SerializeField] private XRSocketInteractor ladderSocketInteractor2;
    [SerializeField] private InteractionLayerMask placedLadderLayerMask;
    
    public static Action OnAllLaddersPlaced;

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
        OnAllLaddersPlaced?.Invoke();
    }

    private void HandleLadderPlacement(XRSocketInteractor currentSocket, XRSocketInteractor nextSocket, SelectEnterEventArgs arg0)
    {
        
        var ladder = arg0.interactableObject.transform.gameObject;
        // Align the ladder with the socket
        // ladder.transform.position = currentSocket.attachTransform.position;
        // ladder.transform.rotation = currentSocket.attachTransform.rotation;

        // Disable interaction layers for the ladder to prevent further grabbing
        var grabInteractable = ladder.GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
            grabInteractable.interactionLayers = placedLadderLayerMask;
        
        currentSocket.interactionLayers = placedLadderLayerMask;

        // Enable climb interaction
        // var climbInteractable = ladder.GetComponent<ClimbInteractable>();
        // if (climbInteractable != null)
        //     climbInteractable.enabled = true;
        
        // Disable the current socket
        // currentSocket.socketActive = false;
        
        // Enable the next socket if available
        if (nextSocket != null)
            nextSocket.socketActive = true;


        // Disable physics and ensure the ladder stays in place
        var rb = ladder.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }

        Debug.Log($"Ladder placed on {currentSocket.name}");
    }
    
    private void OnSocketDeactivated(SelectExitEventArgs arg0)
    {
        Debug.Log("Socket deactivated");
        var ladder = arg0.interactableObject.transform.gameObject;
        var rb = ladder.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }
    }
}
