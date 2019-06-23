using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Health : MonoBehaviour
{
    float countUp;
    float stun = Mathf.Infinity;
    public float health;

    public bool isStunned;


    Rigidbody rb;
    NavMeshAgent navMeshAgent;
    private void Start()
    {
        if(GetComponent<Rigidbody>() != null)
        {
            rb = GetComponent<Rigidbody>();

        }

        if(GetComponent<NavMeshAgent>() != null)
        {
            navMeshAgent = GetComponent<NavMeshAgent>();

        }
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            Die();
        }
        /*
        // logic that plays on hit
        if(isStunned)
        {
            countUp += Time.deltaTime;

            if (countUp <= stun)
            {
                // for the stun duration
                

                if (navMeshAgent != null)
                {
                    navMeshAgent.enabled = false;
                }

                if (rb != null)
                {
                    rb.isKinematic = false;
                    //rb.AddForce(Vector3.forward * 5);
                }
            }
            else
            {
                // End of stun, reset components
                if (rb != null)
                {
                    rb.isKinematic = true;
                }

                if (navMeshAgent != null)
                {
                    navMeshAgent.enabled = true;
                }

                isStunned = false;
                countUp = 0;
            }


        }
        Debug.Log(isStunned);
        */

    }

    public void TakeDamage(float damage,float _stun, float knockback,Vector3 knockbackDir)
    {
        //knockback and animation should play
        
        health -= damage;

        //stun = _stun;
        //isStunned = true;
        //KnockBackAfterFrame(knockbackDir, knockback);
            //rb.AddForce(knockbackDir * knockback);
        




    }

     public void Die()
    {
        // death animation should play here
        Destroy(gameObject);
    }

    IEnumerator KnockBackAfterFrame(Vector3 _knockbackDir, float _knockback)
    {
        yield return new WaitForEndOfFrame();
        rb.AddForce(_knockbackDir * _knockback);
    }
    
}
