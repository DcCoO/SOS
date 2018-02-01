using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour {

	public Text gameName;
	public Image logo;
	public GameObject play;
	public Text highscore;

	public GameObject heart;
	public int score;

	public GameObject sound;
	// Use this for initialization
	void Start () {
		style.fontSize = (int)((float)Screen.width * 36.0f / 456.0f);
		highscore.text = "High Score: " + PlayerPrefs.GetInt ("score");
	}
	
	// Update is called once per frame

	public void StartGame(){
		life = 3;
		gameName.enabled = false;
		logo.enabled = false;
		play.SetActive(false);
		highscore.enabled = false;
		score = 0;
		GameObject.Find ("Spawner").GetComponent<Spawner> ().enabled = true;

		h1 = Instantiate (heart, pos (0.85f, 0.95f), Quaternion.identity);
		h2 = Instantiate (heart, pos (0.90f, 0.95f), Quaternion.identity);
	}

	public void EndGame(){

		GameObject[] objs = GameObject.FindGameObjectsWithTag ("plane");
		foreach (GameObject obj in objs)
			Destroy (obj);

		objs = GameObject.FindGameObjectsWithTag ("trail");
		foreach (GameObject obj in objs)
			Destroy (obj);

		objs = GameObject.FindGameObjectsWithTag ("objective");
		foreach (GameObject obj in objs)
			Destroy (obj);

		gameName.enabled = true;
		logo.enabled = true;
		play.SetActive(true);
		int max = Mathf.Max (score, PlayerPrefs.GetInt ("score"));
		PlayerPrefs.SetInt ("score", max);
		highscore.text = "High Score: " + PlayerPrefs.GetInt ("score");
		highscore.enabled = true;
		GameObject.Find ("Spawner").GetComponent<Spawner> ().Stop ();
		Instantiate (sound, Vector3.zero, Quaternion.identity);
	}

	public GUIStyle style;
	Rect rect = new Rect(0, Screen.height - 50, Screen.width, 50);

	void OnGUI(){
		if (!gameName.enabled) {
			GUI.Label(rect, "Score: " + score.ToString(), style);
		}
	}

	public int life;
	public GameObject h1, h2;

	public void Hit(){
		life--;
		if (life == 0) {
			EndGame ();
		} else if (life == 2) {
			Destroy (h1);
		}
		else {
			Destroy (h2);
		}
	}


	public Vector2 pos(float x, float y){
		x = (float)Screen.width * x;
		y = (float)Screen.height * y;

		return Camera.main.ScreenToWorldPoint (new Vector3 (x, y, 0));
	}

}
