using UnityEngine;
using System.Collections;

public class FaceBodyController : MonoBehaviour {

	Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void makeNeutral() {
		anim.SetTrigger ("Neutral");
	}

	public void makePositive() {
		anim.SetTrigger ("Positive");
	}

	public void makeNegative() {
		anim.SetTrigger ("Negative");
	}

}
