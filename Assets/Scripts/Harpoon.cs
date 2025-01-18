using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harpoon : MonoBehaviour
{
    [SerializeField] private float minVelocity = 1f;

    void OnCollisionEnter(Collision collision)
    {
        // check if the harpoon is being shot by checking the velocity
        if (GetComponent<Rigidbody>().velocity.magnitude < minVelocity) return;

        Explosive explosive = collision.gameObject.GetComponent<Explosive>();
        if (explosive)
        {
            explosive.Explode();
        }
    }
}
