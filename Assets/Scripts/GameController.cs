using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public int jumps = 0;
	public GameObject fade_plane;
	public SleepZSystemController sleep_z_controller;
	public bool restarting_level = false;
	public float fade_in_duration = 3.0f;
	public float fade_out_duration = 3.0f;
	public Renderer fade_renderer;
	public IEnumerator coroutine;

	public virtual void Start () {
		fade_plane.SetActive(true);
		fade_renderer = fade_plane.GetComponent<Renderer>();
		coroutine = FadeIn(true);
		StartCoroutine(coroutine);
	}

	public virtual  void Update () {
		if (jumps <= 0 && !restarting_level) {
			sleep_z_controller.SendMessage("OutputEnable",0);
			StopCoroutine(coroutine);
			coroutine = FadeOut();
			StartCoroutine(coroutine);
			restarting_level = true;
		}
		if (jumps > 0 && restarting_level) {
			StopCoroutine(coroutine);
			coroutine = FadeIn(false);
			StartCoroutine(coroutine);
			sleep_z_controller.SendMessage("OutputDisable",0);
			restarting_level = false;
		}
	}

	public virtual IEnumerator FadeIn(bool start_level) {
		if (start_level) {
			yield return new WaitForSeconds(0.5f);
		}

		float startTime = Time.time;
		float fadeValue;
		float startFade = fade_renderer.material.color.a;
		float altered_fade_in_duration=startFade*fade_in_duration;

		while ((Time.time - startTime) < altered_fade_in_duration) {
			fadeValue = startFade-((Time.time - startTime)/altered_fade_in_duration);

			fade_renderer.material.color = new Color(fade_renderer.material.color.r, fade_renderer.material.color.g, fade_renderer.material.color.b, fadeValue);
			yield return null;
		}
	}

	public virtual IEnumerator FadeOut() {
		float startTime = Time.time;
		float fadeValue;
		
		while ((Time.time - startTime) < fade_out_duration) {
			fadeValue = (Time.time - startTime) / fade_out_duration;
			fade_renderer.material.color = new Color(fade_renderer.material.color.r, fade_renderer.material.color.g, fade_renderer.material.color.b, fadeValue);
			yield return null;
		}
		Application.LoadLevel(Application.loadedLevelName); 
		yield return null;
	}
}
