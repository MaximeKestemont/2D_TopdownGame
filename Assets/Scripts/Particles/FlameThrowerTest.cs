using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Script not used at the moment. WIP to make it so it push objects that it collides with.
public class FlameThrowerTest : MonoBehaviour {
    public ParticleSystem part;
    public ParticleCollisionEvent[] collisionEvents;
    

    ParticleSystem ps;

    // these lists are used to contain the particles which match
    // the trigger conditions each frame.
    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
    List<ParticleSystem.Particle> exit = new List<ParticleSystem.Particle>();
    List<ParticleSystem.Particle> inside = new List<ParticleSystem.Particle>();

    ParticleSystem.Particle[] collision;

    void Start() {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new ParticleCollisionEvent[16];
        //ParticleSystem.CollisionModule coll = ps.collision;
        //coll.enabled = true;
        //coll.bounce = -2.0f;
    }
    void OnParticleCollision(GameObject other) {
    	Debug.Log("PARTICLE COLLISION");
        
        int numParticles = ps.GetParticles(collision);
        for (int j = 0 ; j < numParticles ; ++j) {
            Debug.Log("Particle : " + collision[j]);    
        }

    	
        int safeLength = part.GetSafeCollisionEventSize();
        Debug.Log("Collision number : " + safeLength);
        if (collisionEvents.Length < safeLength)
            collisionEvents = new ParticleCollisionEvent[safeLength];
        
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);
        Rigidbody rb = other.GetComponent<Rigidbody>();
        int i = 0;
        while (i < numCollisionEvents) {
            if (rb) {
                Vector3 pos = collisionEvents[i].intersection;
                Vector3 force = collisionEvents[i].velocity * 10;
                rb.AddForce(force);
            }
            i++;
        }
        
    }



    void OnEnable()
    {
        ps = GetComponent<ParticleSystem>();
    }

	void OnParticleTrigger()
    {
        if (ps != null) {

            //Collider c = ps.GetCollisionEvent();
        // get the particles which matched the trigger conditions this frame
        int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        int numExit = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);
        int numInside = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Inside, inside);


        //Debug.Log("NUM ENTER : " + numEnter);
        //Debug.Log("NUM EXIT : " + numExit);
        //Debug.Log("NUM INSIDE : " + numInside);
        // iterate through the particles which entered the trigger and make them red
        /*
        for (int i = 0; i < numEnter; i++)
        {
            ParticleSystem.Particle p = enter[i];
            p.startColor = new Color32(255, 0, 0, 255);
            enter[i] = p;
        }

        // iterate through the particles which exited the trigger and make them green
        for (int i = 0; i < numExit; i++)
        {
            ParticleSystem.Particle p = exit[i];
            p.startColor = new Color32(0, 255, 0, 255);
            exit[i] = p;
        }*/

        if ( numInside > 0 || numEnter > 0 ) {
            ResourceManager.MainPlayer.AdjustHealth(-1);
        }

        // re-assign the modified particles back into the particle system
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);
        }
    }
}