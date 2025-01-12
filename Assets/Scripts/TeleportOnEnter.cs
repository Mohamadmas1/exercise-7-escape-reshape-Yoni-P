using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TeleportOnEnter : MonoBehaviour
{
    [SerializeField] private Transform teleportTarget;
    [SerializeField] private GameObject globalLight;
    [SerializeField] private CanvasGroup blackScreen;

    private bool _canTeleport = true;
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Detected collision with " + other.gameObject.name + " With tag " + other.gameObject.tag);
        if (_canTeleport && other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Teleporting player to " + teleportTarget.position);
            var characterController = other.GetComponent<CharacterController>();
            characterController.enabled = false;
            other.transform.position = teleportTarget.position;
            characterController.enabled = true;
            
            globalLight.SetActive(false);
            _canTeleport = false;
            StartCoroutine(TeleportCoroutine(other.gameObject));
        }
    }
    
    private IEnumerator TeleportCoroutine(GameObject other)
    {
        blackScreen.DOFade(1.0f, 0.5f);
        var characterController = other.GetComponent<CharacterController>();
        characterController.enabled = false;
        other.transform.position = teleportTarget.position;
        characterController.enabled = true;
        yield return new WaitForSeconds(1.5f);
        blackScreen.DOFade(0.0f, 1.0f);
    }
}
