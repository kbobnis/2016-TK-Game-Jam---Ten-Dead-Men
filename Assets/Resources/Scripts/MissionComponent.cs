using UnityEngine;
using System.Collections;

public class MissionComponent : MonoBehaviour {

	public Mission Mission;

	[SerializeField]
	private int lives = 10;

	// Use this for initialization	
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	internal void SpawnPlayer(GameObject playerPrefab) {
		lives--;
		DisplayLives ();
		if (lives < 0) {
			Game.Me.Fail ();
			return;
		}
		GameObject playerGO = Instantiate(playerPrefab) as GameObject;
		playerGO.SetActive(true);
		playerGO.name = "player";
		playerGO.transform.parent = transform;

		GameObject startTile = null;
		for (int i = 0; i < transform.childCount; i++) {
			if (transform.GetChild(i).GetComponent<TileComponent>().Tile.Type == TileType.Start) {
				startTile = transform.GetChild(i).gameObject;
				break;
			}
		}
		Vector3 pos = startTile.transform.localPosition;
		playerGO.transform.localPosition = new Vector3(pos.x, pos.y, -0.25f);
	}
		
	public void Reset() {
		lives = 10;
	}

	private void DisplayLives() {
		
	}
}
