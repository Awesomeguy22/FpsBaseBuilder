using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerSaveManager : MonoBehaviour
{
    public Rigidbody rb;

    public Vector3 objectRotation;
    public Vector3 objectPosition;

    public Vector3 rbVelocity;
    public Vector3 rbAngularVelocity;

    public string objectName;
    public string prefabLocation;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        objectName = gameObject.name;
        objectRotation = transform.rotation.eulerAngles;
//        Debug.Log(objectRotation);
        objectPosition = gameObject.transform.position;

        rbVelocity = rb.velocity;
        rbAngularVelocity = rb.angularVelocity;

    }

    public void SavePlayer()
    {
        Debug.Log("saveone");
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        gameObject.GetComponent<CharacterController>().enabled = false;
        PlayerData data = SaveSystem.LoadPlayer();

        gameObject.name = data.name;

        Vector3 _position;

        _position.x = data.position[0];
        _position.y = data.position[1];
        _position.z = data.position[2];

        //Debug.Log("load");
        //Debug.Log(_position);
        transform.position = _position;

        Vector3 _rotation = new Vector3
        {
            x = data.rotation[0],
            y = data.rotation[1],
            z = data.rotation[2]
        };

       // Debug.Log(_rotation);
        transform.rotation = Quaternion.Euler(_rotation);

        Vector3 _velocity;

        _velocity.x = data.rigidbodyVelocity[0];
        _velocity.y = data.rigidbodyVelocity[1];
        _velocity.z = data.rigidbodyVelocity[2];

        rb.velocity = _velocity;

        Vector3 _angularVelocity;

        _angularVelocity.x = data.rigidbodyAngularVelocity[0];
        _angularVelocity.y = data.rigidbodyAngularVelocity[1];
        _angularVelocity.z = data.rigidbodyAngularVelocity[2];

        rb.angularVelocity = _angularVelocity;
        gameObject.GetComponent<CharacterController>().enabled = true;
    }
}
