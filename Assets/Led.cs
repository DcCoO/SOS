using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Led : MonoBehaviour {
	public float speed;
	SpriteRenderer sr;
	bool acendendo = false;
	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!acendendo) {
			sr.color -= new Color (speed, speed, speed, 0) * Time.deltaTime;
			if (sr.color.r < 0.5f)
				acendendo = true;
		} else {
			sr.color += new Color (speed, speed, speed, 0) * Time.deltaTime;
			if (sr.color.r > 1f)
				acendendo = false;
		}
	}
}
