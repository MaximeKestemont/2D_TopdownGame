using UnityEngine;
using System.Collections;
 
[RequireComponent(typeof(CharacterScript))]
public class EnemyAIScript : MonoBehaviour {
 
	public enum EnemyState {
		searching,
		following
	}
 
 	// TODO move those variables into a class specific to the character
	public float timeToChangeDirection = 5.0f;				// time in seconds till the character determines a new heading
	public int damage = 5;									// amount of damage to do to the player
 	public float attackSpeed = 1.0f;
     

	private CharacterScript character;						// reference to the character script	
	private float attackSpeedTimer;							// internal timer to track last time dealt damage
 	private float internalTimer = 0.0f;						// internal timer to keep track of new heading
	private Vector2 heading;								// direction relative to the character to head
 
	private EnemyState currentState;
	private GameObject target;
	private GameObject spawnedBy;
 

	
	private void Awake () {
		this.character = this.GetComponent<CharacterScript>();
		this.CreateNewHeading();
		this.attackSpeedTimer = this.attackSpeed;
		this.currentState = EnemyState.searching;
	}
 

	private void Start() {
		this.character.SetDeathFunction( () => {
			Destroy(this.gameObject);
		});
	}
 
 
 

	private void Update () {
		// increment our damage timer by the time it took to complete the last frame
		this.attackSpeedTimer += Time.deltaTime;
	}

 
	private void FixedUpdate() {
 		// Dispatch behaviour
		if ( this.currentState == EnemyState.searching ) {
			this.Search();
		} else if ( this.currentState == EnemyState.following ) {
			this.Follow();
		}
	}
 

    /*
    ========================
    Follow
    ========================
    */
	private void Follow() {
		Vector3 direction = this.target.transform.position - this.transform.position;
		direction.Normalize();
		character.Move(direction);
	}
 

    /*
    ========================
    Search
    ========================
    */
	private void Search() {
		this.internalTimer+=Time.deltaTime;
 
		// determine if we need to create a new heading
		if ( this.internalTimer > this.timeToChangeDirection ) {
			this.CreateNewHeading();
			this.internalTimer = 0.0f;
		}
 
		character.Move(heading);
	}
 
    /*
    ========================
    CreateNewHeading
    ========================
    */
	private void CreateNewHeading() {
		float xCoord=Random.Range(-100.0f, 100.0f);
		float yCoord=Random.Range(-100.0f, 100.0f);
 
		heading=new Vector2(xCoord, yCoord);
		heading.Normalize();
	}
 
    /*
    ========================
    OnCollisionEnter2D
    ========================
    */
    private void OnCollisionEnter2D(Collision2D other) {
  
		if ( other.gameObject.tag == "Player" && this.attackSpeedTimer > this.attackSpeed ) {
			other.gameObject.GetComponent<HeroScript>().InflictDamage( -1 * this.damage, true );
			this.attackSpeedTimer = 0.0f;
		}
 
 		// create a new heading after hitting something
		this.CreateNewHeading();
	}
 
    
    /*
    ========================
    SetSpawnedBy
    ========================
    */
	public void SetSpawnedBy(GameObject obj) {
		this.spawnedBy = obj;
	}

    /*
    ========================
    GetSpawnedBy
    ========================
    */ 
	public GameObject GetSpawnedBy() {
		return this.spawnedBy;
	}
 

    /*
    ========================
    OnTriggerEnter2D
    ========================
    */ 
	private void OnTriggerEnter2D(Collider2D other) {

		// Follow the player // TODO IS TAG A GOOD WAY TO IDENTIFY PLAYER?
		if ( other.gameObject.tag == "Player" ) {
			this.currentState=EnemyState.following;
			this.target = other.gameObject;
		}
	}
 

    /*
    ========================
    OnTriggerExit2D
    ========================
    */ 
	private void OnTriggerExit2D(Collider2D other) {

		if ( other.gameObject.tag == "Player" ) {
			this.currentState=EnemyState.searching;
			this.target = null;
		}
	}
}
