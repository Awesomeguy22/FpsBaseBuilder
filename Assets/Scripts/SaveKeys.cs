using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveKeys : MonoBehaviour
{
    public GameObject blockSpawner;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            blockSpawner.GetComponent<Blocks_SaveManager>().SaveBlocks();
            player.GetComponent<PlayerSaveManager>().SavePlayer();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            blockSpawner.GetComponent<Blocks_SaveManager>().LoadBlocks();
            player.GetComponent<PlayerSaveManager>().LoadPlayer();
        }
    }
}
