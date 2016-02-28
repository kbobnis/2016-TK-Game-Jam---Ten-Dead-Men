using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

	public Player Player;

	public float LifeTime = 10.0f;
	private float PlayerLifeTime;

	public static PlayerManager Me;

	public bool Stop = false;

	void Awake() {
		Me = this;
	}

	void Start() {	
		PlayerLifeTime = LifeTime;
		Player = GetComponent<Player>();

		foreach (SpriteRenderer child in gameObject.GetComponentsInChildren<SpriteRenderer>()) {
			child.sortingOrder = 5;
		}
	}

	void Update() {
		if(!Stop)
			PlayerLifeTime -= Time.deltaTime;
		TimerDisplay.Me.Refresh (PlayerLifeTime);
		if (PlayerLifeTime <= 0) {
			Kill();
		}
	}

	public void RestartTimer() {
		PlayerLifeTime = LifeTime;
	}

	internal void Kill() {
		Player.KillMe();
	}

}
