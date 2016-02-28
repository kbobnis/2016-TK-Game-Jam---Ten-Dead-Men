using UnityEngine;
using System.Collections;

public class PanelLives : MonoBehaviour {

	private int Lives = 9;	// One is already on scene
	public GameObject FacePrefab;

	void Start() {

	}

	internal void ReduceLive() {
		if (transform.childCount == 0) {
			Game.Me.NoMoreLives();
		} else {
			Destroy(transform.GetChild(0).gameObject);
		}
	}

	internal void RestoreLives() {
		foreach (Transform child in transform) {
			Destroy (child.gameObject);
		}

		for (int i = 0; i < Lives; i++) {
			GameObject face = Instantiate(FacePrefab) as GameObject;
			face.transform.SetParent(transform);
			face.transform.localPosition = new Vector2 (0, -i);
		}
	}
}
