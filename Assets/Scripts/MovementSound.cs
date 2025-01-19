using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class MovementSound : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private StudioEventEmitter movementSoundEmitter;

    private void Start()
    {
        if (characterController == null)
        {
            characterController = GetComponent<CharacterController>();
        }
        
        // movementSoundEmitter.Play();
    }

    private void Update()
    {
        Debug.Log(characterController.velocity.magnitude);
        // movementSoundEmitter.SetParameter("Speed", characterController.velocity.magnitude);
        // Debug.Log(movementSoundEmitter.Params[0].Name + " " + movementSoundEmitter.Params[0].Value);
        // Debug.Log(movementSoundEmitter.Params);
        
        // if (characterController.velocity.magnitude > 0.1f)
        // {
        //     
        // }
        // else
        // {
        //     movementSoundEmitter.SetParameter("Speed", 0);
        // }
    }
}
