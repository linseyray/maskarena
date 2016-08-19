using UnityEngine;
using System.Collections;

// This object is only activated if the player is able to hit others (through dash, projectiles,...)
// (e.g. when dashing, when wearing a mask)

public class HitController : MonoBehaviour {

	public Rigidbody2D rigidBody2D; // ours, for impact direction calculation
	public enum HitType { NONE, DASH };
	private HitType currentHitType;

	private AudioSource audioSource;

	public float dashHitStrength = 25;

	// Audio
	public AudioClip hitSound;

	void Awake() {
		audioSource = gameObject.GetComponent<AudioSource>();
	}

	// Use this for initialization
	void Start () {
		currentHitType = HitType.NONE;
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Player") {	
			if (currentHitType == HitType.NONE)
				// No special hit, physics will just push
				return;

			// Hit them!!!!
			Vector2 impactDirection = rigidBody2D.velocity; // the direction we're moving in
			impactDirection.Normalize(); // We only want the direction
			float forceStrength = dashHitStrength; // TODO make dependent on mask type

			// Hit the other player
			other.gameObject.GetComponent<Player>().TakeHit(impactDirection, forceStrength);
			PlayHitSound();

			// Reduce sliding after hit
			rigidBody2D.velocity = new Vector2(0,0);
		} else if (other.gameObject.tag == "bullet") {
			Vector2 impactDirection = other.gameObject.GetComponent<Bullet>().direction.normalized;
			float forceStrength = 2*dashHitStrength;
			this.transform.parent.GetComponent<Player>().TakeHit(impactDirection, forceStrength);
			Destroy(other.gameObject);

		}
	}

	private void PlayHitSound() {
		if (currentHitType == HitType.DASH) {
			audioSource.PlayOneShot(hitSound);
		}
	}

	// Set this when in dash, using a mask,...
	public void SetHitType(HitType newHitType) {
		currentHitType = newHitType;
	}
}
