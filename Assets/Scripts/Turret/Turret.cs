using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    public string[] attackTags;
    public float range;
    public float height;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3 (transform.position.x, height, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        GameObject targetObject = FindNearestTaggedObject(attackTags, range);
        if (targetObject != null)
        {
            Debug.Log("target found");
            transform.GetChild(0).LookAt(targetObject.transform, transform.up);
        }
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
