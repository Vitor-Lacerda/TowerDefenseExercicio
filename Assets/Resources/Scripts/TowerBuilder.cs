using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TowerBuilder : MonoBehaviour {

	public Button destroyButton;

	//public GameObject towerPrefab; 
	public Pathfinder pathfinder;
	public Grid grid;
	Transform currentTower;
	bool building;

	public void Reset(){
		foreach (Tower t in GetComponentsInChildren<Tower>()) {
			DestroyTower (t.transform);
		}
		currentTower = null;
	}

	void Update(){
		if (building && currentTower != null) {
			bool overTile = PlaceTower ();
			if (Input.GetMouseButtonDown (0)) {
				if (overTile) {
					BuildTower ();
				} else {
					StopBuilding (true);
				}
			}	
		}
	}


	bool PlaceTower(){
		RaycastHit hit;
		Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 100, LayerMask.GetMask ("Tile"));
		if (hit.collider != null) {
			currentTower.transform.position = hit.collider.transform.position;
			currentTower.gameObject.SetActive (true);
			return true;
		} else {
			currentTower.gameObject.SetActive (false);
			return false;
		}


	}

	void BuildTower(){
		Vector2 towerGridPos = new Vector2 (currentTower.transform.position.x, currentTower.transform.position.z);
		Tile t = grid.GetTile (towerGridPos);
		if (!t.occupied) {
			grid.SetOccupied (towerGridPos, true, currentTower.GetComponent<Tower>());
			currentTower.GetComponent<Collider> ().enabled = true;
			currentTower.parent = this.transform;
			pathfinder.FloodFill ();
			StopBuilding (false);
		} else {
			StopBuilding (true);
		}
	}

	public void SelectTower(Transform t){
		destroyButton.gameObject.SetActive (true);
		currentTower = t;
	}
		
	public void DestroyTower(){
		if (currentTower != null) {
			grid.SetOccupied (new Vector2 (currentTower.transform.position.x, currentTower.transform.position.z), false);
			Destroy (currentTower.gameObject);
			pathfinder.FloodFill ();
		}
		destroyButton.gameObject.SetActive (false);
	}

	public void DestroyTower(Transform tower){
		Transform prevCurrentTower = currentTower;
		currentTower = tower;
		DestroyTower ();
		currentTower = prevCurrentTower;
	}

	public void DestroyTower(Tile tile){
		if (!tile.occupied || tile.tower == null) {
			return;
		}

		DestroyTower (tile.tower.transform);

		/*
		foreach (Tower t in GetComponentsInChildren<Tower>()) {
			Vector2 towerGridPos = new Vector2 (t.transform.position.x, t.transform.position.z);
			if (towerGridPos == tile.gridPosition) {
				DestroyTower (t.transform);
				return;
			}
		}
		*/


	}

	public void StartBuilding(GameObject towerPrefab){

		Tower t = towerPrefab.GetComponent<Tower> ();
		if (t != null) {
			if (GameManager.instance.SpendGold (t.price)) {
				GameObject newTower = Instantiate (towerPrefab);
				currentTower = newTower.transform;
				currentTower.gameObject.SetActive (false);
				currentTower.GetComponent<Collider> ().enabled = false;
				building = true;
				destroyButton.gameObject.SetActive (false);
			}
		}
	}

	public void StopBuilding(bool destroy){
		GameObject t = currentTower.gameObject;
		currentTower = null;
		building = false;
		if (destroy) {
			Destroy (t);
		}
	}
}
