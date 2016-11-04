using UnityEngine;
using System.Collections;

// TODO to refactor. Should use the physic API (with the PushForce), instead of the check on the target. Especially as the moveSpeed
// is not used anymore...
public class MovingBox : MonoBehaviour {

	public int distanceX = 2;				// distance moving in the X-axis when pushed
	public int distanceY = 2;				// distance moving in the Y-axis when pushed
	public int moveSpeed = 1;

	private Vector2 target;
	private bool isMoving = false;

	// Use this for initialization
	void Start () {
		this.GetComponent<Rigidbody2D>().constraints = 
			RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
	   	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if ( isMoving ) {
			
			// If closer than the delta, then it will not move anymore
			if ( Mathf.Abs(transform.position.magnitude - target.magnitude) < Time.deltaTime * moveSpeed ) {
				isMoving = false;
				this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
			}
		}

	}

    /*
    ========================
    OnCollisionEnter2D
    ========================
    */
	private void OnCollisionEnter2D(Collision2D other) {
		Debug.Log ("COLLISION");
		Debug.Log ("Object : " + other.gameObject.tag);
		Debug.Log ("isMoving : " + isMoving );
	    if (other.gameObject.tag == "Player" && isMoving == false) {

	        Vector2 boxPosition =  this.transform.position;
	        Vector2 playerPosition = other.gameObject.transform.position;

	        float diffX = boxPosition.x - playerPosition.x;
	        float diffY = boxPosition.y - playerPosition.y;

	        // The box can only move in one direction (the one the player pushed)
	        if ( Mathf.Abs(diffX) > Mathf.Abs(diffY)) {
	        	if ( diffX > 0 ) {		// if diffX > 0, this means that the player is on the left side of the box
	        		target = new Vector2(boxPosition.x + distanceX, boxPosition.y);
					Debug.Log ("RIGHT");
	        	} else {
					target = new Vector2(boxPosition.x - distanceX, boxPosition.y);
					Debug.Log ("LEFT");
	        	}
	        	this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY; 

	        } else {
	        	if ( diffY > 0 ) {		// if diffY > 0, this means that the player is on the top side of the box
	        		target = new Vector2(boxPosition.x, boxPosition.y + distanceY);
					Debug.Log ("BOTTOM");
	        	} else {
					target = new Vector2(boxPosition.x, boxPosition.y - distanceY);
					Debug.Log ("UP");
	        	}
	        	this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX; 
	        }
	        isMoving = true;

	    } 
	    
	    // If colliding with something else than the player, stop moving.
	    if (other.gameObject.tag != "Player") {

			//target = this.transform.position;
	    	isMoving = false;
	    	this.GetComponent<Rigidbody2D>().constraints = 
	    		RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY; 
	    }
	}
}
