using UnityEngine;
using System.Collections;
using SimpleJSON;
using System.Linq;
using System.Collections.Generic;
using System;

public class Game : MonoBehaviour {
    
    public GameObject TilePrefab;
    public GameObject SpikeTilePrefab;
    public GameObject StartTilePrefab;
    public GameObject FinishTilePrefab;



	[System.Serializable]
    public class Prefab
    {
		public TileType TileType;
		public GameObject PrefabObject;
    }
    
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

		GameObject missionGO = CreateMissionObject(mission);
		missionGO.transform.parent = MissionContainer.transform;
		missionGO.transform.localPosition = new Vector3();
	}
	
	private GameObject CreateMissionObject(Mission mission){

		GameObject missionGO = new GameObject();
		missionGO.name = mission.Name;

		int y = 0;
		foreach (List<Tile> row in mission.Tiles) {
			int x = 0;
			foreach (Tile el in row) {
				if (el.Type != TileType.Empty) {
					GameObject go = CreateTileAt(x, y, el, missionGO.transform);
					
				}
				x++;
			}
			y++;
		}
		return missionGO;
	}

	private GameObject CreateTileAt(int x, int y, Tile tile, Transform parent) {
	    GameObject tileGO;
	    switch(tile.Type)
	    {
	    case(TileType.Start):
	    {
		tileGO = Instantiate(StartTilePrefab) as GameObject;
		StartPoint = tileGO.transform.position;
	    } break;	    
	    case(TileType.Finish):
	    {
		tileGO = Instantiate(FinishTilePrefab) as GameObject;
	    } break;
	    case(TileType.Spikes):
	    {
		tileGO = Instantiate(SpikeTilePrefab) as GameObject;
	    } break;
	    default:
	    {
		tileGO = Instantiate(TilePrefab) as GameObject;
	    } break;
	    }
	    
		tileGO.transform.parent = parent;
		tileGO.transform.localPosition = new Vector3(-x, -y, 0);
		if (!PreparedMaterials.ContainsKey(tile.Type)) {
			throw new Exception("There is no material " + tile + " in prepared materials");
		}
		tileGO.GetComponent<MeshRenderer>().materials = new Material[1] { PreparedMaterials[tile.Type] };
		switch (tile.Rotation) {
			case Rotation.Up:
				tileGO.transform.Rotate(0, 180, 0);
				break;
			case Rotation.Left:
				tileGO.transform.Rotate(0, 90, 0);
				break;
			case Rotation.Right:
				tileGO.transform.Rotate(0, -90, 0);
				break;
		}
		return tileGO;
	}
}
