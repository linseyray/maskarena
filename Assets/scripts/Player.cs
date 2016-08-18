using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

	public enum PlayerState { IDLE = 0, RUNNING = 1, DASHING = 2, FALLING = 3, DEAD };
	private PlayerState currentState = PlayerState.IDLE;

	private string playerName;
	public List<Color> colors;

	public int startNrLives;
	private int nrLives; // Players

	private Animator animator;
	public SpriteRenderer bodyRenderer; //body
	public SpriteRenderer maskRenderer; //body

	private PlayerController playerController;
	private Rigidbody2D rigidBody2D;

	private bool isFalling = false;
	public float fallingTime; // How long the player should fall before dying
	private float timeTillDeath;

	public GameObject maskGameobject;

	public float defaultMaskTimeout = 5.0f;
	private bool hasMask = false;
	private float maskTimeLeft;
	public Mask.TYPES maskType;

	public int score {get;set;}
	public Sprite[] masks;
	public Sprite emptymask;

	void Awake() {
		playerController = gameObject.GetComponent<PlayerController>();
		bodyRenderer = gameObject.GetComponent<SpriteRenderer>();
		animator = gameObject.GetComponentInChildren<Animator>();
		rigidBody2D = gameObject.GetComponent<Rigidbody2D>();
	}

	void Start () {
		nrLives = startNrLives;
	}

	void Update () {
		if (isFalling) {
			timeTillDeath -= Time.deltaTime;
			if (timeTillDeath <= 0) {
				nrLives--;
				StopFalling();
				if (nrLives > 0) {
					// Respawn
					gameObject.transform.position = new Vector2(0,0);
				}
				else {
					// Die
					gameObject.SetActive(false);
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
	}

	public void SetNumber(int number) {
		playerController.SetupMovementLabels(number);
		bodyRenderer.color = colors[number-1];
	}

	public void GoToState(PlayerState newState) {
		animator.SetInteger("state", (int) newState);
		currentState = newState;
	}

	public PlayerState GetCurrentState() {
		return currentState;
	}

	public void Fall() {
		animator.SetBool("isFalling", true);
		isFalling = true;
		timeTillDeath = fallingTime;
		rigidBody2D.gravityScale = 1;
	}

	private void StopFalling() {
		animator.SetBool("isFalling", false);
		isFalling = false;
		rigidBody2D.gravityScale = 0;
	}

	public bool IsFalling() {
		return isFalling;
	}

	public void ReceiveMask(Mask.TYPES type){
		this.maskType = type;
		maskTimeLeft = defaultMaskTimeout;
		// idea: if(type == Mask.TYPES.specialLongDurationMask) maskTimeLeft = 3*defaultMaskTimeout;

		// TODO: add mask sprite to player object
		maskRenderer.sprite = Sprite.Instantiate(masks[(int)type]);
		maskRenderer.enabled = true;
		hasMask = true;
	}

	private void removeMask(){
		hasMask = false;
		// for now make mask invisible
		maskRenderer.sprite = Sprite.Instantiate(emptymask);
		// TODO: remove mask sprite from player object
	}

	public void specialAction(){
		if(hasMask) {
			switch(maskType) {
			default:
				break;
			}
		}
	}

	public void increaseScore() {
		this.score += 1;
	}

	// Players takes a hit through dashes, boomerangs, projectiles....
	//public void TakeHit(Vector2 impactDirection, float forceStrength) {
	public void TakeHit() {
		// Play animation
		// Add force
		ShakeCamera();

	}

	void OnCollisionEnter2D(Collision2D collision) {
		Debug.Log ("collision done");
		TakeHit ();
	}

	private void ShakeCamera() {
		Camera.main.GetComponent<CameraShake> ().StartShake (0.2f, 0.2f);
	}
}
