using UnityEngine;
using System.Collections;
using System;
 
public class CharacterScript : MonoBehaviour 
{
    
    public float maxSpeed = 5.0f;					// max speed of character
	public float facingAngleAdjustment = -90.0f;	// adjustment for rotation based on sprite starting orientation
	public int maxHealth = 100;
	public Texture2D progressBarFull;
	public Texture2D progressBarEmpty;

	// Cached objects
    private Rigidbody2D cachedRigidBody2D;
    private Transform cachedTransform;
    private Animator animator;
 
    private float speed;							// current speed
    private int health = 100;
    private Vector2 healthScale;					// scale of the health bar
    

    // Death function
    private Action deathFunction = () => {};

    // TODO To move in constant file?
    private Vector2 healthBarSize = new Vector2(60,20);
    private int x_healthMargin = 30;
    private int y_healthMargin = 40;


 
    private void Start()
    {
        this.cachedRigidBody2D = this.GetComponent<Rigidbody2D>();
        this.cachedTransform = this.GetComponent<Transform>();
        this.animator = this.GetComponent<Animator>();

        //this.healthScale = healthBar.transform.localScale;
    }



    public void OnGUI() 
    {
    	// this is more optimal to refresh manually the health when modifying it (in AdjustHealth), than doing it at each frame here.
    	// However, modifying the GUI cannot be done outside OnGUI(). Should consider moving the healthbar to another kind of rendering,
    	// maybe with objects with the Texture component inside, as a renderer.
    	UpdateHealthBar();
 	} 

 
    /*
    ========================
    SetDeathFunction
    ========================
    */
    // This allows a modular behaviour. A new object can be created, the character script added to it, then the Death function
    // set in another script, so that the Death can change without having to modify or extend this code.
    public void SetDeathFunction(Action d) {
    	deathFunction = d;
    }
 

    /*
    ========================
    Move
    ========================
    */
    public void Move(Vector2 movement)
    {
        // move the rigid body, which is part of the physics system
        // This ensures smooth movement
        this.cachedRigidBody2D.velocity = new Vector2(movement.x * maxSpeed, movement.y * maxSpeed);

		// set the speed variable in the animation component to ensure proper state
		speed = Mathf.Abs(movement.x) + Mathf.Abs(movement.y);
        this.animator.SetFloat("Speed", speed);

		// adjust the character orientation if moving
		if (movement.magnitude > 0) {
    		float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg + facingAngleAdjustment;
    		if ( speed > 0.0f ) {
        		// rotate by angle around the z axis
        		this.cachedTransform.rotation = Quaternion.AngleAxis(angle, new Vector3(0, 0, 1));
    		}
    	} else {
    		// this is a bit of a hack. Without it, the character seems to rotate indefinitely when hitting a wall
    		this.cachedRigidBody2D.angularVelocity = 0;
    	}
    }



    /*
    ========================
    AdjustHealth
    ========================
    */
    public void AdjustHealth(int amount) {
    	this.health+=amount;
 	
    	if ( this.health > this.maxHealth ) {
    	    this.health = maxHealth;
    	} else if ( this.health < 0 ) {
    		Debug.Log("DEATH");
    	    this.deathFunction();
    	}

    }


	 
    /*
    ========================
    UpdateHealthBar
    ========================
    */
	private void UpdateHealthBar() {

    	Vector3 p =  Camera.main.WorldToScreenPoint(this.transform.position);		// position relative to the screen
     
     	float healthPercentage = this.GetHealth() * 1.0f / this.maxHealth;

    	// Draw health bar
 	 	GUI.contentColor = Color.Lerp(Color.green, Color.red, 1 - this.GetHealth() * 0.01f);
 	 	GUI.Label(
 	 		new Rect(
 	 			p.x - x_healthMargin, 
 	 			Screen.height - p.y - y_healthMargin, 
 	 			healthBarSize.x, // * healthPercentage TODO the label will scale entirely, not just x...need to refactor
 	 			healthBarSize.y), 
 	 		progressBarFull);
   	 	GUI.DrawTexture(
   	 		new Rect(
   	 			p.x - x_healthMargin, 
   	 			Screen.height - p.y - y_healthMargin, 
   	 			healthBarSize.x, 
   	 			healthBarSize.y), 
   	 		progressBarEmpty, 
   	 		ScaleMode.StretchToFill); 
	}



    /*
    ========================
    GetHealth
    ========================
    */
	public float GetHealth() {
	    return this.health;
	}


    /*
    ========================
    GetHealth
    ========================
    */
    // TODO character should be split between player and ennemies, so that the collision can be specific to each. Currently a player
    // is not suffering from collisions because he is not tagged as an enemy.
	public void OnParticleCollision(GameObject other) {

		// TODO temporary code, need to be refactor so that particles have a ParticleScript attached, with a variable.
		// Then, the check here can be done on this variable (with enum value), to check if this is coming from a flamethrower.
		// OR, the collision can be handled in the particle script directly, so that it does not need to be inherited/rewritten for 
		// every enemy classes.
		if (other.name == "flamethrower_2") {
			AdjustHealth(-1);
		}
		

	}
 
}