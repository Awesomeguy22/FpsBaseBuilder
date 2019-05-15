using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks_SaveManager : MonoBehaviour
{
    public string[] blockTags;

    public GameObject[] blockPrefabs;

    public BlockData[] blocksInScene;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveBlocks()
    {
        List<GameObject> objectsWithBlockTag = new List<GameObject>();

        foreach (string _blockTag in blockTags)
        {
            foreach (GameObject _blockWithTag in GameObject.FindGameObjectsWithTag(_blockTag))
            {
                objectsWithBlockTag.Add(_blockWithTag);
            }
        }
        blocksInScene = new BlockData[objectsWithBlockTag.Count];

        int i = 0;
        foreach (GameObject _block in objectsWithBlockTag)
        {
            Debug.Log(_block.name);
            blocksInScene[i] = new BlockData(_block.GetComponent<BlockSaveManager>());
            i++;
        }
        SaveSysteme.SaveBlock(blocksInScene);
    }

    public void LoadBlocks()
    {
        blocksInScene = SaveSysteme.LoadBlocks();

        GameObject[] blocksSpawned;
        blocksSpawned = new GameObject[blocksInScene.Length];

        List <GameObject> objectsWithBlockTag = new List<GameObject>();

        foreach(string _blockTag in blockTags)
        {
            foreach(GameObject _blockWithTag in GameObject.FindGameObjectsWithTag(_blockTag))
            {
                Destroy(_blockWithTag);
            }
        }

       /** foreach (GameObject _block in objectsWithBlockTag)
        {
            Destroy(_block);
        }**/

        int i = 0;
        foreach(BlockData _blockData in blocksInScene)
        {
            Debug.Log(_blockData.name);

            GameObject _usingBlockPrefab = null;

            foreach(GameObject _blockPrefab in blockPrefabs)
            {
                if (_blockPrefab.GetComponent<BlockSaveManager>().prefabName == _blockData.prefabName)
                {
                    Debug.Log("Name");
                    _usingBlockPrefab = _blockPrefab;
                }
            }
            blocksSpawned[i] = Instantiate(_usingBlockPrefab, null);
            blocksSpawned[i].SetActive(false);
            blocksSpawned[i].GetComponent<BlockSaveManager>().justLoaded = true;
            blocksSpawned[i].GetComponent<BlockSaveManager>().LoadData(_blockData);
            i++;
        }

        foreach(GameObject _blockSpawned in blocksSpawned)
        {
            _blockSpawned.GetComponent<BlockSaveManager>().LoadBlock();
            _blockSpawned.SetActive(true);
        }
        foreach (GameObject _blockSpawned in blocksSpawned)
        {
            _blockSpawned.GetComponent<BlockSaveManager>().LoadPhysics();
            _blockSpawned.SetActive(true);
        }

    }
}
