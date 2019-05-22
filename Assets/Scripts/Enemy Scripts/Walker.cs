using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Walker : MonoBehaviour
{
    public NavMeshAgent enemyNavMeshAgent;

    public string[] findTags;

    // Start is called before the first frame update
    void Start()
    {
        enemyNavMeshAgent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        GameObject target = FindNearestTaggedObject(findTags);
        enemyNavMeshAgent.SetDestination(target.transform.position);

        if (Vector3.Distance(target.transform.position, transform.position) < 2) {
            enemyNavMeshAgent.isStopped = true;
        }
        else
        {
            enemyNavMeshAgent.isStopped = false;
        }

    }



    public GameObject FindNearestTaggedObject(string[] tags)
    {

        GameObject closestGameObject = null;

        foreach (string element in tags)
        {
            GameObject[] gameObjects;
            gameObjects = GameObject.FindGameObjectsWithTag(element);
            float distance = Mathf.Infinity;
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
