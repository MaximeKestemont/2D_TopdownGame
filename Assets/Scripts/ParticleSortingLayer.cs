using UnityEngine;
using System.Collections;

// This script is used to apply a sorting layer to the particle, as it is not possible to do so in the editor
public class ParticleSortingLayer : MonoBehaviour {

    void Start () {
        // Set the sorting layer of the particle system.
        GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = "Particles";
        GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingOrder = 2;
    }
}
