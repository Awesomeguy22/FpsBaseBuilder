using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationScript_Oliver : MonoBehaviour
{

    Rigidbody rb;
    public float rotateSpeed = 5f;

    private bool canDrop = true;
    //Array storing bools x = 0 y = 1 z = 2
    private BitArray Trinary = new BitArray(3);

    // 1 or 0 if rotating on that axis
    int x;
    int y;
    int z;

    Transform Trans;

    GameObject XAxis;
    GameObject YAxis;
    GameObject ZAxis;

    GameObject AxisHolder;

    GameObject axisInstance;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;

        

        AxisHolder = GameObject.FindWithTag("AxisHolder");
        

        Trans = GetComponent<Transform>();

        // loads prefabs from the special resources folder
        // should Just create primitives soon
        XAxis = Resources.Load("XAxis") as GameObject;
        YAxis = Resources.Load("YAxis") as GameObject;
        ZAxis = Resources.Load("ZAxis") as GameObject;
        //axisInstance = Resources.Load("axisInstance") as GameObject;


        //Creates first axis on y       
        Trinary[1] = true;
        AxisCreate();
        
    }

    // Update is called once per frame
    void Update()
    {

        // converts bools to ints
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
        }

        //Rotates left and right
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.transform.Rotate(new Vector3(x, y, z) * rotateSpeed);
            
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.transform.Rotate(new Vector3(x, y, z) * -rotateSpeed);
        }

        //Changes axis of rotation
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            
            AxisChange();
        }

        //drops currently selected block on e press
        if (Input.GetKeyDown("e") && canDrop)
        {
            foreach (Collider collider in gameObject.GetComponents<Collider>())
            {
                collider.isTrigger = false;
            }

            gameObject.transform.SetParent(null);
            gameObject.AddComponent<GlueScript>();
            rb.useGravity = true;

            Destroy(this);
        }
    }

    public void OnDestroy()
    {
        Destroy(GameObject.FindWithTag("Axis"));
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
        Destroy(GameObject.FindWithTag("Axis"));
        // Creates axis based on prefab scale, and object scale times 1.5 for one axis
        if (Trinary[0])
        {

            axisInstance = Instantiate(XAxis, transform.position, transform.rotation, AxisHolder.transform);
            axisInstance.transform.localScale = new Vector3(Trans.localScale.x * 1.5f, XAxis.transform.localScale.y, XAxis.transform.localScale.z);
            

        }

        if (Trinary[1])
        {
            axisInstance = Instantiate(YAxis, transform.position, transform.rotation, AxisHolder.transform);
            axisInstance.transform.localScale = new Vector3(YAxis.transform.localScale.x, Trans.localScale.x * 1.5f , YAxis.transform.localScale.z);
            
        }

        if (Trinary[2])
        {

            axisInstance = Instantiate(ZAxis, transform.position, transform.rotation, AxisHolder.transform);
            axisInstance.transform.localScale = new Vector3(ZAxis.transform.localScale.x, ZAxis.transform.localScale.y, Trans.localScale.x * 1.5f);
            
        }
    }

       private void OnTriggerEnter(Collider other)
    {
        canDrop = false;
        
    }

    void OnTriggerExit(Collider other)
    {
        canDrop = true;
        
    }
}




