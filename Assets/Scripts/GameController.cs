using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public int next_level;
	public int num_levels = 1;
	public int jumps = 30;
	public int max_jumps = 30;
	public float fade_in_duration = 2.0f;
	public float fade_out_duration = 2.0f;
	
	public GameObject fade_plane;
	public PlayerController player_controller; 
	public SleepZSystemController sleep_z_controller;
	public GameObject pause_menu;
	public GameObject pause_button;

	bool ignore_mouse_on_resume = false;
	bool paused = false;
	float pre_pause_fade_value=0;

	[HideInInspector] public bool restarting_level = false;
	[HideInInspector] public IEnumerator coroutine;
	[HideInInspector] public Renderer fade_renderer;
	
	public virtual void Start () {
		next_level = int.Parse(Application.loadedLevelName.Substring(6, Application.loadedLevelName.Length-6));

		fade_plane.SetActive(true);
		fade_renderer = fade_plane.GetComponent<Renderer>();

		coroutine = FadeIn();
		StartCoroutine(coroutine);
	}

	void OnApplicationFocus(bool pauseStatus) {
		if(pauseStatus){
			Pause();
		}
	}

	public virtual void Pause() {
		if (fade_renderer != null) {
			Time.timeScale = 0;
			paused = true;
			pause_button.SetActive (false);
			pause_menu.SetActive (true);
			StopCoroutine (coroutine);
			pre_pause_fade_value = fade_renderer.material.color.a;
			fade_renderer.material.color = new Color (fade_renderer.material.color.r, fade_renderer.material.color.g, fade_renderer.material.color.b, 0.75f);
		}
	}

	public virtual void UnPause() {
		if (fade_renderer != null) {
			Time.timeScale = 1;
			paused = false;
			ignore_mouse_on_resume = true;
			pause_button.SetActive (true);
			pause_menu.SetActive (false);
			StartCoroutine (coroutine);
			fade_renderer.material.color = new Color (fade_renderer.material.color.r, fade_renderer.material.color.g, fade_renderer.material.color.b, pre_pause_fade_value);
		}
	}

	public virtual void GotoNextLevel(){
		jumps = 0;
		next_level = int.Parse (Application.loadedLevelName.Substring (6, Application.loadedLevelName.Length - 6)) + 1;
	}

	public virtual void RestartLevel(){
		jumps = 0;
		player_controller.Disable();
		sleep_z_controller.ParticleDisable();
	}

	public virtual void GotoMainMenu(){
		jumps = 0;
		next_level = num_levels + 1;
		player_controller.Disable();
		sleep_z_controller.ParticleDisable();
	}

	public virtual void Update () {
		if (Input.GetMouseButtonDown(0) && jumps > 0 && !paused) {
			if (!ignore_mouse_on_resume){
				player_controller.Jump();
			}else{
				ignore_mouse_on_resume=false;
			}
		}

		if (jumps >= max_jumps) {
			jumps = max_jumps;
		}
		if (jumps <= 0 && !restarting_level) {
			sleep_z_controller.SendMessage("OutputEnable",0);
			StopCoroutine(coroutine);
			coroutine = FadeOut();
			StartCoroutine(coroutine);
			restarting_level = true;
		}
		if (jumps > 0 && restarting_level) {
			StopCoroutine(coroutine);
			coroutine = FadeIn();
			StartCoroutine(coroutine);
			sleep_z_controller.SendMessage("OutputDisable",0);
			restarting_level = false;
		}
	}

	public virtual IEnumerator FadeIn() {

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

		if (next_level <= num_levels) {
			Application.LoadLevel ("level_" + next_level);
		} else {
			Application.LoadLevel ("title");
		}
		yield return null;
	}
}
