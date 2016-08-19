using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

	public enum PlayerState { IDLE = 0, RUNNING = 1, DASHING = 2, FALLING = 3, DEAD = 99 };
	private PlayerState currentState = PlayerState.IDLE;

	private string playerName;
	private int playerNumber;
	public List<Color> colors;

	private int startNrLives;
	private int nrLives; // Players

	private Animator animator;
	public SpriteRenderer bodyRenderer; //body
	public SpriteRenderer maskRenderer; //body

	private PlayerController playerController;
	private Rigidbody2D rigidBody2D;

	private bool isFalling = false;
	public float fallingTime; // How long the player should fall before dying
	private float timeTillDeath;
	public float fallSpeed = 5;

	public GameObject maskGameobject;

	public float defaultMaskTimeout = 5.0f;
	private bool hasMask = false;
	private float maskTimeLeft;
	public Mask.TYPES maskType;

	public Sprite[] masks;
	public Sprite emptymask;

	public GameObject shadow;

	public GameObject hitCollider;
	public GameObject holeCollider;

	public float sa_cooldownDuration = 2f;
	private float specialAbilityCooldown = 0;
	private bool specialAbilityReady = true;
	public GameObject bulletPrefab;
	public GameObject bombPrefab;
	public GameObject EggPrefab;

	private LivesController livesController;

	public GameObject[] winnerScreens;
	private GameObject winnerScreen;

	void Awake() {
		playerController = gameObject.GetComponent<PlayerController>();
		animator = gameObject.GetComponentInChildren<Animator>();
		rigidBody2D = gameObject.GetComponent<Rigidbody2D>();
	}

	void Start () {
	}
		
	public void SetMaxNrLives(int lives) {
		startNrLives = lives;
		nrLives = lives;
	}

	public void SetLivesController(LivesController c) {
		livesController = c;
	}

	void Update () {
		if (isFalling) {
			timeTillDeath -= Time.deltaTime;
			if (timeTillDeath <= 0) {
				nrLives--;
				livesController.SetLives(playerNumber, nrLives);
				StopFalling();
				if (nrLives > 0) {
					// Respawn
					gameObject.transform.position = new Vector2(0,0);
				}
				else {
					// Die
					currentState = PlayerState.DEAD;
				}
			}
		}

		if(hasMask){
			maskTimeLeft -= Time.deltaTime;
			if(maskTimeLeft < 0) {
				removeMask();
			}
		}
		if(specialAbilityCooldown<0) {
			specialAbilityReady = true;
		} else {
			specialAbilityCooldown -= Time.deltaTime;
		}
	}

	public void SetNumber(int number) {
		playerNumber = number;
		playerController.SetupMovementLabels(number);
		bodyRenderer.color = colors[number-1];
		winnerScreen = winnerScreens [number - 1];
	}

	public int GetNumber() {
		return playerNumber;
	}

	public void GoToState(PlayerState newState) {
		animator.SetInteger("state", (int) newState);
		currentState = newState;
	}

	public PlayerState GetCurrentState() {
		return currentState;
	}

	public void Fall(string tag) {
		animator.SetBool("isFalling", true);
		isFalling = true;
		timeTillDeath = fallingTime;
		rigidBody2D.gravityScale = fallSpeed;
		shadow.SetActive(false);

		// don't collide while falling
		hitCollider.SetActive(false);
		holeCollider.SetActive(false);

		// Don't respawn with mask
		if (hasMask)
			removeMask();

		// make player fall behind platform
		if (tag == "TopTrigger") {
			bodyRenderer.sortingLayerName = "FallingObjects";
			maskRenderer.sortingLayerName = "FallingObjects";
		}
	}

	private void StopFalling() {
		animator.SetBool("isFalling", false);
		isFalling = false;
		rigidBody2D.gravityScale = 0;
		shadow.SetActive(true);
		bodyRenderer.sortingLayerName = "Players";
		maskRenderer.sortingLayerName = "Masks";
		ShakeCamera();
		hitCollider.SetActive(true);
		holeCollider.SetActive(true);
	}

	public bool IsFalling() {
		return isFalling;
	}

	public void ReceiveMask(Mask.TYPES type){
		Debug.Log("Picked up Mask! (" + type + ")");
		this.maskType = type;
		maskTimeLeft = defaultMaskTimeout;
		// idea: if(type == Mask.TYPES.specialLongDurationMask) maskTimeLeft = 3*defaultMaskTimeout;

		maskRenderer.sprite = Sprite.Instantiate(masks[(int)type]);
		maskRenderer.enabled = true;
		hasMask = true;
		specialAbilityReady = true;
		specialAbilityCooldown = sa_cooldownDuration;
		UnmuteTrack ();

		// Now we stinggggg!!
		if (type == Mask.TYPES.CACTUS)
			hitCollider.SetActive(true);
	}

	public GameObject GetWinnerScreen() {
		return winnerScreen;
	}

	private void removeMask(){
		hasMask = false;
		// for now make mask invisible
		maskRenderer.sprite = Sprite.Instantiate(emptymask);
		specialAbilityReady = false;
		MuteTrack();

		// No more sting :(
		if (maskType == Mask.TYPES.CACTUS)
			hitCollider.SetActive(false);
	}

	public void specialAction(){
		if(hasMask && specialAbilityReady) {
			switch(maskType) {
			case Mask.TYPES.SATAN:
					spawnFireRain();
					break;
			case Mask.TYPES.CACTUS:
				break;
			case Mask.TYPES.BOMB:
					layBomb();
					break;
			case Mask.TYPES.CHICKEN:
					shootEgg();
					break;
			default:
				break;
			}
			specialAbilityReady = false;
			specialAbilityCooldown = sa_cooldownDuration;
		}
	}

	// Players takes a hit through dashes, boomerangs, projectiles....
	public void TakeHit(Vector2 impactDirection, float forceStrength) {
		// Play animation
		// Add force
		rigidBody2D.AddForce(impactDirection * forceStrength, ForceMode2D.Impulse);
	}

	private void ShakeCamera() {
		Camera.main.GetComponent<CameraShake> ().StartShake (0.2f, 0.2f);
	}

	private void UnmuteTrack() {
		GameObject manager = GameObject.Find ("AudioManager");
		AudioLoop loop = manager.GetComponent<AudioLoop> ();
		loop.UnmuteRandomTrack ();
	}

	private void MuteTrack() {
		GameObject manager = GameObject.Find ("AudioManager");
		AudioLoop loop = manager.GetComponent<AudioLoop> ();
		loop.MuteRandomTrack ();
	}

	private void spawnFireRain(){
		for(int i = -1;i<2;i++) {
			for(int j = -1;j<2;j++) {
				if(!(i==0&&j==0)){
					GameObject bullet = GameObject.Instantiate(bulletPrefab);
					bullet.transform.position = transform.position + new Vector3(1f*i,1f*j,0);
					bullet.GetComponent<Bullet>().direction = new Vector2(i,j);
				}
			}
		}
	}
	private void layBomb() {
		GameObject bomb = GameObject.Instantiate(bombPrefab);
		bomb.transform.position = transform.position;
	}
	private void shootEgg() {
		GameObject bullet = GameObject.Instantiate(EggPrefab);
		Vector2 direction = -rigidBody2D.velocity.normalized;
		bullet.GetComponent<Bullet>().direction = direction;
		bullet.transform.position = transform.position + (Vector3) direction;
	}
}
