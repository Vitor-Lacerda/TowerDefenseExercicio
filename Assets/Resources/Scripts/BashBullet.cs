using UnityEngine;
using System.Collections;

public class BashBullet : ExplodingBullet {

	protected override void Travel ()
	{
		Explode ();
	}

	protected override void ExplosionEffect (Enemy enemy)
	{
		enemy.Damage (damage);
	}
}
