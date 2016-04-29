using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemyPrefab; 

	// Use this for initialization
	void Start () {
		// what comes out is an object, not a game object, hense "as GameObject"
		foreach(Transform child in transform){
			GameObject enemy = Instantiate(enemyPrefab, child.transform.position,Quaternion.identity) as GameObject;
			enemy.transform.parent = child;
			}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
