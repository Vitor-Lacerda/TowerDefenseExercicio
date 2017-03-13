using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public class GUIManager : MonoBehaviour {

	[SerializeField]
	private Text goldText;
	[SerializeField]
	private Text livesText;
	Color livesTextStartColor;


	[Header("Wave Info")]
	[SerializeField]
	private Text countDownText;
	[SerializeField]
	private Text currentWaveText;

	[Header("Modals")]
	[SerializeField]
	private GameObject victoryModal;
	[SerializeField]
	private GameObject defeatModal;


	void Start(){
		Init ();
	}

	public void Init(){
		livesTextStartColor = livesText.color;
	}

	public  void UpdateGoldText(int value){
		goldText.text = value.ToString("D4");
	}

	public void UpdateLivesText(int value){
		livesText.text = value.ToString ("D2");
		if (value <= 5) {
			livesText.color = Color.red;
		} else {
			livesText.color = livesTextStartColor;
		}
	}

	public void UpdateWaveInfoText(float timerValue, int currentWave){
		countDownText.text = timerValue.ToString ("F2");
		currentWaveText.text = currentWave.ToString ();
	}

	public void Win(){
		victoryModal.SetActive (true);
	}

	public void Lose(){
		defeatModal.SetActive (true);
	}


}
