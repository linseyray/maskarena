using UnityEngine;
using System.Collections;

// This object is only activated if the player is able to hit others (through dash, projectiles,...)
// (e.g. when dashing, when wearing a mask)

public class HitDetector : MonoBehaviour {

	public enum HitType { NONE, DASH };
	private HitType currentHitType;

	// Use this for initialization
	void Start () {
		currentHitType = HitType.NONE;
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Player") {
			Vector2 impactDirection;
			float forceStrength;
			// Hit the other player
			other.gameObject.GetComponent<Player>().TakeHit();
		}
	}
}
