using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drug : MonoBehaviour {

	public ResourceManager.DrugEnum key;
	public Sprite sprite;

	public float drugLevel = 0.0f;
	public int drugDecreaseValue = 1;


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
