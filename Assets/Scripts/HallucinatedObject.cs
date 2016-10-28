using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallucinatedObject : MonoBehaviour {

	private Renderer renderer;
	private Collider2D collider;

	// Use this for initialization
	void Start () {
		renderer = this.GetComponent<Renderer> ();
		collider = this.GetComponent<Collider2D> ();
		renderer.enabled = false;
		collider.enabled = false;
		ResourceManager.AddHallucinatedObjects (this);

		// Set the opacity to 0
		renderer.material.color = new Color (1.0f, 1.0f, 1.0f, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void SetVisibility(bool flag) {
		renderer.enabled = flag;
		if (flag)
			collider.enabled = true; // TODO check if it can be rendered enabled (if already an object there, it will crash for now)
		else
			collider.enabled = false;
	}

	public bool GetVisibility() {
		return renderer.enabled;
	}

	public float GetOpacity() {
		return renderer.material.color.a;
	}

	public Renderer GetRenderer { get { return renderer; } }
}
