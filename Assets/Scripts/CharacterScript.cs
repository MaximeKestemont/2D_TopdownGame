using UnityEngine;
using System.Collections;
using System;
 
public class CharacterScript : MonoBehaviour 
{
    
    public float maxSpeed = 5.0f;					// max speed of character
	public float facingAngleAdjustment = -90.0f;	//adjustment for rotation based on sprite starting orientation

    // cached version of our physics rigid body
    private Rigidbody2D cachedRigidBody2D;
    private Transform cachedTransform;
    private Animator animator;
 

    private float speed;						// current speed

    void Awake()
    {
    }
 
    private void Start()
    {
        this.cachedRigidBody2D = this.GetComponent<Rigidbody2D>();
        this.cachedTransform = this.GetComponent<Transform>();
        this.animator = this.GetComponent<Animator>();
    }
 
    public void Move(Vector2 movement)
    {
        // move the rigid body, which is part of the physics system
        // This ensures smooth movement
        this.cachedRigidBody2D.velocity = new Vector2(movement.x * maxSpeed, movement.y * maxSpeed);

		//set the speed variable in the animation component to ensure proper state
		speed = Mathf.Abs(movement.x) + Mathf.Abs(movement.y);
        this.animator.SetFloat("Speed", speed);

		// adjust the character orientation
    	float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg + facingAngleAdjustment;
    	if ( speed > 0.0f ) {
        	//rotate by angle around the z axis.
        	this.cachedTransform.rotation = Quaternion.AngleAxis(angle, new Vector3(0, 0, 1));
    	}
    }


    public delegate void Death();
 
    public void SetDeathFunction(Death d) {
 
    }
 
    public void AdjustHealth(int i){
    }
 
}