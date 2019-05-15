using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    public Text highScoreText = null;

    private float highScore;

   
    // Update is called once per frame
    void FixedUpdate()
    {
        if (highScore < transform.position.y * 1000)
        {
            
            highScore = Mathf.Round(transform.position.y * 1000);
        }

        highScoreText.text = "HighScore:" + highScore.ToString();
    }
}
