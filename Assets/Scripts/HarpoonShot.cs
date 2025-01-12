using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HarpoonShot : MonoBehaviour
{
    private Rigidbody harpoon;
    private bool isLoaded = false;
    [SerializeField] private float shotForce = 10f;
    [SerializeField] private GameObject socketInteractor;

    [SerializeField] private float shotCooldown = 1f;
    private float shotTimer = 0f;

    private void Update()
    {
        if (shotTimer > 0f)
        {
            shotTimer -= Time.deltaTime;

            if (shotTimer <= 0f)
            {
                shotTimer = 0f;

                // enable the socket interactor
                socketInteractor.SetActive(true);
            }
        }
    }

    // shot the harpoon
    public void OnLoad(SelectEnterEventArgs args)
    {
        harpoon = args.interactableObject.transform.GetComponent<Rigidbody>();
        isLoaded = true;
    }

    public void OnShoot()
    {
        if (!isLoaded) return;

        // detach the harpoon from the socket
        socketInteractor.SetActive(false);
        harpoon.transform.SetParent(null);
        shotTimer = shotCooldown;

        // shoot the harpoon
        harpoon.AddForce(transform.forward * shotForce, ForceMode.Impulse);
        isLoaded = false;
    }
}
