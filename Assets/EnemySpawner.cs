using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemyPrefab; 

	// Use this for initialization
	void Start () {
		// what comes out is an object, not a game object, hense "as GameObject"
		GameObject enemy = Instantiate(enemyPrefab, new Vector3(0,0,0),Quaternion.identity) as GameObject;
		enemy.transform.parent = transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
