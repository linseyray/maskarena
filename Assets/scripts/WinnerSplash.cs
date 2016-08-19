using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class WinnerSplash : MonoBehaviour {
	private Image image;

	// Use this for initialization
	void Start () {
		image = (Image)GetComponent("Image");
		image.GetComponent<Renderer>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Show() {
		image.GetComponent<Renderer>().enabled = true;
	}
}
