using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[System.Serializable]
public class Tile
{
	public TileType Type;
	public Rotation Rotation;

	public Tile(TileType type, Rotation rotation = global::Rotation.Down)
	{
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
    Empty, Stone, HalfStone, Start, Finish, Spikes, DeadMan, SpikesL, FinishAlt
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
			case 10: return new Tile(TileType.DeadMan, Rotation.Up);
			case 11: return new Tile (TileType.SpikesL, Rotation.Up);
			case 12: return new Tile(TileType.SpikesL, Rotation.Right);
			case 13: return new Tile(TileType.SpikesL, Rotation.Left);
			case 14: return new Tile (TileType.SpikesL, Rotation.Down);
			case 15: return new Tile (TileType.FinishAlt, Rotation.Up);
			
			case 9: //we don't need man tile
				throw new Exception("You can not set man anywhere on the level: " + i);
			default:
				throw new Exception("Tile type not recognized from int: " + i);
		}
    }
}
