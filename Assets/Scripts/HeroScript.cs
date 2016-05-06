using UnityEngine;
using System.Collections;
using System;

public class HeroScript : MonoBehaviour {

	// TODO move to character
	public int damage = 10;

	public int websToDestroy = 5;				// number of webs to destroy // TODO to remove
 
 	private bool speedToRestore = false;
 	private float speedResetTimer = 1.0f;
 	private float oldSpeed;
	private CharacterScript character;
	private int destroyedSpiderWebs = 0;		// number of webs destroyed
 
	private Action winFunction = () => {};		// function to call when the level is finished

	private ParticleSystem flamethrower;		// TODO to move in a separate script at one point

	private void Awake() {
		ResourceManager.RegisterMainPlayer(this);

		this.character = this.GetComponent<CharacterScript>();
		character.SetDeathFunction( () => {
            Application.LoadLevel(Application.loadedLevelName); 
        });

        this.SetFinishLevelFunction( () => {
        	Application.LoadLevel("Scene1");
   		});

   		this.flamethrower = this.GetComponentInChildren<ParticleSystem>();
   		this.flamethrower.enableEmission = false;
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
				character.maxSpeed = oldSpeed;
				speedResetTimer = 1.0f;
				speedToRestore = false;
			}
		}
		
	}



    /*
    ========================
    OnCollisionEnter2D
    ========================
    */
    public void AdjustSpeed(float speedPercentage, bool temporary, float duration = 0.0f) {
    	if ( temporary ) {
    		oldSpeed = character.maxSpeed;
    		character.maxSpeed *= speedPercentage;
    		speedToRestore = true;
    		speedResetTimer = duration;
    	} else {
    		character.maxSpeed *= speedPercentage;
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
    SetFinishLevelFunction
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
