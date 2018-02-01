using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMover : MonoBehaviour {

	BoxCollider2D box;
	SpriteRenderer sr;

	public Vector2 position;

	void Start () {
		box = GetComponent<BoxCollider2D> ();
		sr = GetComponent<SpriteRenderer> ();
		Resize ();
	}
	
	// Update is called once per frame
	void Update () {
		
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

		transform.position += new Vector3 (
			transform.position.x + (position.x * worldScreenWidth),
			transform.position.y + (position.y * worldScreenHeight),
			0
		);

	}
}
