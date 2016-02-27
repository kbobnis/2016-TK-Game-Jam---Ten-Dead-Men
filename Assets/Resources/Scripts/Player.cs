using UnityEngine;
using System.Collections;
using System.Linq;

public class Player : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	void Update() {
		if (Input.GetKeyDown(KeyCode.Return)) {
			KillMe();
		}
	}

	public void KillMe() {
		Game.Me.CreateTileAt(transform.localPosition.x, transform.localPosition.y, new Tile(TileType.DeadMan), Game.Me.MissionContainer.transform);
		Game.Me.MissionContainer.GetComponent<MissionComponent>().SpawnPlayer(Game.Me.PlayerPrefab);
		Destroy(gameObject);
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
			Game.Me.ShowMission(++Game.Me.CurrentMissionIndex);
		}

	}
}
