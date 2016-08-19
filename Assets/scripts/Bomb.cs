using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {

	float timepassed = 0;
	// Use this for initialization
	void Start () {
		// TODO: Trigger Animation
	}
	
	// Update is called once per frame
	void Update () {
		timepassed += Time.deltaTime;
		if(timepassed>2f){
			Destroy(this.gameObject);
		}
	}
}
