using UnityEngine;
using System.Collections;

public class ButtonTest : MonoBehaviour {

	public ResourceManager.DrugEnum drugID;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnMouseDown() {
		ResourceManager.drugs [drugID].PlayAction();
	}
}
