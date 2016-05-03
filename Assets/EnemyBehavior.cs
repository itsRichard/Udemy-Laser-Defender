using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {
    public float health = 150;

    public GameObject projectile;
    public float enemyProjectileSpeed = 5f;
    public float shotsPerSecond = 0.5f;
    public int scoreValue = 150;
    private Score scoreKeeper;
    public AudioClip fireSound;
    public AudioClip deadSound;

    void Start()
    {
        scoreKeeper = GameObject.Find("Score").GetComponent<Score>();
    }

    void OnTriggerEnter2D(Collider2D collider)
        {
        // Get the projectile that hit the enemy
        Projectile missile = collider.gameObject.GetComponent<Projectile>();
        if (missile)
            {
            health -= missile.GetDamage();
            missile.hit();
            if (health <= 0)
            {
                Destroy(gameObject);
                scoreKeeper.ScorePoints(scoreValue);
                AudioSource.PlayClipAtPoint(deadSound, transform.position);
            }
            Debug.Log("Hit by a projectile");
            }
        }

    void Update()
    {
        // Probability to Fire = Time Elapsed X Frequency 
        float probability = shotsPerSecond * Time.deltaTime;
        if(Random.value < probability)
        {
            Fire();
        }
    }
    
    void Fire()
    {
        Vector3 startPosition = transform.position + new Vector3(0, 0F, 0);
        GameObject enemyProjectile = Instantiate(projectile, startPosition, Quaternion.identity) as GameObject;
        enemyProjectile.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -enemyProjectileSpeed, 0);
        AudioSource.PlayClipAtPoint(fireSound, transform.position);
    }
}
