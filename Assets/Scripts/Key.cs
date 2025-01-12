using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Key : MonoBehaviour
{
    [SerializeField] private Transform teleportTarget;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject globalLight;
    [SerializeField] private CanvasGroup blackScreen;
    
    private bool isCollected = false;
    private XRBaseInteractable interactable;

    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }
        
        interactable = GetComponent<XRBaseInteractable>();
    }
    
    private void OnEnable()
    {
        if (interactable == null)
        {
            interactable = GetComponent<XRBaseInteractable>();
        }
        
        interactable.selectEntered.AddListener(OnKeyCollected);
    }
    
    private void OnDisable()
    {
        interactable.selectEntered.RemoveListener(OnKeyCollected);
    }

    private void OnKeyCollected(SelectEnterEventArgs args)
    {
        if (isCollected)
        {
            return;
        }
        Debug.Log("Key collected by " + args.interactorObject.transform.root.name);
        isCollected = true;
        Debug.Log("Key collected");
        StartCoroutine(OnKeyCollectedCoroutine());
    }
    
    private IEnumerator OnKeyCollectedCoroutine()
    {
        Debug.Log("Teleporting player to " + teleportTarget.position);
        blackScreen.alpha = 1.0f;
        var characterController = player.GetComponent<CharacterController>();
        characterController.enabled = false;
        player.transform.position = teleportTarget.position;
        teleportTarget.gameObject.SetActive(false);
        characterController.enabled = true;
        yield return new WaitForSeconds(1.0f);
        blackScreen.alpha = 0.0f;
        
        globalLight.SetActive(true);
    }
}
