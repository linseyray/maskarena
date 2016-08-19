using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public Vector2 direction;
	public float bulletSpeed = 15f;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		Vector3 offset = new Vector3(
			direction.x*bulletSpeed*Time.deltaTime,
			direction.y*bulletSpeed*Time.deltaTime,
			0
		);
		transform.Translate(offset);
		if((transform.position).magnitude > 45f) {
			Destroy(this.gameObject);
		}
	}
}
