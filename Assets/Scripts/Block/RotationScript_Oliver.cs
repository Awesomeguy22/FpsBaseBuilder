using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationScript_Oliver : MonoBehaviour
{

    Rigidbody rb;
    float rotateSpeed = 5f;
    float pushForce = 100f;
    private bool canDrop = true;

    string[] canRotateTag = { "Block" };
    bool canRotate = false;
    //Array storing bools x = 0 y = 1 z = 2
    private BitArray Trinary = new BitArray(3);

    // 1 or 0 if rotating on that axis
    int x;
    int y;
    int z;
    
    GameObject AxisHolder;

    GameObject axisInstance;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;

        AxisHolder = GameObject.FindWithTag("AxisHolder");
        foreach (string tag in canRotateTag)
        {
            // a |= b means a = a||b
            canRotate |= transform.tag.Equals(tag);
        }


        //Creates first axis on y
        if (canRotate)
        {
            Trinary[1] = true;

            AxisCreate();
        }
            
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canRotate)
        {
            if (Trinary[0])
            {
                x = 1;

            }
            else
                x = 0;

            if (Trinary[1])
            {
                y = 1;
            }
            else
                y = 0;

            if (Trinary[2])
            {
                z = 1;
            }
            else
                z = 0;


            //Rotates left and right
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Rotate(new Vector3(x, y, z) * rotateSpeed);

            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Rotate(new Vector3(x, y, z) * -rotateSpeed);
            }

            //Changes axis of rotation
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {

                AxisChange();
            }
        }
        //throws currently selected block on e press
        if (Input.GetKeyDown("e") && canDrop)
        {
            foreach (Collider collider in gameObject.GetComponents<Collider>())
            {
                collider.isTrigger = false;
            }
            gameObject.GetComponent<BlockSaveManager>().isInventoryObJect = false;
            Vector3 forceDirection = transform.parent.transform.forward;
            gameObject.transform.SetParent(null);
            gameObject.AddComponent<GlueScript>();
            rb.useGravity = true;
            rb.AddForce(forceDirection * pushForce);

            Destroy(this);
        }
    }

    public void OnDestroy()
    {
        if (canRotate)
        {
            axisInstance.SetActive(false);

        }
    }

    void AxisChange()
    {
        

        if (Trinary[0])
        {
            Trinary[0] = false;
            Trinary[1] = true;

            AxisCreate();
        }
        else if (Trinary[1])
        {
            Trinary[1] = false;
            Trinary[2] = true;

            AxisCreate();
        }
        else if (Trinary[2])
        {
            Trinary[0] = true;
            Trinary[2] = false;

            AxisCreate();
        }
    }

    void AxisCreate()
    {
        foreach(Transform axis in AxisHolder.transform)
        {
            axis.gameObject.SetActive(false);
        }

        // Creates axis based on prefab scale, and object scale times 1.5 for one axis
        if (Trinary[0])
        {

            //axisInstance = Instantiate(XAxis, transform.position, transform.rotation, AxisHolder.transform);
            axisInstance = AxisHolder.transform.GetChild(0).gameObject;
            axisInstance.SetActive(true);
            axisInstance.transform.rotation = transform.rotation;
            axisInstance.transform.localScale = new Vector3(transform.localScale.x * 1.5f, axisInstance.transform.localScale.y, axisInstance.transform.localScale.z);
            

        }

        if (Trinary[1])
        {
            //axisInstance = Instantiate(YAxis, transform.position, transform.rotation, AxisHolder.transform);
            axisInstance = AxisHolder.transform.GetChild(1).gameObject;
            axisInstance.SetActive(true);
            axisInstance.transform.rotation = transform.rotation;
            axisInstance.transform.localScale = new Vector3(axisInstance.transform.localScale.x, transform.localScale.x * 1.5f , axisInstance.transform.localScale.z);
        }

        if (Trinary[2])
        {
            //axisInstance = Instantiate(ZAxis, transform.position, transform.rotation, AxisHolder.transform);
            axisInstance = AxisHolder.transform.GetChild(2).gameObject;
            axisInstance.SetActive(true);
            axisInstance.transform.rotation = transform.rotation;
            axisInstance.transform.localScale = new Vector3(axisInstance.transform.localScale.x, axisInstance.transform.localScale.y, transform.localScale.z * 1.5f);
        }
    }

       private void OnTriggerEnter(Collider other)
    {
        canDrop = false;
        //Debug.Log("Cannot drop");
        
    }

    void OnTriggerExit(Collider other)
    {
        canDrop = true;
        //Debug.Log("Can drop");

    }
}




