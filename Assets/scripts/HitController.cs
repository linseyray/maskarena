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
	public float cactusHitStrength = 400;
	public float hitForceMultiplier = 2.0F;

	private Player player;
	private PlayerController playerController;


	// Audio
	public AudioClip hitSound;

	void Awake() {
		audioSource = gameObject.GetComponent<AudioSource>();
		player = gameObject.transform.parent.gameObject.GetComponent<Player>();
		playerController = gameObject.transform.parent.gameObject.GetComponent<PlayerController>();
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
			if (!playerController.IsDashing() && player.maskType != Mask.TYPES.CACTUS)
				// No special hit, physics will just push
				return;

			// Hit them!!!!
			Vector2 impactDirection = rigidBody2D.velocity; // the direction we're moving in
			impactDirection.Normalize(); // We only want the direction
			impactDirection *= hitForceMultiplier;
			float forceStrength = GetForceStrength();

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
		} else if (other.gameObject.tag == "bomb") {
			Debug.Log("Bomb explosion collision");
			Vector2 impactDirection = (transform.position - other.transform.position).normalized;
			float forceStrength = dashHitStrength;
			this.transform.parent.GetComponent<Player>().TakeHit(impactDirection, forceStrength);
		}
	}

	private void PlayHitSound() {
		if (currentHitType == HitType.DASH) {
			audioSource.PlayOneShot(hitSound);
		}
	}

	private float GetForceStrength() {
		if (player.maskType == Mask.TYPES.CACTUS) {
			Debug.Log("STING!");
			return cactusHitStrength;
		}
		if (playerController.IsDashing())
			return dashHitStrength;
		else return 0;
	}

	// Set this when in dash, using a mask,...
	public void SetHitType(HitType newHitType) {
		currentHitType = newHitType;
	}
}
