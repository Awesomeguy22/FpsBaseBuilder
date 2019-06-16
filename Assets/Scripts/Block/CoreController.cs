using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CoreController : MonoBehaviour
{
    public float rotateSpeed;
    public Text gameover;
    public GameObject CoreCamera;

    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.one * rotateSpeed * Time.deltaTime);
    }

    private void OnDestroy()
    {
        gameover.gameObject.SetActive(true);
        if (CoreCamera != null)
        {
            CoreCamera.SetActive(true);

        }
        Destroy(player);
    }
}
