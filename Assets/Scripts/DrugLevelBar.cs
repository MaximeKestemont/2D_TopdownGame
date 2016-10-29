using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrugLevelBar : MonoBehaviour {


	public float drugLevelThreshold1 = 100f;
	public float currentLevel = 0f;

	public GameObject healthBar;
	public GameObject treshold1;
	public GameObject border;

	private Drug drug;

	private RectTransform borderRect;

	void Start () {
		// TODO should fill the healthBar and treshold automatically (GetCompChildren etc.)
	
		drug = ResourceManager.drugs [ResourceManager.DrugEnum.SPEED_DRUG];
		borderRect = border.GetComponent<RectTransform> ();

		// Set the treshold to the correct position, depending on the value in the drug object
		float leftStartingPosition = borderRect.localPosition.x - borderRect.rect.width * borderRect.localScale.x / 2;
		treshold1.transform.localPosition = new Vector2 (
			leftStartingPosition + (drug.tresholdValue / drug.maxValue) * borderRect.rect.width * borderRect.localScale.x,
			treshold1.transform.localPosition.y
		);
			
	}

	void Update () {
		float currentLevel = drug.GetDrugLevel () / drug.maxValue;
		healthBar.transform.localScale = new Vector3 (currentLevel * borderRect.localScale.x, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
	}
		
}
