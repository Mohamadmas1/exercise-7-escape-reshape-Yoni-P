using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableRockSpawner : MonoBehaviour
{
    [SerializeField] private GameObject rockToDestroy;
    [SerializeField] private GameObject tankGas;
    [SerializeField] private Transform tankSpawnPoint;
    [SerializeField] private GameObject door;
    [SerializeField] private Transform originalDoorTransform;
    [SerializeField] private GameObject rockToThrow;

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

    public void TryDestroyRock(Collision collision, GameObject destroyableRock)
    {
        if (collision.gameObject.CompareTag("RockToThrow"))
        {
            Destroy(destroyableRock);
            Destroy(collision.gameObject);

            spawnTimer = spawnTime;
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
