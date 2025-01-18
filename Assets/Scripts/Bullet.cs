using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // raycast to detect if the bullet hit an explosive object
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            Explosive explosive = hit.transform.GetComponent<Explosive>();
            if (explosive)
            {
                explosive.Explode();
            }
        }
    }
}
