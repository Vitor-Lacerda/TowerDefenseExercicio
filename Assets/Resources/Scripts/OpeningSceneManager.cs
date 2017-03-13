using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class OpeningSceneManager : MonoBehaviour {
	public void StartGame(){
		SceneManager.LoadScene ("CenaJogo");
	}
}
