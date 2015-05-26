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
	public int next_level;
	public int num_levels = 2;
	static public float level_start_time;
	static public float level_end_time;

	public virtual void OnGUI()
	{
		if (level_end_time != 0) {
			GUILayout.Label((level_end_time-level_start_time).ToString());
		} else {
			GUILayout.Label((Time.time-level_start_time).ToString());
		}
	}

	public virtual void Start () {
		level_end_time = 0;
		level_start_time = Time.time;

		string level_string = Application.loadedLevelName;
		next_level = 1;
		//next_level = int.Parse(level_string.Substring(5, level_string.Length-1));//current level

		fade_plane.SetActive(true);
		fade_renderer = fade_plane.GetComponent<Renderer>();

		coroutine = FadeIn(true);
		StartCoroutine(coroutine);
	}

	public virtual void Update () {
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

		float fade_start_time = Time.time;
		float fadeValue;
		float startFade = fade_renderer.material.color.a;
		float altered_fade_in_duration=startFade*fade_in_duration;

		while ((Time.time - fade_start_time) < altered_fade_in_duration) {
			fadeValue = startFade-((Time.time - fade_start_time)/altered_fade_in_duration);

			fade_renderer.material.color = new Color(fade_renderer.material.color.r, fade_renderer.material.color.g, fade_renderer.material.color.b, fadeValue);
			yield return null;
		}
	}

	public virtual IEnumerator FadeOut() {
		float fade_start_time = Time.time;
		float fadeValue;

		while ((Time.time - fade_start_time) < fade_out_duration) {
			fadeValue = (Time.time - fade_start_time) / fade_out_duration;
			fade_renderer.material.color = new Color(fade_renderer.material.color.r, fade_renderer.material.color.g, fade_renderer.material.color.b, fadeValue);
			yield return null;
		}
		level_end_time = fade_start_time;
		if (next_level <= num_levels) {
			Application.LoadLevel ("level_" + next_level);
		} else {
			Application.LoadLevel ("title");
		}
		yield return null;
	}
}
