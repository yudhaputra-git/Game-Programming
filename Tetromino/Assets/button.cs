using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class button : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlayButton(){
		SceneManager.LoadScene(1);
	}

	public void CreditButton(){
		SceneManager.LoadScene(2);
	}

	public void QuitButton(){
		Application.Quit();
	}

	public void HomeButton(){
		SceneManager.LoadScene(0);
	}
}
