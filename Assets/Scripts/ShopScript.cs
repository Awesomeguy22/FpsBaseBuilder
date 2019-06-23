using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScript : MonoBehaviour
{
    BlockPoints playerBlockPoints;

    
    public GameObject[] buyableObjects;
    public float[] prices;

    // Start is called before the first frame update
    void Start()
    {
        playerBlockPoints = GameObject.FindGameObjectWithTag("Player").GetComponent<BlockPoints>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    


}