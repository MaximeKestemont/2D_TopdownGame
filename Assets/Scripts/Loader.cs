using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is used to load resources. Every resources loaded here will be stored in the ResourceManager.
public class Loader : MonoBehaviour {

	public List<Drug> drugs = new List<Drug> ();

	// Use this for initialization
	void Start () {

		// Add the drugs to the ResourceManager
		foreach (Drug drug in drugs) {
			ResourceManager.drugs.Add (drug.key, drug);
		}
	}
	

}
