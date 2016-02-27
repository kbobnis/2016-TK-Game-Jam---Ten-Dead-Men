using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Mission {

	public readonly string Name;
	public readonly List<List<TileType>> Tiles;

	public Mission(string name, List<List<TileType>> tiles) {
		Name = name;
		Tiles = tiles;
	}
}
