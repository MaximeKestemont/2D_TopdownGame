using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drug : MonoBehaviour {

	public ResourceManager.DrugEnum key;
	public Sprite sprite;

	// Cooldown for taking this drug again
	public float drugCooldown = 1.0f; 

	public float drugLevel = 0.0f;
	public int drugDecreaseValue = 1;

	// treshold value for the side-effect to appear
	public float tresholdValue = 10f;
	public float maxValue = 100f;


	void Update () {
		PlayEffect ();
		AutomaticDecrease ();
	}

	/*
	========================
	AutomaticDecrease
	========================
	*/
	public void AutomaticDecrease() {
		if (drugLevel > 0) {
			drugLevel = Mathf.Max (0, drugLevel - drugDecreaseValue * Time.deltaTime);
		}
	}

	/*
	========================
	AdjustDrugLevel
	========================
	*/
	public void AdjustDrugLevel(int amount) { 
		this.drugLevel += amount;
	}

	/*
	========================
	GetDrugLevel
	========================
	*/
	// Action related to the drug usage
	public float GetDrugLevel () {
		return drugLevel;
	}




	/*
	========================
	PlayEffect
	========================
	*/
	// Lasting effect of the drug
	public virtual void PlayEffect () {
		// This need to be overriden in child classes.
	}

	/*
	========================
	PlayAction
	========================
	*/
	// Action related to the drug usage
	public virtual void PlayAction () {
		// This need to be overriden in child classes.
	}



}
