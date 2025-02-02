using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DestroyableRockSpawner : MonoBehaviour
{
    [SerializeField] private GameObject rockToDestroy;
    [SerializeField] private GameObject tankGas;
    [SerializeField] private Transform tankSpawnPoint;
    [SerializeField] private GameObject door;
    [SerializeField] private Transform originalDoorTransform;
    [SerializeField] private GameObject rockToThrow;
    [SerializeField] private XRBaseInteractable hiddenGasCan;

    [SerializeField] private float spawnTime = 1.0f;
    private float spawnTimer = 0.0f;

    private void Update()
    {
        if (spawnTimer > 0)
        {
            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0)
            {
                Spawn();
                spawnTimer = 0;
            }
        }
    }

    public void TryDestroyRock(Collision collision, GameObject destroyableRock, StudioEventEmitter destroyRockEmitter)
    {
        if (collision.gameObject.CompareTag("RockToThrow"))
        {
            Destroy(destroyableRock);
            Destroy(collision.gameObject);
            destroyRockEmitter.Play();

            spawnTimer = spawnTime;
            hiddenGasCan.enabled = true;
        }
    }

    private void Spawn()
    {
        // restore the door to its original transform
        door.transform.position = originalDoorTransform.position;
        door.transform.rotation = originalDoorTransform.rotation;

        // spawn the destroyable rock and create a new tank gas
        Instantiate(rockToDestroy, transform.position, transform.rotation).GetComponent<DestroyableRock>().spawner = this;
        Instantiate(tankGas, tankSpawnPoint.position, tankSpawnPoint.rotation);
    }
}
