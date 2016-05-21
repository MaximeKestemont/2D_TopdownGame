using UnityEngine;
using System.Collections;

public class MovingBox : MonoBehaviour {

	public int distanceX = 2;				// distance moving in the X-axis when pushed
	public int distanceY = 2;				// distance moving in the Y-axis when pushed
	public int moveSpeed = 1;

	private Vector2 target;
	private bool isMoving = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if ( isMoving ) {
			transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * moveSpeed);
			
			// If closer than the delta, then it will not move anymore
			if ( Mathf.Abs(transform.position.magnitude - target.magnitude) < Time.deltaTime * moveSpeed ) {
				isMoving = false;
			}
		}

	}

    /*
    ========================
    OnCollisionEnter2D
    ========================
    */
	private void OnCollisionEnter2D(Collision2D other) {

	    if (other.gameObject.tag == "Player" && isMoving == false) {

	        Vector2 boxPosition =  this.transform.position;
	        Vector2 playerPosition = other.gameObject.transform.position;

	        float diffX = boxPosition.x - playerPosition.x;
	        float diffY = boxPosition.y - playerPosition.y;

	        // The box can only move in one direction (the one the player pushed)
	        if ( Mathf.Abs(diffX) > Mathf.Abs(diffY)) {
	        	if ( diffX > 0 ) {		// if diffX > 0, this means that the player is on the left side of the box
	        		target = new Vector2(boxPosition.x + distanceX, boxPosition.y);
	        	} else {
					target = new Vector2(boxPosition.x - distanceX, boxPosition.y);
	        	}
	        } else {
	        	if ( diffY > 0 ) {		// if diffY > 0, this means that the player is on the top side of the box
	        		target = new Vector2(boxPosition.x, boxPosition.y + distanceY);
	        	} else {
					target = new Vector2(boxPosition.x, boxPosition.y - distanceY);
	        	}
	        }
	        isMoving = true;
	    }
	}
}
