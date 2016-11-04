using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBox : MonoBehaviour {

	public int distanceX = 2;				// distance moving in the X-axis when pushed
	public int distanceY = 2;				// distance moving in the Y-axis when pushed
	public int moveSpeed = 1;

	private Vector2 target;
	private bool isMoving = false;


	private Coroutine coroutine;

	private void OnCollisionEnter2D(Collision2D other) {

		if (other.gameObject.tag == "Player" && !isMoving) {
			isMoving = true;
			coroutine = StartCoroutine ( MoveBox (other));
		}

		if (other.gameObject.tag != "Player") {
			StopCoroutine (coroutine);
			isMoving = false;
		}
	}

	IEnumerator MoveBox(Collision2D other) {

		// ---- COMPUTE DIRECTION ----
		Vector2 boxPosition =  this.transform.position;
		Vector2 playerPosition = other.gameObject.transform.position;

		float diffX = boxPosition.x - playerPosition.x;
		float diffY = boxPosition.y - playerPosition.y;

		// The box can only move in one direction (the one the player pushed)
		if ( Mathf.Abs(diffX) > Mathf.Abs(diffY)) {
			// if diffX > 0, this means that the player is on the left side of the box
			if ( diffX > 0 ) {		
				target = new Vector2(boxPosition.x + distanceX, boxPosition.y);
			} else {
				target = new Vector2(boxPosition.x - distanceX, boxPosition.y);
			}
		} else {
			// if diffY > 0, this means that the player is on the top side of the box
			if ( diffY > 0 ) {		
				target = new Vector2(boxPosition.x, boxPosition.y + distanceY);
			} else {
				target = new Vector2(boxPosition.x, boxPosition.y - distanceY);
			}		
		}

		// ---- MOVE BOX ----
		while ( (Vector2) transform.position != target ) {
			transform.position = Vector2.MoveTowards (transform.position, target, moveSpeed * Time.deltaTime);
			yield return null;
		}
			
		isMoving = false;	
	}

}
