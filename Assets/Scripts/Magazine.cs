using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magazine : MonoBehaviour
{
    public int ammo;

    [SerializeField] GameObject topBullet;
    [SerializeField] GameObject secondTopBullet;

    public void Shoot()
    {
        ammo--;
        if (ammo == 1)
        {
            secondTopBullet.SetActive(false);
        }
        else if (ammo == 0)
        {
            topBullet.SetActive(false);
        }
    }
}
