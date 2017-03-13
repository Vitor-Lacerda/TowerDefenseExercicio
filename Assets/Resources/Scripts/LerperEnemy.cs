using UnityEngine;
using System.Collections;

public class LerperEnemy : Enemy {

	protected override void Move(){
		Vector3 tilePos = new Vector3 (targetTile.transform.position.x, transform.position.y, targetTile.transform.position.z);
		transform.position = Vector3.Lerp (transform.position, tilePos, Time.deltaTime * moveSpeed);
		Vector3 direction = tilePos - transform.position;
		transform.forward = direction;


		if (Mathf.Abs (Vector3.Distance (transform.position, tilePos)) <= 0.1f) {
			currentTile = targetTile;
			targetTile = targetTile.nextTile;
		}
	}
}
