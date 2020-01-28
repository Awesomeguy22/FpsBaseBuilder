using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeriodicSpawner : MonoBehaviour
{

    public float minSize;
    public float maxSize;

    public float spawnDimensions;

    [Header(" Have the same amount of elements for each.")]
    public GameObject[] spawningObjects;
    public float[] spawnPeriods;
    public float[] countUps;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        int i = 0;
        foreach (GameObject spawningObject in spawningObjects)
        {
            countUps[i] += Time.deltaTime;
            if(countUps[i] > spawnPeriods[i])
            {
                float randX = Random.Range(-spawnDimensions, spawnDimensions);
                float randZ = Random.Range(-spawnDimensions, spawnDimensions);

                Instantiate(spawningObjects[i], new Vector3(randX + transform.position.x, transform.position.y, randZ + transform.position.z), Quaternion.identity, transform);
                countUps[i] = 0;
            }

            i++;
        }



    }


}
