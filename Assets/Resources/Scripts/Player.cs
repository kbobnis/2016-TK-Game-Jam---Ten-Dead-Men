using UnityEngine;
using System.Collections;
using System.Linq;

public class Player : MonoBehaviour {

	private bool InsideStart = false;

	void Update() {
		if (Input.GetKeyDown(KeyCode.Return) ) {
			if (InsideStart) {
				Debug.Log("you can not do this inside start");
			} else {
				KillMe();
			}
		}
	}

	public void KillMe() {
		Game.Me.PanelLives.ReduceLive();
		Game.Me.CreateTileAt(transform.localPosition.x, transform.localPosition.y, new Tile(TileType.DeadMan), Game.Me.MissionContainer.transform);
		Game.Me.SpawnPlayer();
		PlayerManager.Me.RestartTimer();
	}

	void OnTriggerEnter2D(Collider2D other) {

		Debug.Log(string.Format("{0} triggered with {1} ", gameObject.name, other.gameObject.name));

		Tile otherTile = other.GetComponent<TileComponent>().Tile;
		if (otherTile.Type == TileType.Spikes) {
			//disable collider so it will not trigger twice
			GetComponent<Collider2D>().enabled = false;
			KillMe();
		}
		if (otherTile.Type == TileType.Finish) {
			GetComponent<Collider2D>().enabled = false;
			Game.Me.ShowNextMission();
		}
		if (otherTile.Type == TileType.Start) {
			InsideStart = true;
		}
	}

}
