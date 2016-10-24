using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Renderer> ().enabled = false;
		ResourceManager.AddHallucinatedObjects (this);
	}
	
	// Update is called once per frame
	void Update () {
//		if (ResourceManager.MainPlayer.drug1Level > 5) {
//			setVisibility (false);
//		}
	}

	public void setVisibility(bool flag) {
		GetComponent<Renderer>().enabled = flag;
	}
}
