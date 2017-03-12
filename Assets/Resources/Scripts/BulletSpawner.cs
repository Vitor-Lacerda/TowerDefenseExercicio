using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class BulletSpawner : MonoBehaviour {

	List<BasicBullet> bullets;
	//public GameObject bulletPrefab;

	// Use this for initialization
	void Start () {
		bullets = new List<BasicBullet> ();
		foreach (BasicBullet bb in GetComponentsInChildren<BasicBullet>(true)) {
			bullets.Add (bb);
		}
	}
	
	public void SpawnBullet(Transform target, Vector3 initialPosition, float bulletDamage, float moveSpeed, GameObject prefab)
	{
		BasicBullet newBullet = FindOrCreateBullet (prefab);
		newBullet.transform.position = initialPosition;
		newBullet.Initialize (target, bulletDamage, moveSpeed);
	}

	BasicBullet FindOrCreateBullet(GameObject prefab){
		foreach (BasicBullet bb in bullets) {
			if (bb.GetComponent<BasicBullet> ().GetType() == prefab.GetComponent<BasicBullet>().GetType()) {
				if (bb.gameObject.activeSelf == false) {
					return bb;
				}
			}
		}

		return CreateBullet(prefab);
	}

	BasicBullet CreateBullet(GameObject bulletPrefab){
		GameObject b = Instantiate (bulletPrefab) as GameObject;
		BasicBullet bb =  b.GetComponent<BasicBullet> ();
		b.transform.parent = this.transform;
		b.SetActive (false);
		bullets.Add (bb);
		return bb;
	}
}
