using UnityEngine;
using System.Collections;

public class ZParticleController : MonoBehaviour {
	public float fade_duration = 1.0f;
	Renderer fade_renderer;

	void Start () {
		fade_renderer = GetComponent<Renderer>();
		StartCoroutine(Fade());
	}

	IEnumerator Fade() {
		float startTime = Time.time;
		float fadeValue;
		
		while ((Time.time - startTime) < fade_duration) {
			fadeValue = (Time.time - startTime) / fade_duration;
			fade_renderer.material.color = new Color(fade_renderer.material.color.r, fade_renderer.material.color.g, fade_renderer.material.color.b, fadeValue);
			transform.localScale = new Vector3 (fadeValue+0.1f,fadeValue+0.1f,fadeValue);
			transform.position = new Vector3(transform.position.x,transform.position.y+0.01f);
			yield return new WaitForSeconds(0.03f);
		}
		startTime = Time.time;
		while ((Time.time - startTime) < fade_duration) {
			fadeValue = 1-((Time.time - startTime) / fade_duration);
			fade_renderer.material.color = new Color(fade_renderer.material.color.r, fade_renderer.material.color.g, fade_renderer.material.color.b, fadeValue);
			transform.localScale = new Vector3 (fadeValue,fadeValue,fadeValue);
			transform.position = new Vector3(transform.position.x,transform.position.y+0.01f);
			yield return new WaitForSeconds(0.03f);
		}
		yield return null;
	}
}
