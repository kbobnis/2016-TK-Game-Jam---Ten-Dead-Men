using UnityEngine;
using System.Collections;
using SimpleJSON;
using System.Linq;
using System.Collections.Generic;
using System;

public class Game : MonoBehaviour {

	public GameObject TilePrefab;
	public GameObject PlayerPrefab;
	public GameObject SpikeTilePrefab;
	public GameObject StartTilePrefab;
	public GameObject FinishTilePrefab;

	public GameObject MissionContainer;

    public Vector3 StartPoint;

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

		CreateMissionTilesIn(mission, MissionContainer);
		MissionContainer.GetComponent<MissionComponent>().SpawnPlayer(PlayerPrefab);
	}
	
	private void CreateMissionTilesIn(Mission mission, GameObject missionContainer){

		missionContainer.name = mission.Name;
		int i = 0;
		foreach (Tile el in mission.Tiles) {
			if (el.Type != TileType.Empty) {
				GameObject go = CreateTileAt(i % mission.Width, i / mission.Width, el, missionContainer.transform);
			}
			i++;
		}
	}

	private GameObject CreateTileAt(int x, int y, Tile tile, Transform parent) {
	    GameObject tileGO;
		switch (tile.Type) {
			case (TileType.Start):
				tileGO = Instantiate(StartTilePrefab) as GameObject;
				StartPoint = tileGO.transform.position;
				break;
			case (TileType.Finish):
				tileGO = Instantiate(FinishTilePrefab) as GameObject;
				break;
			case (TileType.Spikes):
				tileGO = Instantiate(SpikeTilePrefab) as GameObject;
				break;
			default:
				tileGO = Instantiate(TilePrefab) as GameObject;
				break;
		}
	    
		tileGO.name = "" + x + ", " + y + " " + tile.Type + (tile.Rotation != Rotation.Down?" " + tile.Rotation:"");
		tileGO.GetComponent<TileComponent>().Tile = tile;
		tileGO.transform.parent = parent;
		tileGO.transform.localPosition = new Vector3(x, -y, 0);
		if (!PreparedMaterials.ContainsKey(tile.Type)) {
			throw new Exception("There is no material " + tile + " in prepared materials");
		}
		tileGO.GetComponent<MeshRenderer>().materials = new Material[1] { PreparedMaterials[tile.Type] };
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
