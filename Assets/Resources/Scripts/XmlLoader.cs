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

		List<Tile> tiles = new List<Tile>();
		foreach (JSONNode tile in tilesLayerJson["data"].Childs) {
			tiles.Add(TileTypeExtension.FromInt(tile.AsInt));
		}
		return new Mission("Tutorial mission", tiles, width, height);
	}
}


