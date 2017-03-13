using UnityEngine;
using System.Collections;

public class BasicBullet : MonoBehaviour {

	[HideInInspector]
	public float moveSpeed = 10;
	[HideInInspector]
	public float damage;

	protected Transform target;

	public virtual void Initialize(Transform t, float _damage, float _moveSpeed){
		gameObject.SetActive (true);
		target = t;
		damage = _damage;
		moveSpeed = _moveSpeed;
	}

	protected virtual void Update(){
		if (target != null && target.gameObject.activeSelf) {
			Travel ();
		} else {
			gameObject.SetActive (false);
		}
	}

	protected virtual void Travel(){
		Vector3 direction = target.position - transform.position;
		direction.Normalize ();
		transform.position += direction * moveSpeed * Time.deltaTime;
		//transform.LookAt (target);
	}

	protected virtual void OnTriggerEnter(Collider col){
		if (col.gameObject.layer == LayerMask.NameToLayer ("Enemy")) {
			col.GetComponent<Enemy> ().Damage (damage);
			gameObject.SetActive (false);
		}
	}

}
