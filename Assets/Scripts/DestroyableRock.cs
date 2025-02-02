using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DestroyableRock : MonoBehaviour
{
    public DestroyableRockSpawner spawner;
    [SerializeField] private StudioEventEmitter destroyRockEmitter;
    

    private void OnCollisionEnter(Collision collision)
    {
        spawner.TryDestroyRock(collision, gameObject, destroyRockEmitter);
    }
}
