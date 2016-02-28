using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PanelLives : MonoBehaviour {

	private int Lives = 9;	// One is already on scene
	public GameObject FacePrefab;
	public AudioClip[] SoundClips;

	private List<AudioSource> AudioSources = new List<AudioSource>();

	void Start() {
		foreach (AudioClip ac in SoundClips) {
			AudioSource aso = gameObject.AddComponent<AudioSource>();
			aso.clip = ac;
			aso.loop = true;
			aso.Play();
			aso.volume = 0;
			AudioSources.Add(aso);
		}
	}


	internal void ReduceLive() {
		if (transform.childCount == 0) {
			Game.Me.NoMoreLives();
		} else {
			Destroy(transform.GetChild(0).gameObject);
			//making loud one more
			foreach(AudioSource aso in AudioSources){
				if (aso.volume == 0) {
					gameObject.AddComponent<Changer>().Change(0, 0.5f, 0.5f, (float final) => {
						aso.volume = final;
					});
					break;
				}
			}
			Camera.main.gameObject.GetComponent<CameraShake>().Shake();
			PlaySingleSound.SpawnSound(Resources.Load<AudioClip>("sounds/dead_dead"));
		}
	}

	internal void RestoreLives() {
		foreach (Transform child in transform) {
			Destroy (child.gameObject);
		}
		foreach (AudioSource aso in AudioSources) {
			aso.volume = 0;
		}
		AudioSources[0].volume = 0.5f;
		

		for (int i = 0; i < Lives; i++) {
			GameObject face = Instantiate(FacePrefab) as GameObject;
			face.transform.SetParent(transform);
			face.transform.localPosition = new Vector2 (0, -i);
		}
	}
}
