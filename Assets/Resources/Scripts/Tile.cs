using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	[HideInInspector]
	public Vector2 gridPosition;

	public bool startOccupied = false;
	public bool occupied{ get; protected set; }
	[SerializeField]
	protected Material[] materials;

	Renderer _renderer;

	public Tower tower;
	public Tile nextTile;

	void Awake(){
		_renderer = GetComponent<Renderer> ();

	}

	public void SetOccupied(bool b, Tower newTower = null){
		tower = b ? newTower : null;
		occupied = b;
		if (_renderer == null) {
			_renderer = GetComponent<Renderer> ();

		}
		_renderer.material = occupied ? materials [1] : materials [0];

	}

	public void Select(bool b){
		if (b) {
			if (_renderer == null) {
				_renderer = GetComponent<Renderer> ();

			}
			_renderer.material = materials [2];
		} else {
			SetOccupied (occupied);
		}
	}


	void Start(){
		SetOccupied (startOccupied);
	}



}
