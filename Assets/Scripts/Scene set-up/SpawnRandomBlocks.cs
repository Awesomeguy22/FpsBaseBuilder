using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomBlocks : MonoBehaviour
{
    
    GameObject ObjectInstance;
    public float spawnDimensions;

    float Scale;

    float Xpos;
    public float Ypos = 5f;
    float Zpos;

    
    public float minSize = 0.1f;
    public float maxSize = 1f;

    public int[] objectNums;
    public GameObject[] PossibleObjects;

    // Start is called before the first frame update
    void Start()
    {  
        CreateInstances(PossibleObjects, objectNums);
    }

    void CreateInstances(GameObject[] PossibleObjects, int[] objectNums)
    {

        //makes sure all values are set properly
        if (objectNums.Length == PossibleObjects.Length)
        {
            
            // nested for loops to Create all objects in possible objects with respective numbers in objectnum
            for (int j = 0; j < PossibleObjects.Length; j++)
            {
                
                for (int i = 0; i < objectNums[j]; i++)
                {
                    
                    int ObjectsIndex = j;

                    ObjectInstance = Instantiate(PossibleObjects[ObjectsIndex], Vector3.zero, Quaternion.identity, gameObject.transform);

                    //sets random scale
                    Scale = Random.Range(minSize, maxSize);

                    // sets random position
                    Xpos = Random.Range(-spawnDimensions, spawnDimensions);
                    Zpos = Random.Range(-spawnDimensions, spawnDimensions);

                    ObjectInstance.transform.localScale = new Vector3(Scale, Scale, Scale);
                    ObjectInstance.transform.position = new Vector3(Xpos, Ypos, Zpos);

                    ObjectInstance.name += i.ToString();


                }
            }
        }
        else Debug.Log("Objectnumbers must have corresponding objects");

        
        
    }
}
