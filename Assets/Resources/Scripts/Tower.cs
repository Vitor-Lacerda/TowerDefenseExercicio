using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Tower : MonoBehaviour {

	public int price = 20;
	public float fireDelay = 1;
	public float range = 3;
	public float damage = 2;
	public float bulletSpeed = 10;
	public GameObject bulletPrefab;

	Transform target;
	BulletSpawner bulletSpawner;


	float fireTimer;

	void Start () {
		Initialize ();
	}

	void OnEnable(){
		Initialize ();
	}

	public void Initialize(){
		target = null;
		fireTimer = Time.time;
		bulletSpawner = GameObject.FindObjectOfType<BulletSpawner> ();
}
	
	void Update () {
		if (target == null) {
			AcquireTarget ();
		} else {
			if (Time.time >= fireTimer + fireDelay) {
				fireTimer = Time.time;
				bulletSpawner.SpawnBullet (target, transform.position, damage, bulletSpeed, bulletPrefab);
			}

			CheckTarget ();
		}
	}

	void CheckTarget(){
		if (target.gameObject.activeSelf == false) {
			target = null;
		} else {
			CheckRange ();
		}
	}


	void CheckRange(){
		if(Mathf.Abs(Vector3.Distance(transform.position, target.position)) > range){
			target = null;
			fireTimer = Time.time;
		}
	}


	void AcquireTarget(){
		Collider[] enemiesInRange = Physics.OverlapSphere (transform.position, range, LayerMask.GetMask ("Enemy"));

		if (enemiesInRange.Length > 0) {
			target = enemiesInRange [0].transform;
		}


	}

	void OnMouseDown(){
		GameObject.FindObjectOfType<TowerBuilder> ().SelectTower (this.transform);
	}

}
