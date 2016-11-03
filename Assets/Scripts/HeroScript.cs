using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class HeroScript : MonoBehaviour {

	// Composition links
	private CharacterScript character;

	// TODO move to character
	public int damage = 10;

	public int websToDestroy = 5;				// number of webs to destroy // TODO to remove
 
 	// Invicible for a while when getting hurt 
	private bool isInvincible = false;
	private float invicibilityDuration = 1f;		
	private float timeSpentInvincible;

	// Related to drug 1
 	private bool speedToRestore = false;
 	private float speedResetTimer = 1.0f;
	private float baseSpeed;

	private int destroyedSpiderWebs = 0;		// number of webs destroyed
 
	private Action winFunction = () => {};		// function to call when the level is finished

	private ParticleSystem flamethrower;		// TODO to move in a separate script at one point

	private HUD hud;


	private void Awake() {

		ResourceManager.RegisterMainPlayer(this);

		this.character = this.GetComponent<CharacterScript>();
		this.baseSpeed = character.maxSpeed;

		character.SetDeathFunction( () => {
            Application.LoadLevel(Application.loadedLevelName); 
        });

        this.SetFinishLevelFunction( () => {
        	Application.LoadLevel("Scene1");
   		});

   		this.flamethrower = this.GetComponentInChildren<ParticleSystem>();
   		this.flamethrower.enableEmission = false;

		this.hud = GameObject.Find ("GameManager").GetComponent<HUD>();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		// If currently in speed boost, adjust the timer and restore old speed if needed
		if ( speedToRestore ) {
			this.speedResetTimer -= Time.deltaTime;
			if ( speedResetTimer < 0.0 ) {
				character.maxSpeed = this.baseSpeed;
				speedResetTimer = 1.0f;
				speedToRestore = false;
			}
		}

		/*
		foreach ( KeyValuePair<ResourceManager.DrugEnum,Drug> drug in ResourceManager.drugs ) {
			drug.Value.AutomaticDecrease ();
		}
		*/
	
		// Handle the invincibility of the player when he just got hit
		if (isInvincible) {
			timeSpentInvincible += Time.deltaTime;
		 
		  	if (timeSpentInvincible < invicibilityDuration) {
		    	float remainder = timeSpentInvincible % .3f;
		    	GetComponent<Renderer>().enabled = remainder > .15f; 			// make the player blink every 0.15s
		  	} else {
		    	GetComponent<Renderer>().enabled = true;
		    	isInvincible = false;
		  	}
		}

	}



    /*
    ========================
    AdjustSpeed
    ========================
    */
	// TODO currently a bug if used 2 times in a row -> need to remember the basis speed, the old speed
    public void AdjustSpeed(float speedPercentage, bool temporary, float duration = 0.0f) {
    	if ( temporary ) {
    		character.maxSpeed *= speedPercentage;
    		speedToRestore = true;
    		speedResetTimer = duration;
    	} else {
    		character.maxSpeed *= speedPercentage;
    	}
    }


    /*
    ========================
    AdjustHealth
    ========================
    */
    public void AdjustHealth(int amount) {
    	character.AdjustHealth(amount);
    }


    /*
    ========================
    InflictDamage
    ========================
    */
    // When parameter isInvincibleValid is set to true, the invicible window is taken into account.
    // If not, then the player suffers damage even if he is in invicible window.
    public void InflictDamage(int amount, bool isInvincibleValid) {
    	
    	if ( !isInvincible && isInvincibleValid) {
    		isInvincible = true;
    		timeSpentInvincible = 0;
    		AdjustHealth(amount);
    	} else 
    	// This ignores the invicibity window, but does not reset the timer
    	if ( !isInvincibleValid ) {
    		AdjustHealth(amount);
    	}
    }

    /*
    ========================
    AdjustDestroyedWebs
    ========================
    */
	public void AdjustDestroyedWebs(int amount) {
	    
	    this.destroyedSpiderWebs += amount;
	 
	    // check for win condition // TODO change this win condition
	    if (this.destroyedSpiderWebs >= websToDestroy) {
	        // save number of destroyed webs
	        PlayerPrefs.SetInt("DestroyedWebs", destroyedSpiderWebs);
	 
	        // load win level
	        this.winFunction();
	    }
	}
	 

    /*
    ========================
    SetFinishLevelFunction
    ========================
    */
	public void SetFinishLevelFunction(Action function) {
	    this.winFunction = function;
	}


    /*
    ========================
	SetFlamethrowerEmitter
    ========================
    */
	public void SetFlamethrowerEmitter(Boolean b) {
	    this.flamethrower.enableEmission = b;
	}


    /*
    ========================
    OnCollisionEnter2D
    ========================
    */
	private void OnCollisionEnter2D(Collision2D other) {
	    if (other.gameObject.tag == "Enemy") {
	        other.gameObject.GetComponent<CharacterScript>().AdjustHealth(-1 * this.damage);
	    }
	}



}
