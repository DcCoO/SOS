﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public GameObject plane;
	public GameObject target;

	float time = 5.5f;
	float spawnTime = 6;
	float reduceSpawn = 10;
	float reduceTime = 0;
	float k1 = 5, k2 = 2;

	int id = 1;

	public void Stop(){
		id = 1;
		time = 5.5f;
		spawnTime = 6;
		reduceSpawn = 20;
		reduceTime = 0;
		k1 = 5; k2 = 2;
		this.enabled = false;
	}

	// Update is called once per frame
	void Update () {

		if (time > spawnTime) {
			GameObject planeInstance = Instantiate (plane, randomPosition (Random.Range (1, 5)), Quaternion.identity);
			GameObject targetInstance = Instantiate (target, randomPosition (0), Quaternion.identity);

			planeInstance.name = "Plane " + id.ToString ();
			targetInstance.name = "Target " + id.ToString ();
			planeInstance.GetComponent<Plane>().ID = id;
			targetInstance.GetComponent<Objective>().ID = id;

			id++;

			var dir = new Vector3(0,0,0) - planeInstance.transform.position;
			var angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg - 90;
			planeInstance.transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
			
			Color c = randomColor ();
			planeInstance.GetComponent<SpriteRenderer> ().color = c;
			targetInstance.GetComponent<SpriteRenderer> ().color = c;
			time = 0;
		}

		if (reduceTime > reduceSpawn) {
			spawnTime = Mathf.Max(1, spawnTime - 0.25f);
			reduceTime = 0;
		}

		time += Time.deltaTime;
		reduceTime += Time.deltaTime;
	}

	// 013
	// 0x3
	// 023

	Vector2 randomPosition(int q){
		Vector2 tl = world(0, 0);
		Vector2 tr = world(Screen.width, 0);
		Vector2 bl = world(0, Screen.height);
		Vector2 br = world(Screen.width, Screen.height);

		if (q == 0) {
			Vector2 next = new Vector2 (Random.Range(tl.x + 0.2f, tr.x - 0.2f), Random.Range(tl.y + 0.2f, bl.y - 0.2f));

			Plane[] planes = FindObjectsOfType<Plane> ();
			foreach (Plane plane in planes) {
				if (Vector2.Distance (next, plane.transform.position) < 2)
					return randomPosition (0);
			}
			return next;
		}
		else if(q == 1) {
			return new Vector2 (Random.Range(tl.x - k1, tl.x - k2), Random.Range(tl.y - k1, bl.y + k1));
		}
		else if(q == 2) {
			return new Vector2 (Random.Range(tl.x, tr.x), Random.Range(tl.y - k1, tl.y - k2));
		}
		else if(q == 3) {
			return new Vector2 (Random.Range(bl.x, br.x), Random.Range(bl.y + k1, bl.y + k2));
		}
		else {
			return new Vector2 (Random.Range(tr.x + k1, tr.x + k2), Random.Range(tl.y - k1, bl.y + k1));
		}
	}

	Color randomColor(){
		Color c = new Color (Random.value, Random.value, Random.value, 1);
		float h, s, v;
		Color.RGBToHSV (c, out h, out s, out v);
		if (v < 0.4f)
			return randomColor ();
		return c;
	}

	Vector2 world(float x, float y){
		return Camera.main.ScreenToWorldPoint (new Vector2(x, y));
	}
}
