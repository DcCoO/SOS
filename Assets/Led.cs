﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Led : MonoBehaviour {
	public float speed;
	SpriteRenderer sr;
	bool acendendo = false;
	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer> ();
		Resize ();
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

	void Resize(){

		transform.localScale=new Vector3(1,1,1);

		float width=sr.sprite.bounds.size.x;
		float height=sr.sprite.bounds.size.y;


		float worldScreenHeight=Camera.main.orthographicSize*2f;
		float worldScreenWidth=worldScreenHeight/Screen.height*Screen.width;

		Vector3 xWidth = transform.localScale;
		xWidth.x=worldScreenWidth / width;
		transform.localScale=xWidth;
		//transform.localScale.x = worldScreenWidth / width;
		Vector3 yHeight = transform.localScale;
		yHeight.y=worldScreenHeight / height;
		transform.localScale=yHeight;
		//transform.localScale.y = worldScreenHeight / height;

	}
}
