using UnityEngine;
using System.Collections;

public class TimedSpawner : MonoBehaviour {

	
	public GameObject objectToSpawn;					// the object it will spawn
	public float timeToSpawn = 10.0f;
	public int maxObjects = 5;

	private float internalTimer = 0.0f;
	private int spawnedObjects = 0;


	void Start () {
	
	}
	

	void Update () {
    	
    	this.internalTimer += Time.deltaTime;
 	
    	if ( this.internalTimer > this.timeToSpawn && this.spawnedObjects < this.maxObjects ) {
    	    this.Spawn();
    	    this.internalTimer = 0.0f;
    	}
	}


    /*
    ========================
    Spawn
    ========================
    */
	private void Spawn() {
		spawnedObjects += 1;
    	Instantiate( this.objectToSpawn,
        			 this.transform.position,
        			 Quaternion.identity );
    }
}
