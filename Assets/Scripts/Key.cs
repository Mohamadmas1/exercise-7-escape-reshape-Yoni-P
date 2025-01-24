using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using FMODUnity;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Key : MonoBehaviour
{
    [SerializeField] private Transform teleportTarget;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject globalLight;
    [SerializeField] private CanvasGroup blackScreen;
    [SerializeField] private StudioGlobalParameterTrigger globalParameterTrigger;
    [SerializeField] private StudioEventEmitter teleportSound;
    
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
        blackScreen.DOFade(1.0f, 0.5f);

        yield return new WaitForSeconds(0.5f);
        
        var characterController = player.GetComponent<CharacterController>();
        characterController.enabled = false;
        player.transform.position = teleportTarget.position;
        teleportTarget.gameObject.SetActive(false);
        characterController.enabled = true;
        teleportSound.Play();
        
        yield return new WaitForSeconds(1.5f);
        blackScreen.DOFade(0.0f, 1.0f);
        globalParameterTrigger.TriggerParameters();
        
        globalLight.SetActive(true);
    }
}
