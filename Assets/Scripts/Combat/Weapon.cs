using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public float range;
    public float damage;
    public float knockback;
    float countUp;
    public float attackInterval;

    public string[] canAttackTag;

    Camera fpsCam;

    private Transform parent;


    void Start()
    {
        parent = transform.parent;
        fpsCam = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
        countUp += Time.deltaTime;
        if  (countUp < attackInterval)
        {
            Debug.Log("reloading");

        }
        // checks for mouse input, then if reloading is done, then if in inventory
        if (Input.GetButtonDown("Fire1") && countUp >= attackInterval && transform.parent != null && transform.parent.gameObject.tag == "Inventory")
        {
            Shoot(damage);
            countUp = 0;

        }
       
    }

    void Shoot(float damage)
    {
        RaycastHit hit;
        bool canAttack = false;
        
        if (Physics.Raycast(fpsCam.transform.position , fpsCam.transform.forward, out hit, range))
        {
            // sets can attack based on tags
            foreach (string tag in canAttackTag)
            {
                // a |= b means a = a||b
                canAttack |= hit.transform.tag.Equals(tag);
            }

            // If can attack and is active
            if (canAttack && gameObject.activeSelf)
            {
                // hit object is the object clicked
                GameObject hitObject = hit.transform.gameObject;
                if (hitObject.GetComponent<Health>() != null)
                {
                    Health otherHealth = hitObject.GetComponent<Health>();
                    otherHealth.TakeDamage(damage, knockback, Camera.main.transform.forward);
                }
 
            }

        }
    }

}
