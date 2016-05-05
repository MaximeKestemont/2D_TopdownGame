using UnityEngine;
using System.Collections;
using System;

public class HeroScript : MonoBehaviour {

	// TODO move to character
	public int damage = 10;

	public int websToDestroy = 5;				// number of webs to destroy // TODO to remove
 
	private CharacterScript character;
	private int destroyedSpiderWebs = 0;		// number of webs destroyed
 
	private Action winFunction = () => {};		// function to call when the level is finished


	private void Awake() {
		this.character = this.GetComponent<CharacterScript>();
		character.SetDeathFunction( () => {
            Application.LoadLevel(Application.loadedLevelName); 
        });

        this.SetFinishLevelFunction( () => {
        	Application.LoadLevel("Scene1");
   		});
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    /*
    ========================
    OnCollisionEnter2D
    ========================
    */
	private void OnCollisionEnter2D(Collision2D other)
	{
	    if (other.gameObject.tag == "Enemy") {
	        other.gameObject.GetComponent<CharacterScript>().AdjustHealth(-1 * this.damage);
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
	public void SetFinishLevelFunction(Action function)
	{
	    this.winFunction = function;
	}
}
