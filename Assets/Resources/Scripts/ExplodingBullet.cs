using UnityEngine;
using System.Collections;

public class ExplodingBullet : BasicBullet {

	public float explosionRadius = 1;
	private Vector3 impactPosition;

	public override void Initialize (Transform t, float damage, float moveSpeed)
	{
		base.Initialize (t, damage, moveSpeed);
		impactPosition = target.position;
	}

	protected override void Travel ()
	{
		Vector3 direction = impactPosition - transform.position;
		direction.Normalize ();
		transform.position += direction * moveSpeed * Time.deltaTime;
		if (Mathf.Abs(Vector3.Distance(transform.position, impactPosition)) <= 0.1f) {
			Explode ();
		}
	}

	protected override void OnTriggerEnter (Collider col)
	{
		if (col.gameObject.layer == LayerMask.NameToLayer ("Enemy")) {
			Explode ();
		}
	}

	private void Explode(){
		Debug.Log ("Explodiu");
		Collider[] enemies = Physics.OverlapSphere (transform.position, explosionRadius, LayerMask.GetMask ("Enemy"));
		if (enemies.Length > 0) {
			foreach (Collider col in enemies) {
				Enemy enemy = col.GetComponent<Enemy> ();
				float distance = Vector3.Distance (enemy.transform.position, transform.position);
				enemy.Damage(damage*(1-(distance/explosionRadius)));
			}
		}
		gameObject.SetActive (false);
	}
}
