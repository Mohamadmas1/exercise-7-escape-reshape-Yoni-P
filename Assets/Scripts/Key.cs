using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Key : MonoBehaviour
{
    [SerializeField] private Transform teleportTarget;
    [SerializeField] private GameObject player;
    
    private bool isCollected = false;

    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }
    }

    private void OnKeyCollected(SelectEnterEventArgs args)
    {
        if (isCollected)
        {
            return;
        }
        
        isCollected = true;
        Debug.Log("Key collected");
        StartCoroutine(OnKeyCollectedCoroutine());
    }
    
    private IEnumerator OnKeyCollectedCoroutine()
    {
        yield return new WaitForSeconds(1.0f);
        var characterController = player.GetComponent<CharacterController>();
        characterController.enabled = false;
        player.transform.position = teleportTarget.position;
        characterController.enabled = true;
    }
}
