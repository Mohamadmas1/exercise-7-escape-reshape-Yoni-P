using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableWall : MonoBehaviour
{
    public List<GameObject> damageStages;

    public void ExplosionDamage()
    {
        if (damageStages.Count > 0)
        {
            GameObject damageStage = damageStages[0];
            damageStages.RemoveAt(0);
            Destroy(damageStage);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
