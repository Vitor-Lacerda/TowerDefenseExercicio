using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager instance;

	public GUIManager guiManager;

	public int startingGold = 100;
	public int startingLives = 30;

	int currentGold;
	public int currentLives{ get; protected set; }
	public bool won{ get; protected set; }

	void Awake(){
		if (instance == null) {
			instance = this;
		}
	}

	// Use this for initialization
	void Start () {
		Init ();

	}

	void Init(){
		//guiManager.Init ();
		currentGold = startingGold;
		guiManager.UpdateGoldText (startingGold);
		currentLives = startingLives;
		guiManager.UpdateLivesText (currentLives);
		won = false;
		Pause (false);
	}

	public void Reset(){
		Init ();
	}

	public bool SpendGold(int value){
		if (value <= currentGold) {
			currentGold -= value;
			guiManager.UpdateGoldText (currentGold);
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
		guiManager.UpdateGoldText (currentGold);
	}

	public void LoseLife(){
		currentLives--;
		guiManager.UpdateLivesText (currentLives);
		if (currentLives <= 0) {
			Lose ();
		}
	}
		

	void Lose(){
		Debug.LogWarning ("PERDEU");
		Pause (true);
		guiManager.Lose ();
	}

	public void Win(){
		if (won)
			return;
		won = true;
		Time.timeScale = 0;
		Pause (true);
		guiManager.Win ();
	}

	public void Pause(bool b){
		Time.timeScale = b ? 0 : 1;
	}


}
