using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	private Rigidbody2D rigidBody2D;
	public float speed;

	private string axisH;
	private string axisV;

	void Awake() {
		rigidBody2D = gameObject.GetComponent<Rigidbody2D>();
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
	}
}
