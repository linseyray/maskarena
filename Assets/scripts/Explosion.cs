using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

	float timepassed = 0;
	CircleCollider2D colli;
	float initialRadius;
	// Use this for initialization
	void Start () {
		colli = this.GetComponent<CircleCollider2D>();
		initialRadius = colli.radius;
	}
	
	// Update is called once per frame
	void Update () {
		colli.radius = Mathf.Lerp(initialRadius, 8*initialRadius, Mathf.Max(timepassed,1f));
		timepassed += Time.deltaTime;
		if(timepassed>2f){
			Destroy(this.gameObject);
		}
	}

	void OnTriggerEnter2D (Collider2D other) {
		if(other.gameObject.tag == "PlayerCollider") {
			Debug.Log("Bomb explosion collision");
			Vector2 impactDirection = (other.transform.position - transform.position).normalized;
			float forceStrength = 25f;
			other.transform.parent.GetComponent<Player>().TakeHit(impactDirection, forceStrength);
		}
	}
}
