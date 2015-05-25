using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SleepZSystemController : MonoBehaviour {
	public GameObject particle;
	bool output_enabled = false;

	List<GameObject> z_particles = new List<GameObject>();

	void OutputEnable(){
		output_enabled = true;
		StartCoroutine(Output());
	}

	void OutputDisable(){
		output_enabled = false;
		StopCoroutine(Output());
		foreach (GameObject z_particle in z_particles)
		{
			Destroy(z_particle);
		}
	}
	
	IEnumerator Output() {
		GameObject z_particle;
		while (output_enabled) {
			z_particle = Object.Instantiate(particle,transform.position,transform.rotation) as GameObject;
			z_particles.Add(z_particle);
			yield return new WaitForSeconds(0.25f);
		}
	}
}
