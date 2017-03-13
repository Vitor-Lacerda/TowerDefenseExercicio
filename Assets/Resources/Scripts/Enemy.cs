using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

	protected Grid grid;
	protected TowerBuilder towerBuilder;
	protected Animator anim;


	void Start(){
		currentHealth = maxHealth;
		grid = GameObject.FindObjectOfType<Grid> ();
		towerBuilder = GameObject.FindObjectOfType<TowerBuilder> (); 
		anim = GetComponent<Animator> ();
	}

	public virtual void StartAI(Tile t, Tile _endTile){
		currentTile = t;
		targetTile = currentTile.nextTile;
		currentHealth = maxHealth;
		moveSpeed = startMoveSpeed;
		endTile = _endTile;
		UpdateHealthBar ();
		if (anim == null) {
			anim = GetComponent<Animator> ();
		}
		anim.SetBool ("Dead", false);
	}

	protected void Update () {
		if (targetTile != null) {
			Move();
		} else {
			targetTile = currentTile.nextTile;
			if (targetTile == null) {
				targetTile = OpenPath ();
			}
		}

		if (currentTile == endTile) {
			gameObject.SetActive (false);
			GameManager.instance.LoseLife();
		}

		if (Time.time > slowEndTime && moveSpeed != startMoveSpeed && currentHealth >= 0) {
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
		transform.forward = direction;

		if (Mathf.Abs (Vector3.Distance (transform.position, tilePos)) <= 0.1f) {
			currentTile = targetTile;
			targetTile = targetTile.nextTile;
		}

	}

	protected Tile OpenPath(){
		List<Tile> neighbours = grid.GetNeighbours (currentTile,true);
		Tile newTarget = currentTile;
		if (neighbours.Count > 0) {
			foreach (Tile n in neighbours) {
				if (n.occupied) {
					towerBuilder.DestroyTower (n);
					return n;
				}
			}
			newTarget = neighbours [0];
		}

		return newTarget;

	}

	protected void UpdateHealthBar(){

		currentHealth = Mathf.Clamp (currentHealth, 0, maxHealth);

		float ratio = currentHealth / maxHealth;
		Vector3 temp = healthBar.transform.localScale;
		temp.x = ratio;
		healthBar.transform.localScale = temp;
	}

	public void Damage(float value){
		if (value < 0 || currentHealth <= 0) {
			return;
		}

		currentHealth -= value;
		UpdateHealthBar ();
		if (currentHealth <= 0) {
			StartCoroutine (Die ());
		}

	}

	public void Slow(float time){
		slowEndTime = Time.time + time;
		if (moveSpeed == startMoveSpeed) {
			moveSpeed = moveSpeed / 2;
		}
	}

	IEnumerator Die(){
		GameManager.instance.GainGold (goldReward);
		moveSpeed = 0;
		anim.SetBool ("Dead", true);
		yield return new WaitForSeconds (1f);
		gameObject.SetActive (false);
		yield return null;
	}

}
