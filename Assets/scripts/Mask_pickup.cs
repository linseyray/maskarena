using UnityEngine;
using System.Collections;

public class Mask_pickup : MonoBehaviour {
	public float maskDisappearAnimationTime = 5f;
	public float maskDisappearTime = 10f;
	public float maskLifeTime;
	// Use this for initialization
	void Awake () {
		maskLifeTime = maskDisappearTime;
	}
	
	// Update is called once per frame
	void Update () {
		maskLifeTime -= Time.deltaTime;
		if(maskLifeTime < 0)
			Destroy(this.gameObject);
	}

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log("mask collision");
		if(other.tag=="PlayerCollider") {
			// could also compare to global tag list
			Mask mask = this.GetComponent<Mask>();
			other.transform.parent.GetComponent<Player>().ReceiveMask(mask.type);
			// When picked up, destroy immediately
			Object.Destroy(this.gameObject);
			// TODO: trigger pickup animation
		}
	}
}
