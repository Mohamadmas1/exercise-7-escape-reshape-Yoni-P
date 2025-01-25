using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class Explosive : MonoBehaviour
{
    [SerializeField] private GameObject[] explosionPrefabs;
    [SerializeField] private float explosionForce = 500f;
    [SerializeField] private float explosionRadius = 5f;
    [SerializeField] private StudioEventEmitter explosiveEmitter;
    private bool explodeed = false;

    public void Explode()
    {
        if (explodeed) return;
        explodeed = true;

        // Destroy the explosive object
        GetComponent<Collider>().enabled = false;
        Destroy(gameObject);

        // instantiate random explosion effect
        int index = Random.Range(0, explosionPrefabs.Length);
        Instantiate(explosionPrefabs[index], transform.position, transform.rotation);

        // overlap sphere to detect nearby colliders
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider nearbyObject in colliders)
        {
            // damage nearby objects, Send message to the object
            nearbyObject.SendMessage("ExplosionDamage", SendMessageOptions.DontRequireReceiver);

            // add explosion force to nearby rigidbodies
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
        }
        explosiveEmitter.Play();
    }

    public void ExplosionDamage()
    {
        Explode();
    }
}
