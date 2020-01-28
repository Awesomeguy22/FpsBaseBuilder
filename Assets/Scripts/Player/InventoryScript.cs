using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InventoryScript : MonoBehaviour
{

    //empty gameobject child of inventory
    public GameObject pickerUpper;

    int activeItemIndex;
    int previousActiveItemIndex;
    public string[] canDropTag;
    
    private GameObject activeGB;

    // Start is called before the first frame update
    void Start()
    {
        SelectItem();
    }

    // Update is called once per frame
    void Update()
    {
        previousActiveItemIndex = activeItemIndex;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (activeItemIndex >= transform.childCount - 1)
                activeItemIndex = 0;
            else
                activeItemIndex++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (activeItemIndex <= 0)
                activeItemIndex = transform.childCount - 1;
            else
                activeItemIndex--;
        }

        if ( Input.GetButtonDown("Fire2"))
        {
            
                activeItemIndex = 0;
        }




        if (previousActiveItemIndex != activeItemIndex || transform.childCount == 1)
        {
            SelectItem();
        }

        if (activeItemIndex > transform.childCount)
        {
            activeItemIndex = 0;
            pickerUpper.SetActive(true);
        }

        if (activeGB != null)
        {
            if (activeGB.transform.parent == null)
            {
                activeItemIndex = 0;
                pickerUpper.SetActive(true);
            }
        }
    }

    public void SelectItem()
    {
        int i = 0;
        bool canDrop = false;
        foreach ( Transform item in transform)
        {
            //so you can't drop towers after picking up
            if (item.tag == "Tower")
                item.tag = "Block";
            

            // if current item index is the one being held
            if (i == activeItemIndex)
            {
                item.gameObject.SetActive(true);
                activeGB = item.gameObject;
                foreach (string _tag in canDropTag)
                {
                    if (_tag.Equals(item.gameObject.tag))
                    {
                        canDrop = true;
                        
                    }
                }

                // If the held object is a block
                if (canDrop)
                {
                    item.gameObject.AddComponent<RotationScript_Oliver>();

                    // for when the object has multiple colliders
                    foreach (Collider collider in item.GetComponents<Collider>())
                        collider.isTrigger = true;

                    
                }
            }
            //When not being held
            else
            {
                
                Destroy(item.gameObject.GetComponent<RotationScript_Oliver>());
                item.gameObject.SetActive(false);
            }

            i++;
        }
    }
}

