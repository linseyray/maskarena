﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

	public enum PlayerState { IDLE = 0, RUNNING = 1, DASHING = 2, FALLING = 3, DEAD };
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

	public int score {get;set;}
	public Sprite[] masks;
	public Sprite emptymask;

	public GameObject shadow;

	public GameObject hitCollider;
	public GameObject holeCollider;

	public float sa_cooldownDuration = 2f;
	private float specialAbilityCooldown = 0;
	private bool specialAbilityReady = true;
	public GameObject bulletPrefab;
	public LivesController livesController;

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
		Debug.Log(tag);
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
		this.maskType = type;
		maskTimeLeft = defaultMaskTimeout;
		// idea: if(type == Mask.TYPES.specialLongDurationMask) maskTimeLeft = 3*defaultMaskTimeout;

		// TODO: add mask sprite to player object
		maskRenderer.sprite = Sprite.Instantiate(masks[(int)type]);
		maskRenderer.enabled = true;
		hasMask = true;
		specialAbilityReady = true;
		specialAbilityCooldown = sa_cooldownDuration;
		UnmuteTrack ();
	}

	private void removeMask(){
		hasMask = false;
		// for now make mask invisible
		maskRenderer.sprite = Sprite.Instantiate(emptymask);
		// TODO: remove mask sprite from player object
		specialAbilityReady = false;
		MuteTrack();
	}

	public void specialAction(){
		if(hasMask && specialAbilityReady) {
			switch(maskType) {
			case Mask.TYPES.SATAN:
				{
					spawnFireRain();
					break;
				}
			default:
				break;
			}
			specialAbilityReady = false;
			specialAbilityCooldown = sa_cooldownDuration;
		}
	}

	public void increaseScore() {
		this.score += 1;
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
					bullet.transform.parent = transform;
					bullet.transform.localPosition = new Vector2(i,j);
				}
			}
		}
	}
}
