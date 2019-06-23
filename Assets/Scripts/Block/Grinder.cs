using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Grinder : MonoBehaviour
{
 //   public Text highScoreText;
    private BlockPoints playerBlockPoints;
    // Start is called before the first frame update
    void Start()
    {
        playerBlockPoints = GameObject.FindGameObjectWithTag("Player").GetComponent<BlockPoints>();
    }

    // Update is called once per frames
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        bool canGrind = (other.gameObject.tag == "Block");
        if (other.transform.parent != null && other.transform.parent.gameObject.tag == "Inventory")
            canGrind = false;

       if (canGrind)
       {
           playerBlockPoints.blockPoints += Mathf.Round(other.gameObject.transform.localScale.x * other.gameObject.transform.localScale.y * other.gameObject.transform.localScale.z);
           Destroy(other.gameObject);
       }

    }

 
}
