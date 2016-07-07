using UnityEngine;
using System.Collections;
using System.Linq;

public class Player : MonoBehaviour {

	private bool InsideStart = false;

	public GameObject DyingManPrefab;
	public GameObject SkullPrefab;

	void Update() {
		//if (Input.GetKeyDown(KeyCode.P)) {
		if(Input.GetAxis("die") != 0) {
			if (InsideStart) {
				Debug.Log("you can not do this inside start");
			} else {
				KillMe();
			}
		}
	}

	public void KillMe() {
		if (Game.Me != null) {
			Game.Me.PanelLives.ReduceLive();
			Game.Me.CreateTileAt(transform.localPosition.x, transform.localPosition.y, new Tile(TileType.DeadMan, Rotation.Up), Game.Me.MissionContainer.transform);
			//Instantiate (DyingManPrefab, transform.position + new Vector3(-1.25f, -1.25f, 0), Quaternion.identity);
			Game.Me.SpawnPlayer();
			PlayerManager.Me.RestartTimer();
		}
		Instantiate(SkullPrefab, transform.position, Quaternion.identity);
	}

	void OnTriggerEnter2D(Collider2D other) {

		if (other.GetComponent<TileComponent>() == null) {
			return;
		}

		Tile otherTile = other.GetComponent<TileComponent>().Tile;
		if (otherTile.Type == TileType.Spikes) {
			//disable collider so it will not trigger twice
			GetComponent<Collider2D>().enabled = false;
			KillMe();
		}
		if (otherTile.Type == TileType.Finish || otherTile.Type == TileType.FinishAlt) {
			GetComponent<Collider2D>().enabled = false;
			PlaySingleSound.SpawnSound(Resources.Load<AudioClip>("sounds/win"), 0, 0.4f);
			Game.Me.ShowNextMission();
		}
		if (otherTile.Type == TileType.Start) {
			InsideStart = true;
		}
	}

}
