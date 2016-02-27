using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Tile
{
    public readonly TileType Type;
    public readonly Rotation Rotation;

    public Tile(TileType type, Rotation rotation = global::Rotation.Down) {
	Type = type;
	Rotation = rotation;
    }
}

public enum Rotation 
{
    Up, Down, Left, Right
}

public enum TileType
{
    Empty, Stone, HalfStone, Start, Finish, Spikes
}

public static class TileTypeExtension
{

    public static Tile FromInt(this int i)
    {
	switch (i)
	{
	case 0: return new Tile(TileType.Empty);
	case 1: return new Tile(TileType.Stone);
	case 2: return new Tile (TileType.Spikes, Rotation.Up);
	case 3: return new Tile(TileType.Start, Rotation.Up );
	case 4: return new Tile(TileType.Finish, Rotation.Up);
	case 5: return new Tile(TileType.HalfStone);
	case 6: return new Tile(TileType.Spikes, Rotation.Right);
	case 7: return new Tile(TileType.Spikes, Rotation.Left);
	case 8: return new Tile(TileType.Spikes, Rotation.Down);
	default:
	    throw new Exception("Tile type not recognized from int: " + i);
	}
    }
}
