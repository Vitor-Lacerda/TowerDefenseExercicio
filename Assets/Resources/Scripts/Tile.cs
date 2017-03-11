using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	[HideInInspector]
	public Vector2 gridPosition;

	public bool occupied;
	public Material[] materials;
	public Tile nextTile;

	

	Transform seta;

	public void SetOccupied(bool b){
		occupied = b;
		Renderer renderer = GetComponent<Renderer> ();
		renderer.material = occupied ? materials [1] : materials [0];
	}

	//So pra ver o pathfinding

	void Start(){
		seta = transform.GetChild (0);
	}

	void Update(){
		if (nextTile == null) {
			seta.gameObject.SetActive (false);
		} else {
			seta.gameObject.SetActive (true);
			seta.LookAt (nextTile.transform.position);
		}
	}



}
