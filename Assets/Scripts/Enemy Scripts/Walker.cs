using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Walker : MonoBehaviour
{
    public NavMeshAgent enemyNavMeshAgent;
    public float detectRange;


     public string[] findTags;

    // Start is called before the first frame update
    void Start()
    {
        enemyNavMeshAgent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
         if(enemyNavMeshAgent.enabled == true)
        {
            GameObject target = FindNearestTaggedObject(findTags, detectRange);
            if (target != null)
            {
                enemyNavMeshAgent.isStopped = false;
                enemyNavMeshAgent.SetDestination(target.transform.position);
            }
            else
            {
                enemyNavMeshAgent.isStopped = true;
            }
        }
        
        
        
    }


 
    public GameObject FindNearestTaggedObject(string[] tags, float detectRange)
    {

        GameObject closestGameObject = null;

        foreach (string element in tags)
        {
            GameObject[] gameObjects;
            gameObjects = GameObject.FindGameObjectsWithTag(element);
            float distance = detectRange;
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
