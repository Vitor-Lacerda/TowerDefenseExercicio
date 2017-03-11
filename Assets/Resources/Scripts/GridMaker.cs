﻿using UnityEngine;
using System.Collections;

public class GridMaker : MonoBehaviour {

	public GameObject tilePrefab;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < 10; i++) {
			for (int j = 0; j < 10; j++) {
				GameObject tile = Instantiate (tilePrefab, new Vector3 (i, 0, j), Quaternion.identity) as GameObject;
				tile.name = i + "," + j;
				tile.GetComponent<Tile> ().gridPosition = new Vector2 (i, j);
				tile.GetComponent<Tile> ().occupied = false;
			}
		}


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
