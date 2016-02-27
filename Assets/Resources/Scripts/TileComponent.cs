using UnityEngine;
using System.Collections;

public class TileComponent : MonoBehaviour {

	[SerializeField]
	public Tile Tile;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {

		if (Tile.Type == TileType.Spikes && other.GetComponent<PlayerControl>()) {
			//disable collider so it will not trigger twice
			other.GetComponent<Collider>().enabled = false;
			Game.Me.CreateTileAt(other.transform.localPosition.x, other.transform.localPosition.y, new Tile(TileType.DeadMan), Game.Me.MissionContainer.transform);
			Destroy(other.gameObject);
			Game.Me.MissionContainer.GetComponent<MissionComponent>().SpawnPlayer(Game.Me.PlayerPrefab);
			
		}

	}
}
