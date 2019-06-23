using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    public float damage;
    public float stun;
    public float knockback;
    public float range;

    float countUp;
    public float attackInterval;
    public bool canAttack;

    public string[] canAttackTag;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        countUp += Time.deltaTime;

        if (countUp >= attackInterval)
        {
            canAttack = true;
            Debug.Log("Can Attack");
        }
        else
        {
            canAttack = false;
        }


        //Attacking object needs a health script

        GameObject attackingObject = FindNearestTaggedObject(canAttackTag, range);
        if (canAttack && attackingObject != null )
        {
            Attack(attackingObject);

            countUp = 0;
        }

    }

    void Attack(GameObject attackingObject)
    {
        attackingObject.GetComponent<Health>().TakeDamage(damage, stun, knockback, transform.forward);
    }


    public GameObject FindNearestTaggedObject(string[] tags, float range)
    {

        GameObject closestGameObject = null;

        foreach (string element in tags)
        {
            GameObject[] gameObjects;
            gameObjects = GameObject.FindGameObjectsWithTag(element);
            float distance = range;
            Vector3 position = transform.position;
            foreach (GameObject gameObject in gameObjects)
            {
                Vector3 diff = gameObject.transform.position - position;
                float currentDistance = diff.sqrMagnitude;
                if (currentDistance < distance)
                {
                    closestGameObject = gameObject;
                    distance = currentDistance;
                }
            }
        }

        return closestGameObject;
    }
}