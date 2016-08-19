using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private Player player;
	private Rigidbody2D rigidBody2D;
	public SpriteRenderer bodyRenderer;
	public SpriteRenderer maskRenderer;
	private Animator animator;

	public float speed;
	public float dashSpeed;
	public float dashLength; // In seconds
	private bool isDashing = false;
	private float dashTimeLeft = 0;
	public HitController hitController;

	public float stopSpeed = 0.2F;

	private string axisH;
	private string axisV;
	private string dashButton;
	private string specialButton;

	private bool facingLeft = false;

	private AudioSource audioSource;
	public AudioClip dashSound;

	void Awake() {
		rigidBody2D = gameObject.GetComponent<Rigidbody2D>();
		player = gameObject.GetComponent<Player>();
		animator = player.gameObject.GetComponentInChildren<Animator>();
		audioSource = gameObject.GetComponent<AudioSource>();
	}

	void Start() {
	}

	public void SetupMovementLabels(int playerNumber) {
		axisH = "P" + playerNumber + "_Horizontal";
		axisV = "P" + playerNumber + "_Vertical";
		dashButton = "P" + playerNumber + "_Dash";
		specialButton = "P" + playerNumber + "_SpecialAction";
	}
	
	void Update () {
		if (!player.IsFalling() && !isDashing)
			Move();
			
		if (Input.GetButtonDown(dashButton) && !player.IsFalling() && !isDashing)
			Dash();

		if (Input.GetButtonDown(specialButton) && !player.IsFalling() && !isDashing)
			player.specialAction();
		
		if (isDashing) {
			dashTimeLeft -= Time.deltaTime;
			if (dashTimeLeft <= 0)
				EndDash();
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
		if (moveHorizontal == 0 && moveVertical == 0) {
			player.GoToState(Player.PlayerState.IDLE);
			VelocityToZero();
		}
		else
		if (player.GetCurrentState() != Player.PlayerState.RUNNING)
			player.GoToState(Player.PlayerState.RUNNING);
	}

	private void VelocityToZero() {
		rigidBody2D.velocity = Vector2.Lerp(rigidBody2D.velocity, new Vector2(0,0), stopSpeed);
	}

	private void Dash() {
		// Add force
		float moveHorizontal = Input.GetAxis(axisH);
		float moveVertical = Input.GetAxis(axisV);
		Vector2 inputDirection = new Vector2(moveHorizontal, moveVertical);
		rigidBody2D.AddForce(inputDirection * dashSpeed, ForceMode2D.Impulse);

		// Sound
		audioSource.PlayOneShot(dashSound);

		// Animate
		animator.SetBool("isDashing", true);

		// Set state-related stuff
		player.GoToState(Player.PlayerState.DASHING);
		dashTimeLeft = dashLength;
		isDashing = true;
		hitController.gameObject.SetActive(true);
		hitController.SetHitType(HitController.HitType.DASH);
	}

	private void EndDash() {
		animator.SetBool("isDashing", false);
		player.GoToState(Player.PlayerState.IDLE);
		isDashing = false;
		hitController.gameObject.SetActive(false);
		hitController.SetHitType(HitController.HitType.NONE);
	}

	private void FlipSprite() {
		facingLeft	= !facingLeft;
		bodyRenderer.flipX = facingLeft;
		maskRenderer.flipX = facingLeft;
	}

	public bool IsDashing() {
		return isDashing;
	}
}
