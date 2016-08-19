using UnityEngine;
using System.Collections;

public class Countdown : MonoBehaviour {
	private Animator anim;

	// Use this for initialization
	void Start () {
		anim = (Animator)GetComponent("Animator");
		anim.speed = 0.0f;
		anim.GetComponent<Renderer>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void Show() {
		anim.GetComponent<Renderer>().enabled = true;
		anim.speed = 0.25f;
		Destroy (this.gameObject, 3);
	}
}
