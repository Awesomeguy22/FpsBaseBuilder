using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData{
    public float[] position;
    public float[] rotation;

    public float[] rigidbodyVelocity;
    public float[] rigidbodyAngularVelocity;

    public string name;

    public PlayerData(PlayerSaveManager playerSaveManager)
    {
        position = new float[3];
        position[0] = playerSaveManager.objectPosition.x;
        position[1] = playerSaveManager.objectPosition.y;
        position[2] = playerSaveManager.objectPosition.z;

        rotation = new float[3];
        rotation[0] = playerSaveManager.objectRotation.x;
        rotation[1] = playerSaveManager.objectRotation.y;
        rotation[2] = playerSaveManager.objectRotation.z;

        rigidbodyVelocity = new float[3];
        rigidbodyVelocity[0] = playerSaveManager.rbVelocity.x;
        rigidbodyVelocity[1] = playerSaveManager.rbVelocity.y;
        rigidbodyVelocity[2] = playerSaveManager.rbVelocity.z;

        rigidbodyAngularVelocity = new float[3];
        rigidbodyAngularVelocity[0] = playerSaveManager.rbAngularVelocity.x;
        rigidbodyAngularVelocity[1] = playerSaveManager.rbAngularVelocity.y;
        rigidbodyAngularVelocity[2] = playerSaveManager.rbAngularVelocity.z;

        Debug.Log(position[0] +""+ position[1] +""+ position[2]);
        name = playerSaveManager.name;
    }
}
