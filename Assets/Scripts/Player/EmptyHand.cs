using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyHand : MonoBehaviour
{

    public float range = 10f;

    public string[] canPickupTag;

    public Camera fpsCam;

    private Transform parent;


    void Start()
    {
        parent = transform.parent;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        bool canPickUp = false;
        bool isShop = false;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {

            foreach (string tag in canPickupTag)
            {
                // a |= b means a = a||b
                canPickUp |= hit.transform.tag.Equals(tag);
            }

            if (hit.transform.tag.Equals("ShopButton"))
            {


            }

            // If can pickup and is active
            if (canPickUp && gameObject.activeSelf)
            {
                // Pick object is the object clicked
                GameObject pickObject = hit.transform.gameObject;
                
                // Checks for gluescript
                if (pickObject.GetComponent<GlueScript>() != null && pickObject.GetComponent<FixedJoint>() != null)
                {
                    pickObject.GetComponent<GlueScript>().DetachConnectedGameObjects();
                }

                // clones the object outside of the inventory
                GameObject hitter = Instantiate(pickObject, parent);
                if(hitter.tag == "Weapon")
                {
                    // weapon script handles transform
                    // pick up doesnt check every frame, not good enough for animations

                }
                else
                {
                    hitter.transform.position = parent.position;
                    hitter.transform.rotation = parent.rotation;
                }
                

                if (hitter.GetComponent<BlockSaveManager>() != null)
                {
                    hitter.GetComponent<BlockSaveManager>().isInventoryObJect = true;
                }

                // Checks for gluescript
                if (hitter.GetComponent<GlueScript>() != null)
                {
                    hitter.GetComponent<GlueScript>().DestroyFixedJoints();
                    Destroy(hitter.GetComponent<GlueScript>());
                }
                
                hitter.SetActive(false);

                Destroy(hit.transform.gameObject);
            }

        }
    }

}

