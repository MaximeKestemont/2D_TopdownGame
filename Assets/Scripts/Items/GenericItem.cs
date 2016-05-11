using UnityEngine;
using System.Collections;

public class GenericItem : MonoBehaviour {


	public int idItem;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    /*
    ========================
    OnTriggerEnter2D
    ========================
    */
	private void OnTriggerEnter2D(Collider2D other) {

	    if (other.gameObject.tag == "Player") {
	    	
	    	ResourceManager.Inventory.addItemToInventory(idItem);
	 
	        // destroy the item
	        Destroy(this.gameObject);
	    }
	}
}
