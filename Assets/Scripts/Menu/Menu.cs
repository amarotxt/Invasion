using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {
	public GameObject painelTutorial;
	public Text pontuacaoRecord;
	public Text pontuacao;
	int parentPosition;

	void Start(){
		pontuacaoRecord.text = PlayerPrefs.GetInt ("record").ToString();
		pontuacao.text = PlayerPrefs.GetInt ("pontuacao").ToString();
	}
	public void NewGame (){
		SceneManager.LoadScene (1);
	}
	public void OpenTutorial (){
		painelTutorial.SetActive (true);
	}
	public void CloseTutorial(){
		painelTutorial.SetActive (false);
	}


	public void NextPainelTuroial(){
		parentPosition += 1;
		if (parentPosition >= 1 ) {
			parentPosition = 1;
		} 
		DesativarPaineisTutorial ();
		painelTutorial.transform.GetChild (parentPosition).gameObject.SetActive (true);
	}
	public void BackPainelTuroial(){
		parentPosition -= 1;
		if (parentPosition <= 0 ) {
			parentPosition = 0;
		}
		DesativarPaineisTutorial ();
		painelTutorial.transform.GetChild (parentPosition).gameObject.SetActive (true);
	}
	void DesativarPaineisTutorial(){
		for(int i=0;i<=1; i++){
			painelTutorial.transform.GetChild (i).gameObject.SetActive (false);
		}
	}

}
