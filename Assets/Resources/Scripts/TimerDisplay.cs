using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimerDisplay : MonoBehaviour {

	public static TimerDisplay Me;

	public int Min = 20, Max = 60;
	public float Ratio = 4f;

	void Awake() {
		Me = this;
	}

	public void Refresh(float dt) {
		GetComponent<Text>().text = ((int)dt).ToString();
		GetComponent<Text> ().fontSize = (int) (20f + (10f - (int)dt) * 4);
	}
}
