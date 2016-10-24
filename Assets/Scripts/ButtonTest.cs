using UnityEngine;
using System.Collections;

public class ButtonTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnMouseDown() {
		ResourceManager.MainPlayer.AdjustSpeed(2.0f, true, 5.0f);
		ResourceManager.MainPlayer.AdjustDrugLevel (10, 0); // TODO 0 to an enum corresponding to the drug
	}
}
