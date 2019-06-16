using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
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

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {

            foreach (string tag in canPickupTag)
            {
                // a |= b means a = a||b
                canPickUp |= hit.transform.tag.Equals(tag);
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
                    hitter.transform.position = parent.position + new Vector3(1.2f, -0.8f, -0.8f);
                    //new Quaternion() quaternion = Quaternion.Euler(-115, 11, -125);
                    hitter.transform.localRotation = Quaternion.Euler(new Vector3(-115, 11, -125));
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

