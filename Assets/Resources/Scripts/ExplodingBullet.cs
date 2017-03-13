using UnityEngine;
using System.Collections;

public class ExplodingBullet : BasicBullet {

	public float explosionRadius = 1;
	public ParticleSystem particle;


	protected override void OnTriggerEnter (Collider col)
	{
		if (col.gameObject.layer == LayerMask.NameToLayer ("Enemy")) {
			Explode ();
		}
	}

	protected void Explode(){
		Collider[] enemies = Physics.OverlapSphere (transform.position, explosionRadius, LayerMask.GetMask ("Enemy"));
		if (enemies.Length > 0) {
			foreach (Collider col in enemies) {
				Enemy enemy = col.GetComponent<Enemy> ();
				ExplosionEffect (enemy);
			}
		}
		particle.transform.parent = transform.parent;
		particle.transform.position = new Vector3 (transform.position.x, 0.1f, transform.position.z);
		particle.transform.localScale = Vector3.one;
		particle.Play ();
		gameObject.SetActive (false);
	}

	protected virtual void ExplosionEffect(Enemy enemy){
		float distance = Vector3.Distance (enemy.transform.position, transform.position);
		enemy.Damage(damage*(1-(distance/explosionRadius)));
	}
}
