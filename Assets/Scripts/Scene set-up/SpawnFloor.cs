using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFloor : MonoBehaviour
{
    // Spawns tiles from center of spawner in a square
    GameObject ObjectInstance;

    float Scale;

    public float XposOffset;
    public float Ypos = -2.5f;
    public float ZposOffset;

    public float tileSize;
    public int gridDimensions = 5;

    public int [] tileWeights;
    int totalWeight = 0;
    public GameObject[] Tiles;

    // Start is called before the first frame update
    void Start()
    {
        CreateInstances(Tiles, tileWeights, gridDimensions);
    }

    void CreateInstances(GameObject[] Tiles, int[] tileWeights, int gridDimensions)
    {
            
            foreach (int Weight in tileWeights)
            {
            totalWeight += Weight;
            }

            

            // nested for loops to loop square lines
            for (int j = 0; j < gridDimensions; j++)
            {

                for (int i = 0; i < gridDimensions; i++)
                {

                // selects tile index based on weighted values
                int randNum = Random.Range(0, totalWeight);
                //Debug.Log("randNum: " + randNum);
                int tileIndex = 0;
                int tempWeight = 0;

                for (int k = 0; k < tileWeights.Length ; k++)
                    {
                    tempWeight += tileWeights[k];
                    //Debug.Log("TempWeight: " + tempWeight);

                    if (randNum < tempWeight)
                        {
                            tileIndex = k;
                            break;
                        }
                        
                    }

                // Sets tile pos based on pos in grid, then offsets
                float Xpos;
                float Zpos;

                if (gridDimensions % 2 == 1)
                    {
                     Xpos = tileSize * (i - (gridDimensions - 1) / 2) + XposOffset;
                     Zpos = tileSize * (j - (gridDimensions - 1) / 2) + ZposOffset;
                }
                else
                {
                     Xpos = tileSize * (i - (gridDimensions + 1f) / 2 + 1) + XposOffset;
                     Zpos = tileSize * (j - (gridDimensions + 1f) / 2 + 1) + ZposOffset;
                }


                // Creates tile based on position in square
                ObjectInstance = Instantiate(Tiles[tileIndex], new Vector3(Xpos, Ypos, Zpos), Quaternion.identity, gameObject.transform);
                    ObjectInstance.transform.localScale = Vector3.one * tileSize;
                    ObjectInstance.name += i.ToString() + ", " + j.ToString();


                }
            }
        }



    
}
