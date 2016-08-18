﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

	public enum PlayerState { IDLE = 0, RUNNING = 1, DASHING = 2, FALLING = 3, DEAD };
	private PlayerState currentState = PlayerState.IDLE;

	private string name;
	private int number;
	public List<Color> colors;

	public int startNrLives;
	private int nrLives; // Players

	private Animator animator;
	private SpriteRenderer spriteRenderer;
	private PlayerController playerController;
	private Rigidbody2D rigidBody2D;

	private bool isFalling = false;
	public float fallingTime; // How long the player should fall before dying
	private float timeTillDeath;


	void Awake() {
		playerController = gameObject.GetComponent<PlayerController>();
		spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		animator = gameObject.GetComponentInChildren<Animator>();
		rigidBody2D = gameObject.GetComponent<Rigidbody2D>();
	}

	void Start () {
		nrLives = startNrLives;
	}
	
	void Update () {
		if (isFalling) {
			timeTillDeath -= Time.deltaTime;
			Debug.Log(timeTillDeath);
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
	}

	public void SetNumber(int number) {
		this.number = number;
		playerController.SetupMovementLabels(number);
		spriteRenderer.color = colors[number-1];
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
}
