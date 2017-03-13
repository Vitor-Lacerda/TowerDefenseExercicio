using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour {

	Tile[,] tileMatrix;
	Tile selectedTile;

	void Awake () {
		tileMatrix = new Tile[15,15];
		foreach (Tile t in GetComponentsInChildren<Tile>(true)) {
			tileMatrix [(int)t.gridPosition.x, (int)t.gridPosition.y] = t;
			t.SetOccupied (false);
			t.nextTile = null;
		}
		selectedTile = null;
	}

	public Tile GetTile(Vector2 pos){
		return tileMatrix [(int)pos.x,(int)pos.y];
	}

	public void SetOccupied(Vector2 pos, bool b, Tower tower = null){
		Tile t = GetTile (pos);
		t.SetOccupied (b, tower);
	}

	public void SelectTile(Vector2 pos, bool b){
		Tile t = GetTile(pos);
		t.Select(b);
		selectedTile = t;

	}

	public void Unselect(){
		if(selectedTile != null){
			selectedTile.Select (false);
		}
		selectedTile = null;
	}

	public void UnselectAll(){
		foreach (Tile t in tileMatrix) {
			t.Select (false);
		}
	}

	public List<Tile> GetNeighbours(Tile t, bool returnOccupied){
		Vector2 pos = t.gridPosition;
		List<Tile> neighbours = new List<Tile> ();
		Tile neighbour;


		if (pos.x + 1 <= 14) {
			neighbour = GetTile (new Vector2 (pos.x + 1, pos.y));
			if ((returnOccupied || !neighbour.occupied) && !neighbour.startOccupied) {
				neighbours.Add (neighbour);
			}
		}


		if (pos.x-1 >= 0) {
			neighbour = GetTile (new Vector2 (pos.x - 1, pos.y));
			if ((returnOccupied || !neighbour.occupied) && !neighbour.startOccupied) {
				neighbours.Add (neighbour);
			}
		}



		if (pos.y + 1 <= 14) {
			neighbour = GetTile (new Vector2 (pos.x, pos.y + 1));
			if ((returnOccupied || !neighbour.occupied) && !neighbour.startOccupied) {
				neighbours.Add (neighbour);
			}
		}

		if (pos.y - 1 >= 0) {
			neighbour = GetTile (new Vector2 (pos.x, pos.y - 1));
			if ((returnOccupied || !neighbour.occupied) && !neighbour.startOccupied) {
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
