using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Text myText = gameObject.GetComponent<Text>();
        myText.text = Score.playerScore.ToString();
        Score.Reset();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
