using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour {

	AudioSource audio;

	void Start () {
		audio = GetComponent<AudioSource> ();
		audio.Play ();
		Destroy (gameObject, audio.clip.length);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
