using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health;
    Rigidbody rigidbody;
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(float damage, float knockback,Vector3 knockbackDir)
    {
        //knockback and animation should play
        
        health -= damage;
        rigidbody.AddForce(knockbackDir * knockback);
        
    }
   
    public void Die()
    {
        // death animation should play here
        Destroy(gameObject);
    }

}
