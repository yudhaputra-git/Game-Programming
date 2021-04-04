using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	private float score;
	public Text scoretext; 
	public GameObject[] enemies;
	// Use this for initialization
	void Start () {
		score = 0f;
		PlayerPrefs.SetFloat("Score", score);
	}
	
	// Update is called once per frame
	void Update () {
		enemies = GameObject.FindGameObjectsWithTag("Dummie");
		Debug.Log(enemies.Length);
		if(enemies.Length <= 0 && SceneManager.GetActiveScene().name == "Level1"){
			SceneManager.LoadScene("Level2");
		}
		if(enemies.Length <= 0 && SceneManager.GetActiveScene().name == "Level2"){
			SceneManager.LoadScene("Level3");
		}
		if(enemies.Length <= 0 && SceneManager.GetActiveScene().name == "Level3"){
			SceneManager.LoadScene("Victory");
		}
		score += Time.deltaTime;
		PlayerPrefs.SetFloat("Score", score);
		if(Input.GetKeyDown("n")){
			SceneManager.LoadScene("MainMenu");
		}
		if(scoretext != null){
			scoretext.text = "Your score is : " + score.ToString();
		}
		else{
			return;
		}
	}

	public void Play(){
		SceneManager.LoadScene("Level1");
	}

	public void Quit(){
		Application.Quit();
	}

	public void Home(){
		SceneManager.LoadScene("MainMenu");
	}

	public void About(){
		SceneManager.LoadScene("About");
	}

	public void Help(){
		SceneManager.LoadScene("Help");
	}
}
