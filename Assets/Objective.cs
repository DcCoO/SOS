using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour {

	public int ID;

	public GameObject sound;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "plane") {
			if (this.ID == col.gameObject.GetComponent<Plane> ().ID) {
				GameObject.Find ("Controller").GetComponent<Controller> ().score++;
				Instantiate (sound, Vector3.zero, Quaternion.identity);
				col.gameObject.GetComponent<Plane> ().Land ();
				Destroy (gameObject);
			}
		}
	}
}
