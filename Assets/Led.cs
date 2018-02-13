using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Led : MonoBehaviour {
	public float speed;
	Light lt;
	bool acendendo = false;
	// Use this for initialization
	void Start () {
		lt = GetComponent<Light> ();
		Resize ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!acendendo) {
			lt.intensity -= speed * Time.deltaTime;
			if (lt.intensity < 0.01f)
				acendendo = true;
		} else {
			lt.intensity += speed * Time.deltaTime;
			if (lt.intensity > 0.5f)
				acendendo = false;
		}
	}

	void Resize(){

		SpriteRenderer sr = GetComponent<SpriteRenderer> ();
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
