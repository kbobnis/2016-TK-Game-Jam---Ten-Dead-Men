using UnityEngine;
using System.Collections;
using SimpleJSON;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Xml;

public class Game : MonoBehaviour {

	public GameObject Prefabs;
	public GameObject PlayerPrefab;
	public GameObject MissionContainer;
	public TileTypeMaterial[] TileTypeMaterials;
	private Dictionary<TileType, GameObject> PreparedPrefabs = new Dictionary<TileType, GameObject> ();
	public static Game Me;


	public int CurrentMissionIndex = 0;
	private List<Mission> Missions;

	[System.Serializable]
	public class TileTypeMaterial {
		public TileType TileType;
		public GameObject PrefabObject;
	}

	void Start () {
		Prefabs.SetActive(false);
		Me = this;

		Missions = LoadMissions();

		foreach(TileTypeMaterial ttm in TileTypeMaterials){
			PreparedPrefabs.Add(ttm.TileType, ttm.PrefabObject);
		}

		ShowMission(CurrentMissionIndex);
	}

	public void Fail()
	{
		PrepareMission ();
		ShowMission (CurrentMissionIndex);
	}

	public void ShowMission(int index) {
		Debug.Log("Show mission numer: " + index);

		if (index >= Missions.Count ) {
			Application.LoadLevel("intro");
			return;
		}

		//destroy all previous dirts
		for (int i = 0; i < MissionContainer.transform.childCount; i++) {
			Destroy(MissionContainer.transform.GetChild(i).gameObject);
		}

		CreateMissionTilesIn(Missions[index], MissionContainer);
		MissionContainer.GetComponent<MissionComponent>().SpawnPlayer(PlayerPrefab);
	}

	public void PrepareMission()
	{
		MissionContainer.GetComponent<MissionComponent> ().Reset ();
	}
	
	private void CreateMissionTilesIn(Mission mission, GameObject missionContainer){

		missionContainer.name = mission.Name;
		int i = 0;
		foreach (Tile el in mission.Tiles) {
			if (el.Type != TileType.Empty) {
				GameObject go = CreateTileAt(i % mission.Width, -i / mission.Width, el, missionContainer.transform);
			}
			i++;
		}
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
		
		GameObject tileGO = Instantiate (PreparedPrefabs [tile.Type]) as GameObject;
		tileGO.name = "" + x + ", " + y + " " + tile.Type + (tile.Rotation != Rotation.Down?" " + tile.Rotation:"");
		tileGO.GetComponent<TileComponent>().Tile = tile;
		tileGO.transform.parent = parent;
		tileGO.transform.localPosition = new Vector3(x, y, 0);
		switch (tile.Rotation) {
			case Rotation.Up:
				tileGO.transform.Rotate(0, 0, 0);
				break;
			case Rotation.Left:
				tileGO.transform.Rotate(0, 90, 0);
				break;
			case Rotation.Right:
				tileGO.transform.Rotate(0, -90, 0);
				break;
			case Rotation.Down:
				tileGO.transform.Rotate(0, 180, 0);
				break;
		}
		return tileGO;
	}
}
