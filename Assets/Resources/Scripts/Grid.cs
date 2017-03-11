using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour {

	Tile[,] tileMatrix;

	void Awake () {
		tileMatrix = new Tile[10,10];
		foreach (Tile t in GetComponentsInChildren<Tile>()) {
			tileMatrix [(int)t.gridPosition.x, (int)t.gridPosition.y] = t;
			t.SetOccupied (false);
			t.nextTile = null;
		}
	}

	public Tile GetTile(Vector2 pos){
		return tileMatrix [(int)pos.x,(int)pos.y];
	}

	public void SetOccupied(Vector2 pos, bool b){
		Tile t = GetTile (pos);
		t.SetOccupied (b);
	}

	public List<Tile> GetNeighbours(Tile t){
		Vector2 pos = t.gridPosition;
		List<Tile> neighbours = new List<Tile> ();
		Tile neighbour;
		if (pos.x-1 >= 0) {
			neighbour = GetTile (new Vector2 (pos.x - 1, pos.y));
			if (!neighbour.occupied) {
				neighbours.Add (neighbour);
			}
		}

		if (pos.x + 1 <= 9) {
			neighbour = GetTile (new Vector2 (pos.x + 1, pos.y));
			if (!neighbour.occupied) {
				neighbours.Add (neighbour);
			}
		}

		if (pos.y + 1 <= 9) {
			neighbour = GetTile (new Vector2 (pos.x, pos.y + 1));
			if (!neighbour.occupied) {
				neighbours.Add (neighbour);
			}
		}

		if (pos.y - 1 >= 0) {
			neighbour = GetTile (new Vector2 (pos.x, pos.y - 1));
			if (!neighbour.occupied) {
				neighbours.Add (neighbour);
			}
		}



		return neighbours;
	}

	public void ResetTiles(){
		foreach (Tile t in tileMatrix) {
			t.nextTile = null;
		}
	}
}
