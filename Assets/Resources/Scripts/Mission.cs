using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[System.Serializable]
public class Mission {

	public string Name;
	public List<Tile> Tiles;
	public int Width, Height;

	public Mission(string name, List<Tile> tiles, int w, int h) {
		Name = name;
		Tiles = tiles;
		Width = w;
		Height = h;
	}
}
