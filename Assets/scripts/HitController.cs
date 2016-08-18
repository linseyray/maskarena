using UnityEngine;
using System.Collections;

// This object is only activated if the player is able to hit others (through dash, projectiles,...)
// (e.g. when dashing, when wearing a mask)

public class HitController : MonoBehaviour {

	public Rigidbody2D rigidBody2D; // ours, for impact direction calculation
	public enum HitType { NONE, DASH };
	private HitType currentHitType;

	public float dashHitStrength = 25;

	// Use this for initialization
	void Start () {
		currentHitType = HitType.NONE;
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Player") {
			Vector2 impactDirection = rigidBody2D.velocity; // the direction we're moving in
			impactDirection.Normalize(); // We only want the direction
			float forceStrength = dashHitStrength; // TODO make dependent on mask type

			// Hit the other player
			other.gameObject.GetComponent<Player>().TakeHit(impactDirection, forceStrength);
		}
	}
}
