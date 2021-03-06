﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedDrug : Drug {

	public float speedIncrease = 1.5f;

	/*
	========================
	PlayAction
	========================
	*/
	public override void PlayAction () {
		ResourceManager.MainPlayer.AdjustSpeed(speedIncrease, true, 5.0f);
		this.AdjustDrugLevel (10);
	}

	/*
	========================
	PlayEffect
	========================
	*/
	public override void PlayEffect () {
		if (this.drugLevel > this.tresholdValue) {
			foreach (HallucinatedObject obj in ResourceManager.HallucinatedObjects) {
				if (!obj.GetVisibility ()) {
					obj.SetVisibility (true);
				}
				if ( obj.GetOpacity() < 1.0f) {
					float opacity = obj.GetOpacity() + Time.deltaTime / ResourceManager.fadingDuration;
					obj.GetRenderer.material.color = new Color (1.0f, 1.0f, 1.0f, opacity);
				}
			}

		} else {
			foreach (HallucinatedObject obj in ResourceManager.HallucinatedObjects) {
				if ( obj.GetOpacity() > 0.0f) {
					float opacity = obj.GetOpacity() - Time.deltaTime / ResourceManager.fadingDuration;
					obj.GetRenderer.material.color = new Color (1.0f, 1.0f, 1.0f, Mathf.Max(0.0f, opacity));
					if (opacity < 0.1f) {
						obj.SetVisibility (false);
					}
				}
			}			
		}
	}


}
