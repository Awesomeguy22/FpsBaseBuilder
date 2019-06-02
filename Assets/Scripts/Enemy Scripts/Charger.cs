using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Charger : MonoBehaviour
{

    // IMPORTANT: the head must be the first child in the hierarchy
    public NavMeshAgent enemyNavMeshAgent;

    public string[] findTags;

    public float chargeInterval = 5f;
    public float chargeLength = 5f;
    float counter;

    GameObject target;

    public Material charging;
    public Material notCharging;

    MeshRenderer meshRenderer;
    GameObject head;
    // Start is called before the first frame update
    void Start()
    {
        enemyNavMeshAgent = GetComponent<NavMeshAgent>();
        head = gameObject.transform.GetChild(0).gameObject;
        meshRenderer = head.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        GameObject target = FindNearestTaggedObject(findTags);

        //Debug.Log("Time: " + counter.ToString());


        counter += Time.deltaTime;
        if (counter > chargeInterval)
        {
            // Charging
            enemyNavMeshAgent.isStopped = false;
            enemyNavMeshAgent.SetDestination(target.transform.position);
            meshRenderer.material = charging;

            if (counter > chargeInterval + chargeLength)
            {
                //Stops charging
                counter = 0f;
                enemyNavMeshAgent.isStopped = true;
                //Debug.Log("Stopped Charging");

            }
        }
        else
        {
            // not charging
            Vector3 XZCoords = new Vector3 (target.transform.position.x, gameObject.transform.position.y, target.transform.position.z);
            gameObject.transform.LookAt(XZCoords);

            meshRenderer.material = notCharging;
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
