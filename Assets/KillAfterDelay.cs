using UnityEngine;
using System.Collections;

public class KillAfterDelay : MonoBehaviour {

	public float Delay = 2.0f;

	void Update() {
		Delay -= Time.deltaTime;
		if (Delay <= 0) {
			Destroy (gameObject);
		}
	}
}
