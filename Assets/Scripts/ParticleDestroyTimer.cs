using UnityEngine;
using System.Collections;

public class ParticleDestroyTimer : MonoBehaviour {

	public IEnumerator DestroyParticle(float seconds) {
		
		yield return new WaitForSeconds(seconds*Random.value);

		Destroy(gameObject);
		yield return null;
	}

}
