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
    public float spawnDelay = 0.5f;

    // Use this for initialization derp derp derp
    void Start () {
        SetPlayspaceBoundaries();
        SpawnEnemiesUntilFull();
	}

    public void SetPlayspaceBoundaries()
    {
        float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceToCamera));
        Vector3 rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distanceToCamera));
        xMax = rightBoundary.x;
        xMin = leftBoundary.x;
    }

    void SpawnEnemiesUntilFull()
    {
        Transform freePosition = NextFreePosition();
        if (freePosition)
        {
            GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = freePosition;  // set the enemy's parent as the free position that was just filled
        }

        if (NextFreePosition()) // only spawn if there is a free position REVIEW THIS AGAIN  WITH AND WITHOUT THIS  
        {
            Invoke("SpawnEnemiesUntilFull", spawnDelay); // add a time delay
        }

       
    }

    public void SpawnEnemies()
    {
        foreach(Transform child in transform)
        {
            GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
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

        if (AllMembersDead())
        {
            Debug.Log("Empty Formation.  All members are dead.");
            SpawnEnemiesUntilFull();
        }
    }

    bool AllMembersDead()
    {
        // should be six
        Debug.Log("Transform.childcount is " + transform.childCount);
        // checking the transform in the transform (positions in the enemy formation)
        foreach (Transform childPositionGameObject in transform)  
        {
            if (childPositionGameObject.childCount > 0)
            {
                return false; // if any position has more than one, all members are not dead.  
            }
        }
        return true;
    }

    Transform NextFreePosition()
    {
        foreach(Transform childPositionGameObject in transform)
        {
            if (childPositionGameObject.childCount < 1)
            {
                return childPositionGameObject; 
            }
        }
        return null;
    }
    
}
