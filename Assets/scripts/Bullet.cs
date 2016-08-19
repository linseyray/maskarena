using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	private Vector2 direction;
	public float bulletSpeed = 15f;
	// Use this for initialization
	void Start () {
		direction = transform.position - transform.parent.position;
		direction.Normalize();
	}
	
	// Update is called once per frame
	void Update () {
		if((transform.position - transform.parent.position).magnitude > 35f) {
			Destroy(this);
		} else {
			transform.Translate((Vector3) direction*bulletSpeed*Time.deltaTime);
		}
	}
}
