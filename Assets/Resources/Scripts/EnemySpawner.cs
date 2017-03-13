using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class EnemySpawner : MonoBehaviour {

	public Tile[] startTiles;
	public Tile endTile;
	public GameObject[] enemyPrefabs;

	List<Enemy> enemyList;


	void Start () {
		enemyList = new List<Enemy> ();
		foreach (Enemy e in gameObject.GetComponentsInChildren<Enemy>(true)) {
			enemyList.Add (e);
		}
	}

	public void Reset(){
		foreach (Enemy e in enemyList) {
			e.gameObject.SetActive (false);
		}
	}
		
	void Update () {
		if (Input.GetButtonDown ("Jump")) {
			SpawnEnemy (enemyPrefabs[Random.Range(0, enemyPrefabs.Length)]);
		}
	}

	public int CountLiveEnemies(){
		return enemyList.Where (e => e.gameObject.activeSelf).ToList ().Count;
	}

	public void SpawnEnemy(GameObject prefab){
		Enemy newEnemy = FindOrCreateEnemy (prefab);
		Tile start = startTiles [Random.Range (0, startTiles.Length)];
		newEnemy.transform.position = start.transform.position;
		newEnemy.gameObject.SetActive (true);
		newEnemy.StartAI (start, endTile);
	}

	Enemy FindOrCreateEnemy(GameObject prefab){
		
		foreach (Enemy e in enemyList) {
			if (e.name == prefab.name) {
				if (e.gameObject.activeSelf == false) {
					return e;
				}
			}
		}

		return CreateEnemy (prefab);
	}

	Enemy CreateEnemy(GameObject prefab){
		GameObject e = Instantiate (prefab) as GameObject;
		Enemy en =  e.GetComponent<Enemy> ();
		e.transform.parent = this.transform;
		e.name = prefab.name;
		e.SetActive (false);
		enemyList.Add (en);
		return en;
	}
}
