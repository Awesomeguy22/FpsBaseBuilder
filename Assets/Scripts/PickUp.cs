using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{

    public float damage = 10f;
    public float range = 10f;

    public string[] canPickupTag;

    public Camera fpsCam;
    public GameObject pickerUpper;

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

        //checks for Hit?
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            //Debug.Log(hit.transform.name);

            foreach (string _tag in canPickupTag)
            {
                canPickUp |= hit.transform.tag.Equals(_tag);
            }

            if (canPickUp && pickerUpper.activeSelf)
            {
                // Pick object is the object clicked
                GameObject pickObject = hit.transform.gameObject;
                
                // Checks for gluescript
                if (pickObject.GetComponent<GlueScript>() != null && pickObject.GetComponent<FixedJoint>() != null)
                {
                    pickObject.GetComponent<GlueScript>().DetachConnectedGameObjects();
                }

                // clones the object outside of the inventory
                GameObject hitter = Instantiate(pickObject, transform);
                hitter.transform.position = transform.position;
                hitter.transform.rotation = transform.rotation;

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

