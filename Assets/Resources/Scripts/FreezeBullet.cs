using UnityEngine;
using System.Collections;

public class FreezeBullet : ExplodingBullet {

	public float slowTime = 1;

	protected override void ExplosionEffect (Enemy enemy)
	{
		base.ExplosionEffect (enemy);
		enemy.Slow (slowTime);
	} 

}
