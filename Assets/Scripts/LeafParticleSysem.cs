using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LeafParticleSysem : MonoBehaviour {
	public GameObject particle;
	public int num_particles = 3;

	void OnTriggerExit2D(Collider2D other) {
		GameObject new_particle;

		if (other.CompareTag ("Player")) {
	        for (int i = 0; i<num_particles; i++){
	        	new_particle = Object.Instantiate(particle,other.transform.position,Random.rotation) as GameObject;
	        	new_particle.GetComponent<Rigidbody2D>().velocity = other.attachedRigidbody.velocity*Random.value*0.8f;
				new_particle.GetComponent<Rigidbody2D>().angularVelocity = 180*Random.Range(-1f,1f);
	        	StartCoroutine(new_particle.GetComponent<ParticleDestroyTimer>().DestroyParticle(2.0f));
	        }
		}
	}


}
