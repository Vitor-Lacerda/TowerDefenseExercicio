using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	[HideInInspector]
	public Vector2 gridPosition;

	public bool startOccupied = false;
	public bool occupied{ get; protected set; }
	[SerializeField]
	protected Material[] materials;


	public Tile nextTile;

	public void SetOccupied(bool b){
		occupied = b;
		Renderer renderer = GetComponent<Renderer> ();
		renderer.material = occupied ? materials [1] : materials [0];
	}

	void Start(){
		SetOccupied (startOccupied);
	}



}
