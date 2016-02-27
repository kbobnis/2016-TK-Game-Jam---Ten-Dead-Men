using UnityEngine;
using System.Collections;
using SimpleJSON;
using System.Linq;
using System.Collections.Generic;
using System;

public class Game : MonoBehaviour {

	public GameObject TilePrefab;
	public GameObject MissionContainer;

	[SerializeField]
	public TileTypeMaterial[] TileTypeMaterials;
	[System.Serializable]
	public class TileTypeMaterial {
		public TileType TileType;
		public Material Material;
	}

	private Dictionary<TileType, Material> PreparedMaterials = new Dictionary<TileType, Material>();

	// Use this for initialization
	void Start () {
		Mission mission = XmlLoader.LoadMission(Resources.Load<TextAsset>("maps/tutorial1").text);

		foreach(TileTypeMaterial ttm in TileTypeMaterials){
			PreparedMaterials.Add(ttm.TileType, ttm.Material);
		}

		GameObject missionGO = CreateMissionObject(mission);
		missionGO.transform.parent = MissionContainer.transform;
		missionGO.transform.localPosition = new Vector3();
		GameObject outerTiles = CreateOuterTiles(mission);
		outerTiles.transform.parent = MissionContainer.transform;
		outerTiles.transform.localPosition = new Vector3();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private GameObject CreateOuterTiles(Mission mission) {
		GameObject outerTiles = new GameObject();
		outerTiles.name = mission.Name + " outer tiles";

		int y = mission.Tiles.Count;
		int x = mission.Tiles[0].Count;
		for (int i = -1; i < x + 1; i++) {
			GameObject go = CreateTileAt(i, -1, TileType.Stone);
			go.transform.parent = outerTiles.transform;
			GameObject go2 = CreateTileAt(i, y, TileType.Stone);
			go2.transform.parent = outerTiles.transform;
		}
		for (int i = 0; i < y; i++) {
			GameObject go = CreateTileAt(-1, i, TileType.Stone);
			go.transform.parent = outerTiles.transform;
			GameObject go2 = CreateTileAt(x, i, TileType.Stone);
			go2.transform.parent = outerTiles.transform;
		}
		return outerTiles;
	}

	private GameObject CreateMissionObject(Mission mission){

		GameObject missionGO = new GameObject();
		missionGO.name = mission.Name;

		int y = 0;
		foreach (List<TileType> row in mission.Tiles) {
			int x = 0;
			foreach (TileType el in row) {
				if (el != TileType.Empty) {
					GameObject go = CreateTileAt(x, y, el);
					go.transform.parent = missionGO.transform;
				}
				x++;
			}
			y++;
		}
		return missionGO;
	}

	private GameObject CreateTileAt(int x, int y, TileType tileType) {
		GameObject tile = Instantiate(TilePrefab) as GameObject;
		tile.transform.localPosition = new Vector3(-x, -y, 0);
		if (!PreparedMaterials.ContainsKey(tileType)) {
			throw new Exception("There is no material " + tileType + " in prepared materials");
		}
		tile.GetComponent<MeshRenderer>().materials = new Material[1] { PreparedMaterials[tileType] };
		return tile;
	}
}
