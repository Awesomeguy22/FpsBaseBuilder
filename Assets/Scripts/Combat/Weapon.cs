using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public float range;
    public float damage;
    public float stun;
    public float knockback;

    float countUp;
    public float attackInterval;
    public bool canAttack;

    public string[] canAttackTag;

    public Vector3 canAttackPos;
    public Vector3 cantAttackPos;
    public Vector3 canAttackdir;
    public Vector3 cantAttackdir;


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
            //Debug.Log("reloading");

        }
        // checks if in inventory , if not held should be inactive
        if (transform.parent != null && transform.parent.gameObject.tag == "Inventory")
        {
            transform.localPosition = canAttackPos;
            if (canAttack)
            {
                transform.localPosition = canAttackPos;
                transform.localRotation = Quaternion.Euler(canAttackdir);

            }
            else
            {
                transform.localPosition = cantAttackPos;
                transform.localRotation = Quaternion.Euler(cantAttackdir);
            }


            // checks if done reloading
            if (countUp >= attackInterval)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    Shoot(damage);
                    countUp = 0;
                }

                canAttack = true;

            }
            else
            {
                canAttack = false;
            }
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
                    otherHealth.TakeDamage(damage, stun, knockback, Camera.main.transform.forward);
                }
 
            }

        }
    }

}
