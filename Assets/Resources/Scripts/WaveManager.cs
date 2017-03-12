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

	[Header("Attributes")]
	public float waveInterval = 10;
	public float firstWaveInterval = 30;
	public GameObject[] enemyPrefabs;

	float lastWaveTime;
	int currentWave;


	void Start(){
		//Compensa a diferenca do primeiro tempo.
		lastWaveTime = Time.time + (firstWaveInterval - waveInterval);
		currentWave = -1;
	}

	void Update(){
		if (Time.time > lastWaveTime + waveInterval) {
			currentWave++;
			WaveElement[] waveEnemies;
			if (currentWave < waves.Length) {
				waveEnemies = waves [currentWave].enemies;
			} else {

				//TODO: Gerar aleatorio mais direitinho.

				waveEnemies = new WaveElement[3];
				waveEnemies [0].enemyPrefab = enemyPrefabs [0];
				waveEnemies [0].amount = currentWave * 20;

				waveEnemies [1].enemyPrefab = enemyPrefabs [1];
				waveEnemies [1].amount = currentWave * 15;

				waveEnemies [2].enemyPrefab = enemyPrefabs [2];
				waveEnemies [2].amount = currentWave * 10;
			}
			StartCoroutine(SpawnWave (waveEnemies));
			lastWaveTime = Time.time;
		}

	}

	IEnumerator SpawnWave(WaveElement[] waveEnemies){
		foreach (WaveElement enemy in waveEnemies) {
			for (int i = 0; i < enemy.amount; i++) {
				enemySpawner.SpawnEnemy (enemy.enemyPrefab);
				yield return null;
			}
		}

		yield return null;

	}



		







}
