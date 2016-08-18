using UnityEngine;
using System.Collections;

// TODO rename to PlayerController

public class PlayerMovement : MonoBehaviour {

	private Player player;
	private Rigidbody2D rigidBody2D;
	public SpriteRenderer bodyRenderer;
	public SpriteRenderer maskRenderer;
	private Animator animator;

	public float speed;
	public float dashSpeed;
	public float dashLength; // In seconds

	private string axisH;
	private string axisV;
	private string dashButton;

	private bool facingLeft = false;

	void Awake() {
		rigidBody2D = gameObject.GetComponent<Rigidbody2D>();
		player = gameObject.GetComponent<Player>();
		animator = player.gameObject.GetComponentInChildren<Animator>();
	}

	void Start() {
	}

	public void SetupMovementLabels(int playerNumber) {
		axisH = "P" + playerNumber + "_Horizontal";
		axisV = "P" + playerNumber + "_Vertical";
		dashButton = "P" + playerNumber + "_Dash";
	}
	
	void Update () {
		Move();
			
		if (Input.GetButtonDown(dashButton))
			Dash();

		Debug.Log(player.GetCurrentState());
		if (Player.PlayerState.DASHING == player.GetCurrentState()) {
			float dashTimeLeft = animator.GetFloat("dashTimeLeft") - Time.deltaTime;
			//Debug.Log("dashing.." + dashTimeLeft + " " + Time.deltaTime);
			animator.SetFloat("dashTimeLeft", dashTimeLeft);
		}
	}

	private void Move() {
		float moveHorizontal = Input.GetAxis(axisH);
		float moveVertical = Input.GetAxis(axisV);
		Vector2 inputDirection = new Vector2(moveHorizontal, moveVertical);
		rigidBody2D.AddForce(inputDirection * speed);

		// Flip the sprite if necessary
		if (!facingLeft && moveHorizontal < 0 || facingLeft && moveHorizontal > 0)
			FlipSprite();

		// Change animation states 
		if (moveHorizontal == 0 && moveVertical == 0)
			player.GoToState(Player.PlayerState.IDLE);
		else
		if (player.GetCurrentState() != Player.PlayerState.RUNNING)
			player.GoToState(Player.PlayerState.RUNNING);
	}

	private void Dash() {
		float moveHorizontal = Input.GetAxis(axisH);
		float moveVertical = Input.GetAxis(axisV);
		Vector2 inputDirection = new Vector2(moveHorizontal, moveVertical);
		player.GoToState(Player.PlayerState.DASHING);
		animator.SetFloat("dashTimeLeft", dashLength);
		rigidBody2D.AddForce(inputDirection * dashSpeed, ForceMode2D.Impulse);
	}

	private void FlipSprite() {
		facingLeft	= !facingLeft;
		bodyRenderer.flipX = facingLeft;
		//maskRenderer.flipX = facingRight;
	}
}
