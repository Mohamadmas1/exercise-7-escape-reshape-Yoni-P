using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableRock : MonoBehaviour
{
    public DestroyableRockSpawner spawner;

    private void OnCollisionEnter(Collision collision)
    {
        spawner.TryDestroyRock(collision, gameObject);
    }
}
