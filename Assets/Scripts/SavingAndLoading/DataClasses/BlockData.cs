using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlockData{

    public float[] position;
    public float[] rotation;
    public float[] scale;

    public float[] rigidbodyVelocity;
    public float[] rigidbodyAngularVelocity;

    public string name;
    public string tag;
    public string prefabName;
    public string parentName;

    public bool hasGlueScript;
    public float fixedBodyCount;

    public BlockData(BlockSaveManager _blockSaveManager)
    {
        position = new float[3];
        position[0] = _blockSaveManager.objectPosition.x;
        position[1] = _blockSaveManager.objectPosition.y;
        position[2] = _blockSaveManager.objectPosition.z;

        rotation = new float[3];
        rotation[0] = _blockSaveManager.objectRotation.x;
        rotation[1] = _blockSaveManager.objectRotation.y;
        rotation[2] = _blockSaveManager.objectRotation.z;

        scale = new float[3];
        scale[0] = _blockSaveManager.objectScale.x;
        scale[1] = _blockSaveManager.objectScale.y;
        scale[2] = _blockSaveManager.objectScale.z;

        rigidbodyVelocity = new float[3];
        rigidbodyVelocity[0] = _blockSaveManager.rbVelocity.x;
        rigidbodyVelocity[1] = _blockSaveManager.rbVelocity.y;
        rigidbodyVelocity[2] = _blockSaveManager.rbVelocity.z;

        rigidbodyAngularVelocity = new float[3];
        rigidbodyAngularVelocity[0] = _blockSaveManager.rbAngularVelocity.x;
        rigidbodyAngularVelocity[1] = _blockSaveManager.rbAngularVelocity.y;
        rigidbodyAngularVelocity[2] = _blockSaveManager.rbAngularVelocity.z;

        //Debug.Log(position[0] + "" + position[1] + "" + position[2]);
        name = _blockSaveManager.name;
        tag = _blockSaveManager.tag;
        prefabName = _blockSaveManager.prefabName;
        parentName = _blockSaveManager.parentName;

        hasGlueScript = _blockSaveManager.hasGlueScript;
        fixedBodyCount = _blockSaveManager.fixedBodyCount;
    }
}
