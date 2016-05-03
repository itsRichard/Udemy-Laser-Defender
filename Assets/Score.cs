using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour {
    public static int playerScore = 0;  //static stores value against the class, not the instance  
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

    public static void Reset()
    {
        playerScore = 0;
    }

}
