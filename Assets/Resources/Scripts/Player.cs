using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;

public class Player : MonoBehaviour {

	public float deathTimer = 10;
	public bool counting = false; // Whether player is slowly dyin gor not

	// Use this for initialization
	void Start () {
		counting = true;
	}
	
	void Update() {
		if (counting) {
			deathTimer -= Time.deltaTime;
			DisplayTime ();
		}
		if (Input.GetKeyDown(KeyCode.Return) || deathTimer <= 0) {
			KillMe();
		}
	}

	public void KillMe() {
		Game.Me.CreateTileAt(transform.localPosition.x, transform.localPosition.y, new Tile(TileType.DeadMan), Game.Me.MissionContainer.transform);
		Game.Me.MissionContainer.GetComponent<MissionComponent>().SpawnPlayer(Game.Me.PlayerPrefab);
		Destroy(gameObject);
	}

	private void DisplayTime() {
		
	}

	void OnTriggerEnter(Collider other) {

		Tile otherTile = other.GetComponent<TileComponent>().Tile;
		if (otherTile.Type == TileType.Spikes) {
			//disable collider so it will not trigger twice
			GetComponents<Collider>().ToList().ForEach(t => t.enabled = false);
			KillMe();
		}
		if (otherTile.Type == TileType.Finish) {
			GetComponents<Collider>().ToList().ForEach(t => t.enabled = false);
			Game.Me.PrepareMission ();
			Game.Me.ShowMission(++Game.Me.CurrentMissionIndex);
		}

	}
}
