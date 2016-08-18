using UnityEngine;
using System.Collections;

public class Mask_pickup : MonoBehaviour {
	public float maskDisappearAnimationTime = 1.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag.ToString().Equals("Player")) {
		// could also compare to global tag list
			Mask mask = this.GetComponent<Mask>();
			other.GetComponent<Player>().ReceiveMask(mask.type);
			// TODO: trigger pickup animation
			Destroy(this.gameObject, maskDisappearAnimationTime);
			Debug.Log("mask collision");
		}
	}
}
