using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour {
    public int playerScore = 0;
    private Text myText;  

  
    void Start()
    {
        myText = GetComponent<Text>();
        Reset();
    }

    public void ScorePoints(int points)
    {
        playerScore += points;
        myText.text = playerScore.ToString();
    }

    public void Reset()
    {
        playerScore = 0;
        myText.text = playerScore.ToString();
    }

}
