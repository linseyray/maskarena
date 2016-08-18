using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

	public enum PlayerState { IDLE = 0, RUNNING = 1, DASHING = 2 };
	private PlayerState currentState = PlayerState.IDLE;

	private string name;
	private int number;
	public List<Color> colors;

	private Animator animator;
	private SpriteRenderer spriteRenderer;
	private PlayerMovement playerMovement;

	void Awake() {
		playerMovement = gameObject.GetComponent<PlayerMovement>();
		spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		animator = gameObject.GetComponentInChildren<Animator>();
	}

	void Start () {
	}
	
	void Update () {
	}

	public void SetNumber(int number) {
		this.number = number;
		playerMovement.SetupMovementLabels(number);
		spriteRenderer.color = colors[number-1];
	}
		
	public void GoToState(PlayerState newState) {
		animator.SetInteger("state", (int) newState);
		currentState = newState;

		/*
		switch (newState) {
		case PlayerState.IDLE:
			break;
		case PlayerState.RUNNING:
			break;
		}
		*/
	}

	public PlayerState GetCurrentState() {
		return currentState;
	}
}
