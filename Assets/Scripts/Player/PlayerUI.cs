using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Text highScoreText ;
    public Text healthText;
    public Text blockPointsText;

    private float localHighScore;
    private float localBlockPoints;

    private Health healthScript;
    private BlockPoints blockPoints;

    private void Start()
    {
        healthScript = GetComponent<Health>();
        blockPoints = GetComponent<BlockPoints>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {

        if (localHighScore < transform.position.y * 1000)
        {

            localHighScore = Mathf.Round(transform.position.y * 1000);
        }

        localBlockPoints = Mathf.Round(blockPoints.blockPoints * 10);

        highScoreText.text = "HighScore: " + localHighScore.ToString();
        healthText.text = "Health: " + healthScript.health.ToString();
        blockPointsText.text = "Block Points: " + blockPoints.blockPoints.ToString();


    }
}
