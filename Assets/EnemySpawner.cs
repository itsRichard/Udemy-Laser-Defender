using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemyPrefab;
    public float width = 10f;
    public float height = 5f;
    private bool movingRight = true;
    public float speed = 5f;
    private float xMax;
    private float xMin;

    // Use this for initialization
    void Start () {

        float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceToCamera));
        Vector3 rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distanceToCamera));
        xMax = rightBoundary.x;
        xMin = leftBoundary.x;

        // what comes out is an object, not a game object, hense "as GameObject"
        foreach (Transform child in transform){
            GameObject enemy = Instantiate(enemyPrefab, child.transform.position,Quaternion.identity) as GameObject;
			enemy.transform.parent = child;
			}
	}

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));
    }

    private float clampXmin, clampXmax;

    // Update is called once per frame
    void Update () {

        if (movingRight)
        {
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        }
        else
        {
            transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
            // can also be done as Vector3.left * speed * Time.deltaTime
        }

        float rightEdgeOfFormation = transform.position.x + (0.5f * width);
        float leftEdgeOfFormation = transform.position.x - (0.5f * width);

        if (leftEdgeOfFormation < xMin)
        {
            movingRight = true;
        } else if(rightEdgeOfFormation > xMax)
        {
            movingRight = false;
        }
    }
}
