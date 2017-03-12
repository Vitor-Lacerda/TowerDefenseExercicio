using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager instance;

	public int startingGold = 100;
	public int startingLives = 30;
	public Text goldText;

	int currentGold;
	int currentLives;

	// Use this for initialization
	void Start () {
		if (instance == null) {
			instance = this;
		}
		currentGold = startingGold;
		goldText.text = currentGold.ToString ();

		currentLives = startingLives;
	}

	public bool SpendGold(int value){
		if (value <= currentGold) {
			currentGold -= value;
			goldText.text = currentGold.ToString();
			return true;
		}
		Debug.LogWarning ("Falta ouro");
		return false;
	}

	public void GainGold(int value){
		if (value < 0) {
			return;
		}
		currentGold += value;
		goldText.text = currentGold.ToString();

	}

	public void LoseLife(){
		currentLives--;
		if (currentLives <= 0) {
			Lose ();
		}
	}

	void Lose(){
		Debug.LogWarning ("PERDEU");
		Time.timeScale = 0;
	}


}
