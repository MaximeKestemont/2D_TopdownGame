using UnityEngine;
using System.Collections;

public class PoisonScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /*
    ========================
    OnParticleCollision
    ========================
    */
    void OnParticleCollision(GameObject other) {
    	
    	Debug.Log("COLLISION : " + other);
    	other.GetComponent<CharacterScript>().AdjustHealth(-1);
        
    }
}
