using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
		BasicBullet bb = GetComponent<BasicBullet> ();
		Debug.Log (bb.GetType ().ToString());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
