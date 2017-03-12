using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pathfinder : MonoBehaviour {

	public Grid grid;
	public Tile exitTile;

	void Start(){
		FloodFill ();
	}

	//FloodFill, como no watershed, so que interpretado "ao contrario"
	//O tile onde os inimigos saem da tela e o fundo da bacia
	//"Visiting" na verdade e o tile anterior a "neighbour", mas como eu estou comecando do final e indo pra cada tile,
	//ele acaba sendo o tile que pra onde eu tenho que ir se estiver em "neighbour"

	public void FloodFill(){
		//So pra deixar todos "nao visitados"
		grid.ResetTiles ();

		//Tile exitTile = grid.GetTile (exitPosition);
		Queue<Tile> border = new Queue<Tile> ();
		border.Enqueue (exitTile);

		exitTile.nextTile = null;

		while (border.Count > 0) {
			Tile visiting = border.Dequeue ();
			foreach (Tile neighbour in grid.GetNeighbours(visiting)) {
				if (neighbour.nextTile == null) {
					border.Enqueue (neighbour);
					neighbour.nextTile = visiting;
				}
			}
		}


	}
}
