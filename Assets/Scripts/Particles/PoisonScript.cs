using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class PoisonScript : MonoBehaviour {

   	ParticleSystem ps;

    // these lists are used to contain the particles which match
    // the trigger conditions each frame.
    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
    List<ParticleSystem.Particle> exit = new List<ParticleSystem.Particle>();
    List<ParticleSystem.Particle> inside = new List<ParticleSystem.Particle>();

    void OnEnable()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void OnParticleTrigger()
    {
    	if (ps != null) {

        	// get the particles which matched the trigger conditions this frame
        	int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        	int numExit = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);
        	int numInside = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Inside, inside);
	
        	//Debug.Log("NUM ENTER : " + numEnter);
        	//Debug.Log("NUM EXIT : " + numExit);
        	//Debug.Log("NUM INSIDE : " + numInside);
        	// iterate through the particles which entered the trigger and make them red
	
        	if ( numInside > 0 || numEnter > 0 ) {
        		ResourceManager.MainPlayer.InflictDamage(-1, false);		// poison does not care about invicible window
        	}
	
        	// re-assign the modified particles back into the particle system
        	ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        	ps.SetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);
    	}
    }

}
