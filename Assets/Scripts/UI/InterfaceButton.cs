using UnityEngine;
using System.Collections;

public class InterfaceButton : MonoBehaviour {

	// TODO DEPECRATED, only there to show how button can also be done

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
