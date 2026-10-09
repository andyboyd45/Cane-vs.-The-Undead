using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally_attack_Far : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private GameObject arrow;
    [SerializeField] private float attackCooldown = 1.5f;
    [SerializeField] private Transform shotpoint;

    [Header("Detection Settings")]
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float sightRange = 8f;

    private Vector2 offset = new Vector2(0.5f,0f);
    private Animator animator; 
    private float attackTimer = 0f;
    private bool isAttacking = false;


    //Getting animation component
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    ///<summary>
    ///Update is called once per frame
    ///Checks if enemy is in line of sight
    ///Also updates cooldown timer for attacks
    ///</summary>
    void Update()
    {
        if (isAttacking){return;}
        if (EnemyInSight())
        {
            Attack();
        }


        attackTimer += Time.deltaTime;
    }

    //Uses raycast to check if an enemy is on the same y axis and to its right
    bool EnemyInSight()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, sightRange, enemyLayer);
        return hit.collider != null;
    }

    //Handles attack logic
    void Attack()
    {
        if(attackTimer >= attackCooldown)
        {
            attackTimer = 0f;
            isAttacking = true;
            animator.SetTrigger("Attack");
        }
    }
    //tells script that attack is done
    void FinishAttack()
    {
        isAttacking = false;
    }

    //Called by animation event
    //creates an arrow prefab that is shot from the shotpoint
    void shoot()
    {
        Vector2 offset_pos = (Vector2)shotpoint.position + offset;
        GameObject newarrow = Instantiate(arrow, offset_pos, shotpoint.rotation);
    }


}
