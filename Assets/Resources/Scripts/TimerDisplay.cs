using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimerDisplay : MonoBehaviour {

	public static TimerDisplay Me;

	void Awake() {
		Me = this;
	}

	public void Refresh(float dt) {
		GetComponent<Text>().text = ((int)dt).ToString();
	}
}
