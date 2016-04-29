using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed = 15.0f;
	public float padding = 1.0f;
	float xmin;
	float xmax;

	
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
	
	}
}
