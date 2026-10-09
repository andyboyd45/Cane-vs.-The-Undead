using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow : MonoBehaviour
{
    [Header("Arrow Settings")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private int damage = 25;

    private bool madeContact = false;
    private float lifetime = 3f;

    // Update is called once per frame
    //Arrow is constantly moving to the right unless contact is made
    //Destroys the arrow after its lifetime expires
    void Update()
    {
        if (!madeContact)
        {
            movement();
        }
        
        lifetime -= Time.deltaTime;
        if (lifetime <= 0f)
        {
            Destroy(gameObject);
        }
    }
    //Movement logic for arrow
    void movement()
    {
        transform.Translate(1 * speed * Time.deltaTime,0,0);
    }
    //Handles collision with enemy and deals damage
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Enemy"))
        {
            madeContact = true;
            if (coll.TryGetComponent<Health>(out Health health))
            {
                health.TakeDamage(damage);
                Debug.Log("Arrow hit"+  coll.name +" for " + damage + " damage.");
            }
            Destroy(gameObject);
        }
    }
    //Handles when the arrow exits collision with a random object (resets contact status)
    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.CompareTag("Enemy"))
        {
            madeContact = false;
        }
    }
}
