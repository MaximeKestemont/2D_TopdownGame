using UnityEngine;
using System.Collections;

public class GenericItem : MonoBehaviour {

	public int idItem;

    /*
    ========================
    OnTriggerEnter2D
    ========================
    */
	private void OnTriggerEnter2D(Collider2D other) {

		// Collect the item when colliding with it
	    if (other.gameObject.tag == "Player") {
	    	
	    	ResourceManager.Inventory.addItemToInventory(idItem);
	 
	        // destroy the item
	        Destroy(this.gameObject);
	    }
	}
}
