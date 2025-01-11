using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportOnEnter : MonoBehaviour
{
    [SerializeField] private Transform teleportTarget;
    [SerializeField] private GameObject globalLight;
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Detected collision with " + other.gameObject.name + " With tag " + other.gameObject.tag);
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Teleporting player to " + teleportTarget.position);
            var characterController = other.GetComponent<CharacterController>();
            characterController.enabled = false;
            other.transform.position = teleportTarget.position;
            characterController.enabled = true;
            
            globalLight.SetActive(false);
        }
    }
}
