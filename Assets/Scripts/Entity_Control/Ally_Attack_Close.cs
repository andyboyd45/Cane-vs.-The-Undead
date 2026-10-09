using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private int damage = 20;
    [SerializeField] private float attackCooldown = 1f;

    private float attackTimer = 0f;

    private GameObject collidewith;

    private bool isAttacking = false;

    private Animator animator;

    private BoxCollider2D box;

    void Awake()
    {
        animator = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();
    }
    
    // <summary>
    //Finds the closest enemy within its hitbox and attacks if the tag is an enemy
    //</summary>
    GameObject FindClosestEnemy()
    {
        Collider2D[] enemies = Physics2D.OverlapBoxAll(box.bounds.center, box.bounds.size, 0f);

        foreach(Collider2D enemy in enemies)
        {
            if(!enemy.CompareTag("Enemy")){continue;}
            if(enemy.TryGetComponent<Health>(out Health health) && health.isDead){continue;}

            //Debug.Log("Found closest enemy: " + enemy.gameObject.name);
            return enemy.gameObject;
        }
        //Debug.Log("No enemies found in range.");
        return null;
    }

    

    //Update is called once per frame
    //Always checking if enemy is in hitbox
    //Also updates cooldown timer for attacks
    void Update()
    {
        collidewith = FindClosestEnemy();

        if(isAttacking){return;}

        if(collidewith != null && collidewith.CompareTag("Enemy"))
        {
            //Debug.Log("Attacking enemy: " + collidewith.name);
            attack();
        }

        attackTimer += Time.deltaTime;

    }

    //<summary>
    //Handles the attack logic 
    //checking if the attack cooldown has passed and triggering the attack animation.
    //</summary>
    void attack()
    {
        if(attackTimer >= attackCooldown)
        {
            attackTimer = 0f;
            isAttacking = true;
            animator.SetTrigger("Attack");
        }
    }

    //<summary>
    //Deals damage to the currently targeted enemy.
    //Method is called by the animation event
    //</summary>
    public void DealDamage()
    {
        if(collidewith != null && collidewith.TryGetComponent<Health>(out Health health))
        {
            //Debug.Log("Dealing " + damage + " damage to " + collidewith.name);
            health.TakeDamage(damage);
        }
    }

    //<summary>
    //Method is called in animation event
    //Tells script that the attack is finished
    //</summary>
    public void FinishAttack()
    {
        Debug.Log("Finished attack.");
        isAttacking = false;
    }

}
