using UnityEngine;
using System.Collections;


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
	public GameObject[] enemyPrefabs;
	public float enemyInterval = 0.3f;

	float lastWaveTime;
	bool continuedPlaying;
	int currentWave;


	void Start(){
		Init ();
	}

	void Init(){
		//Compensa a diferenca do primeiro tempo.
		lastWaveTime = Time.time + (firstWaveInterval - waveInterval);
		currentWave = -1;
		continuedPlaying = false;
	}

	public void Reset(){
		Init ();
	}

	void Update(){
		if (Time.time > lastWaveTime + waveInterval) {
			currentWave++;
			if (currentWave < waves.Length) {
				StartCoroutine(SpawnWave (waves [currentWave].enemies));
			} else if(continuedPlaying){
				//TODO: Gerar aleatorio mais direitinho.

				WaveElement[] waveEnemies;


				waveEnemies = new WaveElement[3];
				waveEnemies [0].enemyPrefab = enemyPrefabs [0];
				waveEnemies [0].amount = currentWave * 20;

				waveEnemies [1].enemyPrefab = enemyPrefabs [1];
				waveEnemies [1].amount = currentWave * 15;

				waveEnemies [2].enemyPrefab = enemyPrefabs [2];
				waveEnemies [2].amount = currentWave * 10;
				StartCoroutine(SpawnWave (waveEnemies));
			}
			lastWaveTime = Time.time;
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

	public void KeepPlaying(){
		continuedPlaying = true;
		lastWaveTime = Time.time;
	}



		







}
