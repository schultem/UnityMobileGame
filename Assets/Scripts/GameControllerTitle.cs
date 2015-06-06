using UnityEngine;
using System.Collections;

public class GameControllerTitle : GameController {

	public override void Start () {
		base.next_level = 1;
		base.fade_plane.SetActive(true);
		base.fade_renderer = fade_plane.GetComponent<Renderer>();
		base.coroutine = base.FadeIn();
		StartCoroutine(base.coroutine);
	}

	public override IEnumerator FadeOut() {
		float startTime = Time.time;
		float fadeValue;
		
		while ((Time.time - startTime) < fade_out_duration) {
			fadeValue = (Time.time - startTime) / fade_out_duration;
			fade_renderer.material.color = new Color(fade_renderer.material.color.r, fade_renderer.material.color.g, fade_renderer.material.color.b, fadeValue);
			yield return null;
		}
		Application.LoadLevel("level_"+base.next_level);
		yield return null;
	}
}
