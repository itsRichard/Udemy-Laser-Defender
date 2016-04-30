using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed = 15.0f;
	public float padding = 1.0f;
	public float xmin;
	public float xmax;
    public float playerProjectileSpeed;
    public float playerFireRate = 0.2f;
    public float health = 250f;

    public GameObject projectile;
	
	// Use this for initialization
	void Start () {
		//calculate xmin and xmax from the camera itself
		float distance = transform.position.z - Camera.main.transform.position.z;	//distance between camera and object
		Vector3 leftMost =  Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance)); // 0,0 is bottom left corner
		Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1,0,distance)); // 1,0 is the bottom right corner
		xmin = leftMost.x + padding;
		xmax = rightMost.x - padding;
	}
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.LeftArrow)){
			//transform.position += new Vector3(-speed * Time.deltaTime,0,0);
			transform.position += Vector3.left * speed * Time.deltaTime;
		}
		else if (Input.GetKey(KeyCode.RightArrow)){
			//transform.position += new Vector3(speed * Time.deltaTime,0,0);
			transform.position += Vector3.right * speed * Time.deltaTime;
		}
		
		// clamp the value 
		float newX = Mathf.Clamp (transform.position.x, xmin, xmax);
		// move to a new x position that is now clamped between xmin and xmax.  
		transform.position = new Vector3 (newX,transform.position.y, transform.position.z);

        // fire rate control
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("Fire", 0.000001f, playerFireRate);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("Fire");
        }
	} 

    void OnTriggerEnter2D(Collider2D collider)
    {
        // Get the projectile that hit the enemy

        Projectile missile = collider.gameObject.GetComponent<Projectile>();
        if (missile)
        {
            Debug.Log("Player Hit");
            health -= missile.GetDamage();
            missile.hit();
            if (health <= 0)
            {
                Destroy(gameObject);
            }
 
        }


    }

    void Fire()
    {
        Vector3 offset = new Vector3(0, 1, 0);
        // Create Projectile based on the project prefab attached to this game object script
        GameObject playerProjectile = Instantiate(projectile, transform.position+offset, Quaternion.identity) as GameObject;
        // Shoot it 
        playerProjectile.GetComponent<Rigidbody2D>().velocity = new Vector3(0, playerProjectileSpeed, 0);
    }
}
