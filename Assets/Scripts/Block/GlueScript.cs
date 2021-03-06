﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlueScript : MonoBehaviour
{
    //private float Seconds = 0.05f;
    private float breakForce = 150f;
    private FixedJoint fj;

    private List <GameObject> previousHits = new List<GameObject>();

    public float fixedBodyCount;
    public bool justLoaded;

    void Update()
    {
        foreach(FixedJoint _fixedJoint in gameObject.GetComponents<FixedJoint>())
        {
            if(_fixedJoint.connectedBody == null)
            {
                Destroy(_fixedJoint);
            }
        }

        if (justLoaded)
        {
            if(fixedBodyCount == gameObject.GetComponents<FixedJoint>().Length)
            {
                gameObject.GetComponent<BlockSaveManager>().LoadPhysics();
                justLoaded = false;
            }
        }
        
    }

    void OnCollisionEnter(Collision collision)
    {
        bool canGlue = true;

        foreach(GameObject hit in previousHits)
        {
            if(hit == collision.gameObject)
                canGlue = false;
            else      
                canGlue = true;
        }
        // if it has a rigid body and is on a tower
        if ( canGlue == true && collision.rigidbody != null && collision.gameObject.tag == "Tower" || collision.gameObject.tag == "Foundation")
        {
            //StartCoroutine(Glue(Seconds, collision));
            gameObject.tag = "Tower";
            fj = gameObject.AddComponent<FixedJoint>();
            fj.connectedBody = collision.rigidbody;
            fj.breakForce = breakForce;
            previousHits.Add(collision.gameObject);
            
            

        }
        // no leftover gluescripts 4 me
        
        else if (collision.gameObject.GetComponent<GlueScript>() == null)
        {
            DestroyFixedJoints();
            Destroy(this);

        }
    }

    IEnumerator Glue(float Seconds, Collision collision)
    {
        yield return new WaitForSeconds(Seconds);
        if (collision.rigidbody != this.gameObject.GetComponent<Rigidbody>())
        {
            
        }
        
    }

   
    

    public void DestroyFixedJoints()
    {
        foreach (FixedJoint _fj in gameObject.GetComponents<FixedJoint>())
        {
            Destroy(_fj);
        }

    }

    public void DetachConnectedGameObjects()
    {
        foreach (FixedJoint _fj in gameObject.GetComponents<FixedJoint>())
        {
            if (_fj.connectedBody != null)
            {
                if (_fj.connectedBody.gameObject.GetComponent<GlueScript>() != null)
                {
                    //Debug.Log(_fj.connectedBody.name);
                    _fj.connectedBody.gameObject.GetComponent<GlueScript>().DetachGameobject(gameObject);
                }
            }
           // Destroy(_fj);
        }

    }

    public void DetachGameobject(GameObject _detachedGameObject)
    {
        foreach (FixedJoint _fixedJoint in gameObject.GetComponents<FixedJoint>())
        {
            if(_fixedJoint.connectedBody == _detachedGameObject.GetComponent<Rigidbody>())
            {
                //Debug.Log("Destroy");
                Destroy(_fixedJoint);
            }
        }
    }
}



