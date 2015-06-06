using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SleepZSystemController : MonoBehaviour {
	public GameObject particle;
	public IEnumerator coroutine;
	List<GameObject> z_particles = new List<GameObject>();

	void OutputEnable(){
		coroutine = Output();
		StartCoroutine(coroutine);
	}

	void OutputDisable(){
		StopCoroutine(coroutine);
		foreach (GameObject z_particle in z_particles)
		{
			Destroy(z_particle);
		}
	}

	public void ParticleDisable(){
		particle.SetActive (false);
	}

	IEnumerator Output() {
		GameObject z_particle;
		while (true) {
			z_particle = Object.Instantiate(particle,transform.position,transform.rotation) as GameObject;
			z_particles.Add(z_particle);
			yield return new WaitForSeconds(0.45f);
		}
	}
}
