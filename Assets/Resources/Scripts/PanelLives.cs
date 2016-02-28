using UnityEngine;
using System.Collections;

public class PanelLives : MonoBehaviour {

	private int Lives = 3;
	public GameObject FacePrefab;

	internal void ReduceLive() {
		if (transform.childCount == 0) {
			Game.Me.NoMoreLives();
		} else {
			Destroy(transform.GetChild(0).gameObject);
		}
	}

	internal void RestoreLives() {
		for (int i = 0; i < transform.childCount; i++ ) {
			Destroy(transform.GetChild(0).gameObject);
		}

		for (int i = 0; i < Lives; i++) {
			GameObject face = Instantiate(FacePrefab) as GameObject;
			face.transform.SetParent(transform);
		}
	}
}
