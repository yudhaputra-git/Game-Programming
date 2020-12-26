using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour {
	private int life = 100;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void PlayGame(){
		SceneManager.LoadScene("Level1");
		PlayerPrefs.SetInt("MyLife",life);
		PlayerPrefs.SetInt("Score",0);
	}

	public void Level2(){
		SceneManager.LoadScene("Level2");
	}

	public void Level3(){
		SceneManager.LoadScene("Level3");
	}

	public void Home(){
		SceneManager.LoadScene("MenuScene");
	}
	
	public void Quit(){
		Application.Quit();
	}
}
