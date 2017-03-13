using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BuildButton : MonoBehaviour {

	public Button button;
	public Tower towerPrefab;
	// Update is called once per frame
	void Update () {
		button.interactable = GameManager.instance.CheckGold (towerPrefab.price);
	}
}
