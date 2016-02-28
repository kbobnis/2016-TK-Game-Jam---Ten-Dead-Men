using UnityEngine;
using System.Collections;

public class DeadBodySpawnControl : MonoBehaviour {

	public float KillDistance = 1.0f;

	void Update() {
		foreach (GameObject go in GameObject.FindObjectsOfType<GameObject>()) {
			if (Vector2.Distance (go.transform.position, transform.position) < KillDistance) {
				if (go.CompareTag ("BlockDeadBody"))
					Destroy (gameObject);
			}
		}
	}
}
