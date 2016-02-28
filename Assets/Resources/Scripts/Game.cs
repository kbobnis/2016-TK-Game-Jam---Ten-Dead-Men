using UnityEngine;
using System.Collections;
using SimpleJSON;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Xml;

public class Game : MonoBehaviour {

	public GameObject PlayerPrefab;
	public GameObject MissionContainer;
	public TileTypeMaterial[] TileTypeMaterials;
	public static Game Me;
	public PanelLives PanelLives;

	private int CurrentMissionIndex = 0;
	private List<Mission> Missions;
	public static string LoadSceneText = "";
	private Vector2 StartPos;

	[System.Serializable]
	public class TileTypeMaterial {
		public TileType TileType;
		public GameObject PrefabObject;
	}

	public GameObject Message;
	public Vector2 MessageOffset;
	private Vector2 FinishPos;

	public float SpawnerCooldown = 0.5f;
	private bool Spawning = false;
	private float SpawnerTimer = 0;

	public float RestartingCooldown = 2f;
	private bool Restarting = false;
	private float RestartingTimer = 0f;

	void Awake() {
		Me = this;
	}

	void Start () {
		Missions = LoadMissions();

		ShowMission(CurrentMissionIndex);
	}

	void Update() {
		if (!Restarting) {
			if (Input.GetKeyDown (KeyCode.Backspace)) {
				Restart ();
				PlayerPrefab.SetActive (false);
				return;
			}
			if (Spawning) {
				SpawnerTimer += Time.deltaTime;
				if (SpawnerTimer >= SpawnerCooldown) {
					ExecSpawnPlayer ();
					Spawning = false;
				}
			}
		} else {
			RestartingTimer += Time.deltaTime;
			if (RestartingTimer > RestartingCooldown) {
				Message.SetActive (false);
				Spawning = false;
				Restarting = false;
				ShowMission (CurrentMissionIndex);
			}
		}
	}

	public void Fail()
	{
		NoMoreLives();
	}

	public void ShowMission(int index) {

		if (index >= Missions.Count || index < 0) {
			LoadSceneText = "Game Finished, congratulations!";
			Application.LoadLevel("intro");
			return;
		}

		//destroy all previous dirts
		for (int i = 0; i < MissionContainer.transform.childCount; i++) {
			GameObject go = MissionContainer.transform.GetChild(i).gameObject;
			if (go.GetComponent<Player>() == null) {
				Destroy(go);
			}
		}

		MissionContainer.GetComponent<MissionComponent>().Mission = Missions[index];
		CreateMissionTilesIn(Missions[index]);
		
		PanelLives.RestoreLives();
		PlayerManager.Me.RestartTimer();
	}

	private void CreateMissionTilesIn(Mission mission){

		MissionContainer.name = mission.Name;
		Vector2 startPos = new Vector2();
		int i = 0;
		foreach (Tile el in mission.Tiles) {
			if (el.Type != TileType.Empty) {
				GameObject go = CreateTileAt(i % mission.Width, -i / mission.Width, el, MissionContainer.transform);
				if (el.Type == TileType.Start) {
					StartPos = go.transform.localPosition;
				}
				if (el.Type == TileType.Finish /* || el.Type == TileType.FinishAlt */) {
					FinishPos = go.transform.localPosition;
				}
			}
			i++;
		}
		SpawnPlayer();
	}

	public void SpawnPlayer() {
		SpawnerTimer = 0;
		Spawning = true;
		PlayerPrefab.SetActive (false);
	}

	void ExecSpawnPlayer() {

		PlayerPrefab.SetActive(true);
		PlayerPrefab.name = "player";
		PlayerPrefab.transform.parent = MissionContainer.transform;
		PlayerPrefab.GetComponent<Collider2D>().enabled = true;
		PlayerPrefab.transform.localPosition = new Vector3(StartPos.x, StartPos.y, 0);
	}

	private List<Mission> LoadMissions() {
		XmlDocument model = new XmlDocument();
		model.LoadXml(Resources.Load<TextAsset>("model").text);

		XmlNodeList missionsXml = model.GetElementsByTagName("mission");

		List<Mission> missions = new List<Mission>();
		int i = 0;
		foreach (XmlNode missionXml in missionsXml) {
			string path = missionXml.Attributes["path"].Value;
			Debug.Log("loading mission " + path);
			missions.Add(XmlLoader.LoadMission(Resources.Load<TextAsset>(path).text));
		}
		return missions;
	}

	public GameObject CreateTileAt(float x, float y, Tile tile, Transform parent) {

			GameObject tileGO = Instantiate(TileTypeMaterials.First( t=> t.TileType == tile.Type ).PrefabObject) as GameObject;
			tileGO.name = "" + x + ", " + y + " " + tile.Type + (tile.Rotation != Rotation.Down ? " " + tile.Rotation : "");
			tileGO.AddComponent<TileComponent>().Tile = tile;
			tileGO.transform.parent = parent;
			tileGO.transform.localPosition = new Vector3(x, y, 0);
			switch (tile.Rotation) {
				case Rotation.Up:
					tileGO.transform.Rotate(0, 0, 0);
					break;
				case Rotation.Left:
					tileGO.transform.Rotate(0, 0, -90);
					break;
				case Rotation.Right:
					tileGO.transform.Rotate(0, 0, 90);
					break;
				case Rotation.Down:
					tileGO.transform.Rotate(0, 0, 180);
					break;
			}
			return tileGO;
	}

	internal void NoMoreLives() {
		Restart ();
		//LoadSceneText = "No more lives, try again.";
		//Application.LoadLevel("intro");
	}

	internal void ShowNextMission() {
		ShowMission(++CurrentMissionIndex);
	}

	internal void Restart() {
		Restarting = true;
		RestartingTimer = 0;
		Message.SetActive (true);
		Message.transform.position = FinishPos + MessageOffset;
	}
}
