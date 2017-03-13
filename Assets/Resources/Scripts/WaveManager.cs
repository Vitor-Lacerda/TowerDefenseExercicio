using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public struct WaveElement
{
	public GameObject enemyPrefab;
	public int amount;
}

[System.Serializable]
public class Wave{
	public WaveElement[] enemies;
}


public class WaveManager : MonoBehaviour {

	[Header("Waves")]
	public Wave[] waves;

	[Header("Managers")]
	public EnemySpawner enemySpawner;
	public GUIManager guiManager;

	[Header("Attributes")]
	public float waveInterval = 10;
	public float firstWaveInterval = 30;
	public float enemyInterval = 0.3f;

	float lastWaveTime;
	bool continuedPlaying;
	int currentWave;

	GameObject[] enemyPrefabs;


	void Start(){
		Init ();
		enemyPrefabs = new GameObject[3];
	}

	void Init(){
		//Compensa a diferenca do primeiro tempo.
		lastWaveTime = Time.time + (firstWaveInterval - waveInterval);
		currentWave = -1;
		continuedPlaying = false;
	}

	public void Reset(){
		Init ();
		StopCoroutine ("SpawnWave");
	}

	void Update(){
		if (Time.time > lastWaveTime + waveInterval) {
			if (currentWave < waves.Length - 1) {
				currentWave++;
				StartCoroutine(SpawnWave (waves [currentWave].enemies));
			} else if(continuedPlaying){
				WaveElement[] waveEnemies = new WaveElement[enemyPrefabs.Length];
				for(int i = 0; i<enemyPrefabs.Length;i++){
					waveEnemies [i].enemyPrefab = enemyPrefabs [Random.Range (0, enemyPrefabs.Length)];
					waveEnemies [i].amount = currentWave;
				}



				StartCoroutine(SpawnWave (waveEnemies));
			}
			lastWaveTime = Time.time;
		}

		if (GameManager.instance.currentLives <= 0) {
			StopCoroutine ("SpawnWave");

		}

		if (currentWave == waves.Length - 1) {
			if (!GameManager.instance.won && enemySpawner.CountLiveEnemies () == 0 && GameManager.instance.currentLives > 0) {
				GameManager.instance.Win ();
			}
		}

		guiManager.UpdateWaveInfoText (lastWaveTime + waveInterval - Time.time, currentWave + 1);
	}

	IEnumerator SpawnWave(WaveElement[] waveEnemies){
		foreach (WaveElement enemy in waveEnemies) {
			for (int i = 0; i < enemy.amount; i++) {
				enemySpawner.SpawnEnemy (enemy.enemyPrefab);
				yield return new WaitForSeconds (enemyInterval);
			}
		}

		yield return null;

	}

	public void SendNextWave(){
		lastWaveTime = -waveInterval;
	}

	public void KeepPlaying(){
		continuedPlaying = true;
		lastWaveTime = Time.time;
		//Recupera os prefabs que tem nas waves
		HashSet<GameObject> hashset = new HashSet<GameObject>();
		foreach (Wave wave in waves) {
			foreach (WaveElement we in wave.enemies) {
				hashset.Add (we.enemyPrefab);
			}
		}

		hashset.CopyTo (enemyPrefabs);

	}



		







}
