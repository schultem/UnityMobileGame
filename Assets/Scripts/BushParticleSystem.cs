using UnityEngine;
using System.Collections;

public class BushParticleSystem : MonoBehaviour {
	public GameObject particle;
	public int num_particles = 3;
	
	void OnCollisionEnter2D(Collision2D other) {
		GameObject new_particle;

		if (other.gameObject.tag == "Player") {
			for (int i = 0; i<num_particles; i++){
				new_particle = Object.Instantiate(particle,transform.position+Random.onUnitSphere*GetComponent<CircleCollider2D>().radius,transform.rotation) as GameObject;
				new_particle.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-1f,1f), 1);
				new_particle.GetComponent<Rigidbody2D>().angularVelocity = 180*Random.Range(-1f,1f);
				StartCoroutine(new_particle.GetComponent<ParticleDestroyTimer>().DestroyParticle(2.0f));
			}
		}
	}
}
