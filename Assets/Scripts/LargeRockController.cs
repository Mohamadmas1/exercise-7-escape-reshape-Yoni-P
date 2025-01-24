using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class LargeRockController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private int life;
    [SerializeField] private MissionData hammerHints;     
    [SerializeField] private StudioEventEmitter hammerSound;
    
    private int hitProperty = Animator.StringToHash("Hit");
    private int lifeProperty = Animator.StringToHash("Life");
    
    private bool _canBeHit = true;

    private void Start()
    {
        animator.SetInteger(lifeProperty, life);
    }
    
    public void Hit()
    {
        if (!_canBeHit) return;
        
        hammerSound.Play();
        
        life--;
        if (life==0)
        {
            if (hammerHints != null)
            {
                hammerHints.Solved = true;
            }
        }
        animator.SetInteger(lifeProperty, life);
        animator.SetTrigger(hitProperty);
        
        StartCoroutine(HitCooldown());
    }

    private IEnumerator HitCooldown()
    {
        _canBeHit = false;
        yield return new WaitForSeconds(1f);
        _canBeHit = true;
    }
}
