using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	private int life, score, highscore;

	[SerializeField]
	private Text lifetext, scoretext, highscoretext;

	[SerializeField]
	private GameObject gameoverpanel;
	private float timeplayed;
	// Use this for initialization
	void Start () {
		gameoverpanel.SetActive(false);
		timeplayed = 0f;
		life = PlayerPrefs.GetInt("MyLife");
		score = PlayerPrefs.GetInt("Score");
	}
	
	// Update is called once per frame
	void Update () {
		life = PlayerPrefs.GetInt("MyLife");
		score = PlayerPrefs.GetInt("Score");
		if(life <= 0){
			gameoverpanel.SetActive(true);
			Cursor.visible = true;
		}
		timeplayed += Time.deltaTime;
		score -= Mathf.RoundToInt(timeplayed);
		if(score > highscore){
				highscore = score;
				PlayerPrefs.SetInt("Highscore", highscore);
		}
		highscore = PlayerPrefs.GetInt("Highscore");
		highscoretext.text = "High Score : " + highscore.ToString();
		scoretext.text = "Score : " + score.ToString();
		lifetext.text = "Life : " + life.ToString();
	}
}
