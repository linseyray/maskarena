using UnityEngine;
using System.Collections;

public class HoleDetector : MonoBehaviour {

	void Start () {
	}
	
	void Update () {
	}
		
	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.tag == "TopTrigger" || collider.tag == "SideTrigger" || collider.tag == "BottomTrigger")
			gameObject.transform.parent.gameObject.SendMessage("Fall", collider.tag);
	}
}
