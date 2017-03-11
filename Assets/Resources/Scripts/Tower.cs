using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Tower : MonoBehaviour {


	void Start () {
	
	}
	
	void Update () {
	
	}

	void OnMouseDown(){
		GameObject.FindObjectOfType<TowerBuilder> ().SelectTower (this.transform);
	}
}
