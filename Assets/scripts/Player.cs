using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

	public enum PlayerState { IDLE = 0, RUNNING = 1, DASHING = 2, DEAD };
	private PlayerState currentState = PlayerState.IDLE;

	private string name;
	private int number;
	public List<Color> colors;

	private Animator animator;
	private SpriteRenderer spriteRenderer;
	private PlayerMovement playerMovement;

	public float defaultMaskTimeout = 5.0f;
	private bool hasMask = false;
	private float maskTimeLeft;
	private Mask.TYPES maskType;

	public int score {get;set;}

	// temporary variables
	public Sprite defaultMask;


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

	public void ReceiveMask(Mask.TYPES type){
		this.maskType = type;
		maskTimeLeft = defaultMaskTimeout;
		// idea: if(type == Mask.TYPES.specialLongDurationMask) maskTimeLeft = 3*defaultMaskTimeout;

		// TODO: add mask sprite to player object
		SpriteRenderer maskRenderer = maskGameobject.GetComponent<SpriteRenderer>();
		maskRenderer.sprite = Sprite.Instantiate(defaultMask);
		maskRenderer.enabled = true;
		hasMask = true;
	}

	private void removeMask(){
		hasMask = false;
		if (maskGameobject) {
			// for now make mask invisible 
			maskGameobject.GetComponent<SpriteRenderer>().enabled = false;
		}
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


}
