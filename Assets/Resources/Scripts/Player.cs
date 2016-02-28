using UnityEngine;
using System.Collections;
using System.Linq;

public class Player : MonoBehaviour {

	private bool InsideStart = false;

	public GameObject DyingManPrefab;
	public GameObject SkullPrefab;

	void Update() {
		if (Input.GetKeyDown(KeyCode.P)) {
			if (InsideStart) {
				Debug.Log("you can not do this inside start");
			} else {
				KillMe();
			}
		}
	}

	public void KillMe() {
		Game.Me.PanelLives.ReduceLive();
		Game.Me.CreateTileAt(transform.localPosition.x, transform.localPosition.y, new Tile(TileType.DeadMan, Rotation.Up), Game.Me.MissionContainer.transform);
		//Instantiate (DyingManPrefab, transform.position + new Vector3(-1.25f, -1.25f, 0), Quaternion.identity);
		Instantiate (SkullPrefab, transform.position, Quaternion.identity);
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
		if (otherTile.Type == TileType.Finish || otherTile.Type == TileType.FinishAlt) {
			GetComponent<Collider2D>().enabled = false;
			Game.Me.ShowNextMission();
		}
		if (otherTile.Type == TileType.Start) {
			InsideStart = true;
		}
	}

}
