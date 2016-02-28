using UnityEngine;
using System.Collections;

public delegate void ChangeMethod(float finalA);

public class Changer : MonoBehaviour {

	private float StartTime, DeltaTime, StartV, EndV;
	private ChangeMethod ChangeMethod;
	
	public void Change(float startV, float endV, float time, ChangeMethod changeMethod) {
		Debug.Log( string.Format( "Adding changer from {0} to {1} ", startV, endV));
		EndV = endV;
		StartV = startV;
		DeltaTime = time;
		StartTime = Time.time;
		ChangeMethod = changeMethod;
	}
	

	void Update() {
		if (StartTime != 0) {

			if (Time.time > DeltaTime + StartTime) {
				Destroy(this);
			}
			float percent = (Time.time - StartTime) / DeltaTime;
			float deltaA = (EndV * percent);
			float step = Mathf.SmoothStep(0.0f, 1.0f, percent);
			float finalA = Mathf.Lerp(StartV, deltaA, step);

			ChangeMethod(finalA);
		}
	}

}
