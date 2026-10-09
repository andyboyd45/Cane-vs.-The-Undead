using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class movement : MonoBehaviour
{
    [Header("Enemy Movement Settings")]
    [SerializeField] private float speed = 2f;
    [Header("Attack Settings")]
    [SerializeField] private int damage = 20;
    [SerializeField] private float attackCooldown = 1f;
    private float attackTimer = 0f;
    private Animator animator;

    private GameObject collidewith = null;
    private bool isAttacking = false;

    //Getting animation component
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    //Handles enemy movement and sees if any collisions were made
    //If the collision was an ally, it will trigger the attack logic
    //Handles cooldown timer for attacks
    void Update()
    {
        if(isAttacking){return;}

        if(collidewith == null || !collidewith.CompareTag("Ally"))
        {
            //Movement to the left
            transform.Translate(-1 * speed * Time.deltaTime,0,0);
            animator.SetBool("isRunning",true);
        }
        else 
        {
            animator.SetBool("isRunning",false);
            Attack(); 
        }
        attackTimer += Time.deltaTime;
    }

    //Attacking logic for the enemy
    void Attack()
    {
        if(attackTimer >= attackCooldown)
        {

            Debug.Log("Attacking " + collidewith.name);

            attackTimer = 0f;
            isAttacking = true;
            animator.SetTrigger("Attack");
        }
    }

    //Called by animation
    //Deals damage to the ally that the enemy is colliding with
    public void DealDamage()
    {
        if (collidewith != null && collidewith.TryGetComponent<Health>(out Health health))
        {
            health.TakeDamage(damage);
        }
    }

    //Tells the script that the attack animation has finished
    public void FinishAttack()
    {
        isAttacking = false;
    }
    //Handles collision with allies and triggers attack animation
    private void OnTriggerEnter2D(Collider2D coll)
    {   
        Debug.Log("Hit " + coll.gameObject.name + " with tag " + coll.gameObject.tag);

        if (coll.gameObject.CompareTag("Ally"))
        {
            collidewith = coll.gameObject;
            animator.SetBool("isAttacking",true);
            animator.SetBool("isRunning",false);
        }

    }

    //Handles when the enemy stops colliding with an ally
    //Will reset enemy to start walking again
    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Ally"))
        {
            collidewith = null;
            animator.SetBool("isAttacking",false);
            animator.SetBool("isRunning",true);
        }
    }

}
