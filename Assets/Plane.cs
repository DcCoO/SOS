﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour {

	public int ID;

	List<GameObject> path;

	public GameObject pathPoint;

	public Vector2 nextPosition;

	bool drawing;

	Vector2 position;

	float moveSpeed;
	float turnSpeed;

	float distance;

	public GameObject objective;

	bool active = false;

	public GameObject sound;

	float offset = 17;
	// Use this for initialization
	void Start () {
		path = new List<GameObject>();
		distance = 0.2f;
		moveSpeed = 0.5f;
		turnSpeed = 2f;
		GetComponent<CircleCollider2D> ().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (!active) {
			Vector2 screenPos = Camera.main.WorldToScreenPoint (transform.position);
			active = 
				offset <= screenPos.x && 
				screenPos.x <= Screen.width - offset && 
				offset <= screenPos.y && 
				screenPos.y <= Screen.height - offset;
			if(active) GetComponent<CircleCollider2D> ().enabled = true;
		}
		
		if (drawing) {
			Vector2 currMouse = worldPos ();
			if ((currMouse - position).magnitude > distance) {
				position = currMouse;
				path.Add (Instantiate (pathPoint, currMouse, Quaternion.identity));
				path[path.Count - 1].GetComponent<SpriteRenderer> ().color = GetComponent<SpriteRenderer> ().color;
			}
		}

		if (path.Count == 0) {
			path.Add (Instantiate (pathPoint, transform.position + transform.up * distance, Quaternion.identity));
			path [0].SetActive (false);
		}

		Vector2 target = (Vector2) path [0].transform.position - (Vector2)transform.position;
		target.Normalize ();

		float targetAngle = Mathf.Atan2 (target.y, target.x) * Mathf.Rad2Deg;

		//transform.position += (Vector3)target * moveSpeed * Time.deltaTime;
		GetComponent<Rigidbody2D>().velocity = (Vector3)target * moveSpeed * Time.deltaTime * 100;
		transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler(0, 0, targetAngle - 90), turnSpeed * Time.deltaTime);

		if (Vector2.Distance (transform.position, path [0].transform.position) < 0.1f) {
			Destroy (path [0]);
			path.RemoveAt (0);
		}
	}

	void OnMouseDown(){
		foreach (GameObject g in path)
			Destroy (g);
		path.Clear ();
		drawing = true;
		position = worldPos();
	}

	void OnMouseUp(){
		drawing = false;
	}

	public void Explode(){
		foreach (GameObject g in path)
			Destroy (g);
		Destroy(GameObject.Find("Target " + this.ID.ToString()));
		Instantiate (sound, Vector3.zero, Quaternion.identity);
		Destroy (gameObject);
	}

	public void Land(){
		foreach (GameObject g in path)
			Destroy (g);
		Destroy(GameObject.Find("Target " + this.ID.ToString()));
		Destroy (gameObject);
	}

	Vector2 screenPos(){
		return (Vector2) Camera.main.WorldToScreenPoint (transform.position);
	}

	Vector2 worldPos(){
		return (Vector2)Camera.main.ScreenToWorldPoint (Input.mousePosition);
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "plane") {
			GetComponent<AudioSource> ().Play ();
			GameObject.Find ("Controller").GetComponent<Controller> ().Hit ();
			Explode ();
		}
		else if(col.gameObject.tag == "wall") {
			GetComponent<AudioSource> ().Play ();
			GameObject.Find ("Controller").GetComponent<Controller> ().Hit ();
			Explode ();
		}
	}
}
