using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Mission {

	public readonly string Name;
	public readonly List<List<Tile>> Tiles;

	public Mission(string name, List<List<Tile>> tiles) {
		Name = name;
		Tiles = tiles;
	}
}
