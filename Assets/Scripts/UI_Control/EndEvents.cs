using UnityEngine;

public class EndEvents : MonoBehaviour
{
    private BoxCollider2D box;
    public int PassOver = 3;
    //Using Collision to detect when  enemies get passed
    void Awake()
    {
        box = GetComponent<BoxCollider2D>();
    }
    //When an enemy gets passed, The PassOver counter goes down
    //Entities are also cleaned up and deleted since you do not need them anymore
    void FindClosestEnemy()
    {
        Collider2D[] enemies = Physics2D.OverlapBoxAll(box.bounds.center, box.bounds.size, 0f);

        foreach(Collider2D enemy in enemies)
        {
            if(!enemy.CompareTag("Enemy")){continue;}
            PassOver--;
            Destroy(enemy.gameObject);
            Debug.Log("PassOver decremented. Current value: " + PassOver);
        }

    }

    //Checks for enemies every frame
    void Update()
    {
        FindClosestEnemy();
    }
}
