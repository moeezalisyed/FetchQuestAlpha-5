﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScreenManager : MonoBehaviour {

	public Image fqlogo=null;
	[SerializeField] private Button menuyesbutton = null;
	public Text MessagePrompt;
	public Text InputText;
	public string inputstring;
	public bool isPrompt;


	public AudioSource menuyessound;




	// Use this for initialization
	void Start () {

		fqlogo.GetComponent<Image> ();
		menuyesbutton.GetComponent<Button> ();
		menuyesbutton.onClick.AddListener (() => UserInputPrompt ());
		//MessagePrompt.GetComponent<Text> ();
		//InputText.GetComponent<Text> ();
		menuyessound.GetComponent<AudioSource> ();
		isPrompt = false;
		inputstring = null;

	}

	//accessed when start button is clicked
	void UserInputPrompt()
	{
		fqlogo.gameObject.SetActive (false);
		menuyesbutton.gameObject.SetActive (false);
		isPrompt = true;

	}

	void Update(){


		if (isPrompt) 
		{
			
			StartCoroutine(LoadGame());
				
		}

}
			



	

	IEnumerator LoadGame(){
		yield return new WaitForSeconds (2);
		SceneManager.LoadScene("QGiver");	
		
	}
}