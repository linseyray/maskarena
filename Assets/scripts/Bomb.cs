using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {

	float timepassed = 0;
	public GameObject bombBoomPrefab;
	// Use this for initialization
	void Start () {
		// TODO: Trigger Animation
	}
	
	// Update is called once per frame
	void Update () {
		timepassed += Time.deltaTime;
		if(timepassed>2f){
			spawnExplosion();
			Destroy(this.gameObject);
		}
	}

	private void spawnExplosion(){
		GameObject go = GameObject.Instantiate(bombBoomPrefab);
		go.transform.position = transform.position;


	}
}
