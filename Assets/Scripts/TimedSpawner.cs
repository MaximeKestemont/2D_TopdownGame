using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TimedSpawner : MonoBehaviour {

	
	public GameObject objectToSpawn;					// the object it will spawn
	public float timeToSpawn = 10.0f;
	public int maxObjects = 5;

	private float internalTimer = 0.0f;
	private List<GameObject> spawnedObjects = new List<GameObject>();


	void Start () {
	
	}
	

	void Update () {
    	
    	this.internalTimer += Time.deltaTime;
 	
    	if ( this.internalTimer > this.timeToSpawn && this.spawnedObjects.Count < this.maxObjects ) {
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
		//spawnedObjects += 1;
    	GameObject obj = (GameObject) Instantiate( this.objectToSpawn,
    									 		   this.transform.position,
        			 					 		   Quaternion.identity );
    	spawnedObjects.Add(obj);
    }


    /*
    ========================
    OnTriggerEnter2D
    ========================
    */
	private void OnTriggerEnter2D(Collider2D other) {

	    if (other.gameObject.tag == "Player") {
	    	// Destroy the spawned spiders
	        foreach (GameObject obj in spawnedObjects) {
	            Destroy(obj);
	        }
	 
	        other.GetComponent<HeroScript>().AdjustDestroyedWebs(1);
	 
	        // destroy the spawner
	        Destroy(this.gameObject);
	    }
	}
}
