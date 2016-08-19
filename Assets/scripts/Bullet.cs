using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	private Vector2 direction;
	public float bulletSpeed = 15f;
	// Use this for initialization
	void Start () {
		direction = transform.position - transform.parent.position;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 offset = new Vector3(
			direction.x*bulletSpeed*Time.deltaTime,
			direction.y*bulletSpeed*Time.deltaTime,
			0
		);
		transform.Translate(offset);
		Debug.Log(offset);
		if((transform.position - transform.parent.position).magnitude > 35f) {
			Destroy(this);
		}
	}
}
