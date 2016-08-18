using UnityEngine;
using System.Collections;

// TODO rename to PlayerController

public class PlayerMovement : MonoBehaviour {

	private Player player;
	private Rigidbody2D rigidBody2D;
	public SpriteRenderer bodyRenderer;
	public SpriteRenderer maskRenderer;

	public float speed;

	private string axisH;
	private string axisV;

	private bool facingLeft = false;

	void Awake() {
		rigidBody2D = gameObject.GetComponent<Rigidbody2D>();
		player = gameObject.GetComponent<Player>();
	}

	void Start() {
	}

	public void SetupMovementLabels(int playerNumber) {
		axisH = "P" + playerNumber + "_Horizontal";
		axisV = "P" + playerNumber + "_Vertical";
	}
	
	void Update () {
		Move();
	}

	private void Move() {
		float moveHorizontal = Input.GetAxis(axisH);
		float moveVertical = Input.GetAxis(axisV);
		Vector2 inputDirection = new Vector2(moveHorizontal, moveVertical);
		rigidBody2D.AddForce(inputDirection * speed);

		if (!facingLeft && moveHorizontal < 0 || facingLeft && moveHorizontal > 0)
			FlipSprite();

		if (moveHorizontal == 0 && moveVertical == 0)
			player.GoToState(Player.PlayerState.IDLE);
		else
		if (player.GetCurrentState() != Player.PlayerState.RUNNING)
			player.GoToState(Player.PlayerState.RUNNING);
	}

	private void FlipSprite() {
		facingLeft	= !facingLeft;
		bodyRenderer.flipX = facingLeft;
		//maskRenderer.flipX = facingRight;
	}
}
