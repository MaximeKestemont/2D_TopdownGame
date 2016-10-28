using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrugLevel : MonoBehaviour {


	public float drugLevelThreshold1 = 100f;
	public float currentLevel = 0f;

	public GameObject healthBar;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("increaseDrugLevel", 1f, 1f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void increaseDrugLevel() {
		currentLevel += 2;
		float myHealth = currentLevel / drugLevelThreshold1;
		setHealthBar (myHealth);
	}

	void setHealthBar(float myHealth) {
		healthBar.transform.localScale = new Vector3 (myHealth, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
	}
}
