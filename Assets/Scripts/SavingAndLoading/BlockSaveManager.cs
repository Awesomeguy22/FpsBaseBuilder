using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSaveManager : MonoBehaviour
{
    public Rigidbody blockRb;

    public Vector3 objectRotation;
    public Vector3 objectPosition;
    public Vector3 objectScale;

    public Vector3 rbVelocity;
    public Vector3 rbAngularVelocity;

    public string objectName;
    public string objectTag;
    public string prefabName;

    public string parentName;

    public bool hasGlueScript;
    public bool justLoaded;

    public float fixedBodyCount;

    void Start()
    {
        blockRb = gameObject.GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        if (justLoaded == false)
        {
            objectName = gameObject.name;
            if (transform.parent != null)
            {
                parentName = transform.parent.name;
            }
            else
            {
                parentName = null;
            }

            if (gameObject.GetComponent<GlueScript>() != null)
            {
                hasGlueScript = true;
            }
            else
            {
                hasGlueScript = false;
            }
            objectRotation = transform.rotation.eulerAngles;
            objectPosition = transform.position;
            objectScale = transform.localScale;

            rbVelocity = blockRb.velocity;
            rbAngularVelocity = blockRb.angularVelocity;

            fixedBodyCount = gameObject.GetComponents<FixedJoint>().Length;
        }

    }

    public void LoadData(BlockData _block)
    {

        objectName = _block.name;
        objectTag = _block.tag;
        parentName = _block.parentName;

        hasGlueScript = _block.hasGlueScript;

        Vector3 _position;

        _position.x = _block.position[0];
        _position.y = _block.position[1];
        _position.z = _block.position[2];

        //Debug.Log("load");
        //Debug.Log(_position);
        objectPosition = _position;

        Vector3 _rotation = new Vector3
        {
            x = _block.rotation[0],
            y = _block.rotation[1],
            z = _block.rotation[2]
        };

        // Debug.Log(_rotation);
        objectRotation = _rotation;

        Vector3 _scale = new Vector3 { 
            x = _block.scale[0],
            y = _block.scale[1],
            z = _block.scale[2]
        };

        objectScale = _scale;

        Vector3 _velocity;

        _velocity.x = _block.rigidbodyVelocity[0];
        _velocity.y = _block.rigidbodyVelocity[1];
        _velocity.z = _block.rigidbodyVelocity[2];

        rbVelocity = _velocity;

        Vector3 _angularVelocity;

        _angularVelocity.x = _block.rigidbodyAngularVelocity[0];
        _angularVelocity.y = _block.rigidbodyAngularVelocity[1];
        _angularVelocity.z = _block.rigidbodyAngularVelocity[2];

        rbAngularVelocity = _angularVelocity;

        fixedBodyCount = _block.fixedBodyCount;
    }
    public void LoadBlock()
    {

        gameObject.name = objectName;
        gameObject.tag = objectTag;

        if (parentName != null)
        {
            transform.SetParent(GameObject.Find(parentName).transform);
        }

        blockRb = gameObject.GetComponent<Rigidbody>();

        transform.position = objectPosition;

        if (transform.parent != null)
        {
            foreach (Collider _collider in gameObject.GetComponents<Collider>())
            {
                _collider.isTrigger = true;
            }
            gameObject.SetActive(false);
            blockRb.useGravity = false;
            transform.position = transform.parent.position;
        }
        // Debug.Log(_rotation);
        transform.rotation = Quaternion.Euler(objectRotation);
        transform.localScale = objectScale;

        if (hasGlueScript == true)
        {
            Debug.Log("GlueScriptAdded");
            GlueScript gluyScripty = gameObject.AddComponent<GlueScript>();
            gluyScripty.justLoaded = true;
            gluyScripty.fixedBodyCount = fixedBodyCount;
        }

    }

    public void LoadPhysics()
    {
        if (transform.parent == null)
        {
            //blockRb.isKinematic = false;
            blockRb.velocity = rbVelocity;

            blockRb.angularVelocity = rbAngularVelocity;
        }

        justLoaded = false;
    }
}
