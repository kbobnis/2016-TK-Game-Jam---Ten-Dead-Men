using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleJSON;
using UnityEngine;

public class XmlLoader {
	
	internal static Mission LoadMission(string missionString) {
		
		JSONNode n = JSONNode.Parse(missionString);
		JSONNode tilesLayerJson = n["layers"].Childs.ElementAt(0);

		int width = tilesLayerJson["width"].AsInt;
		int height = tilesLayerJson["height"].AsInt;

		List<List<TileType>> tiles = new List<List<TileType>>();
		int x = 0;
		List<TileType> row = new List<TileType>();
		foreach (JSONNode tile in tilesLayerJson["data"].Childs) {
			row.Add(TileTypeExtension.FromInt(tile.AsInt));
			x++;
			if (x >= width) {
				tiles.Add(row);
				row = new List<TileType>();
				x = 0;
			}
		}
		return new Mission("Tutorial mission", tiles);
	}
}

public enum TileType {
	Empty, Stone, HalfStone, Start, Finish, SpikesUp
}

public static class TileTypeExtension {

	public static TileType FromInt(this int i){
		switch (i) {
			case 0: return TileType.Empty;
			case 1: return TileType.Stone;
			case 2: return TileType.SpikesUp;
			case 3: return TileType.Start;
			case 4: return TileType.Finish;
			case 5: return TileType.HalfStone;
			default:
				throw new Exception("Tile type not recognized from int: " + i);
		}
	}
}
