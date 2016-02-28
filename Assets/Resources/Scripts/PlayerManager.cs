using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

	public Player Player;

	public float LifeTime = 10.0f;
	private float PlayerLifeTime;

	public static PlayerManager Me;

	void Start() {
		Me = this;
		PlayerLifeTime = LifeTime;
		Player = GetComponent<Player>();
	}

	void Update() {
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
