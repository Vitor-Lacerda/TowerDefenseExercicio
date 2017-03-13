using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public float maxHealth = 10;
	public float startMoveSpeed = 1;
	public int goldReward;

	public Transform healthBar;

	protected float currentHealth;
	protected Tile currentTile;
	protected Tile targetTile;
	protected Tile endTile;
	protected float slowEndTime;
	protected float moveSpeed;


	void Start(){
		currentHealth = maxHealth;
	}

	public virtual void StartAI(Tile t, Tile _endTile){
		currentTile = t;
		targetTile = currentTile.nextTile;
		currentHealth = maxHealth;
		moveSpeed = startMoveSpeed;
		endTile = _endTile;
		UpdateHealthBar ();
	}

	protected void Update () {
		if (targetTile != null) {
			//LerpMove ();
			Move();
		} else {
			targetTile = currentTile.nextTile;
		}

		if (currentTile == endTile) {
			gameObject.SetActive (false);
			//Perder vida
			GameManager.instance.LoseLife();
		}

		if (Time.time > slowEndTime && moveSpeed != startMoveSpeed) {
			moveSpeed = startMoveSpeed;
		}
	}



	protected virtual void Move(){
		Vector3 temp = transform.position;
		Vector3 tilePos = new Vector3 (targetTile.transform.position.x, temp.y, targetTile.transform.position.z);
		Vector3 direction = tilePos - temp;
		direction.Normalize ();

		temp += direction * moveSpeed * Time.deltaTime;

		transform.position = temp;

		if (Mathf.Abs (Vector3.Distance (transform.position, tilePos)) <= 0.1f) {
			currentTile = targetTile;
			targetTile = targetTile.nextTile;
		}

	}

	protected void UpdateHealthBar(){

		currentHealth = Mathf.Clamp (currentHealth, 0, maxHealth);

		float ratio = currentHealth / maxHealth;
		Vector3 temp = healthBar.transform.localScale;
		temp.x = ratio;
		healthBar.transform.localScale = temp;
	}

	public void Damage(float value){
		if (value < 0) {
			return;
		}

		currentHealth -= value;
		UpdateHealthBar ();
		if (currentHealth <= 0) {
			Die ();
		}

	}

	public void Slow(float time){
		slowEndTime = Time.time + time;
		if (moveSpeed == startMoveSpeed) {
			moveSpeed = moveSpeed / 2;
		}
	}

	void Die(){
		GameManager.instance.GainGold (goldReward);
		gameObject.SetActive (false);
	}

}
