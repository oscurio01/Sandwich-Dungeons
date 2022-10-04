using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthSystem), typeof(Animator), typeof(Animation))]
public class DeadAction : MonoBehaviour
{
    Animator animator;
    HealthSystem myHealthSystem;
    bool onlyOnce = false;

    public string triggerDead = "";

    private void Start()
    {
        animator = GetComponent<Animator>();
        myHealthSystem = GetComponent<HealthSystem>();
    }

    private void Update()
    {
        if(myHealthSystem.isDead)
        {
            animator.SetBool(triggerDead, true);
            GetComponent<Collider>().enabled = false;
        }
    }
}
