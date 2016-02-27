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

		List<List<Tile>> tiles = new List<List<Tile>>();
		int x = 0;
		List<Tile> row = new List<Tile>();
		foreach (JSONNode tile in tilesLayerJson["data"].Childs) {
			row.Add(TileTypeExtension.FromInt(tile.AsInt));
			x++;
			if (x >= width) {
				tiles.Add(row);
				row = new List<Tile>();
				x = 0;
			}
		}
		return new Mission("Tutorial mission", tiles);
	}
}


