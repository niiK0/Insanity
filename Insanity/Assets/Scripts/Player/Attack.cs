using StatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private Animator anim;
    public int noOfClicks = 0;
    public bool canHitNext;
    public bool isAttacking = false;

    [SerializeField] private GameInput gameInput;
    [SerializeField] Collider weapCollider;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        noOfClicks = 0;
        canHitNext = true;
        gameInput.OnAttackAction += StartCombo;
        weapCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if (isAttacking)
        //{
        //    weapCollider.enabled = true;
        //}
        //else
        //{
        //    weapCollider.enabled = false;
        //}
    }

    void StartCombo()
    {
        if (canHitNext)
        {
            noOfClicks++;
        }

        if (noOfClicks == 1)
        {
            isAttacking = true;
            anim.SetInteger("attack", 1);
        }
    }

    public void VerifyCombo()
    {
        canHitNext = false;

        if(anim.GetCurrentAnimatorStateInfo(0).IsName("Hit1") && noOfClicks == 1)
        {
            anim.SetInteger("attack", 0);
            canHitNext = true;
            noOfClicks = 0;
            isAttacking = false;
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Hit1") && noOfClicks >= 2)
        {
            anim.SetInteger("attack", 2);
            canHitNext = true;
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Hit2") && noOfClicks == 2)
        {
            anim.SetInteger("attack", 0);
            canHitNext = true;
            noOfClicks = 0;
            isAttacking = false;
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Hit2") && noOfClicks >= 3)
        {
            anim.SetInteger("attack", 3);
            canHitNext = true;
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Hit3"))
        {
            anim.SetInteger("attack", 0);
            canHitNext = true;
            noOfClicks = 0;
            isAttacking = false;
        }

    }

    private void PlayAttackSound()
    {
        AudioManager.instance.Play("PlayerAttack");
    }

    private void DisablePlayerWeaponCollider()
    {
        weapCollider.GetComponent<Collider>().enabled = false;
    }

    private void EnablePlayerWeaponCollider()
    {
        weapCollider.GetComponent<Collider>().enabled = true;
    }
}
