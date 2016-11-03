using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrugButtonCooldown : MonoBehaviour {

	public Drug drug = null;

	private Button button;

	// Use this for initialization
	void Start () {
		button = GetComponent<Button> ();
	}

	public void DisableButton() {
		button.interactable = false;
		StartCoroutine (EnableButton());
	}

	IEnumerator EnableButton() {
		Debug.Log (drug.drugCooldown);
		yield return new WaitForSeconds(drug.drugCooldown);
		button.interactable = true;
	}
}
