using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ControllerEnemyHealthBar : MonoBehaviour {

	GameObject healthEnemyBar;
	GameObject healthBarInstance;
	GameObject canvasHealthBar;

	Slider healthSlider;

	Vector2 positionHealthBar;

	float healthValues;
	// Use this for initialization

	void Start () {
		canvasHealthBar = GameObject.Find ("CanvasHealthBar");
		healthEnemyBar = (GameObject)Resources.Load("Prefabs/Inimigos/CanvasUtilites/HealthBarParent",typeof(GameObject));
		healthBarInstance = Instantiate (healthEnemyBar, transform.position, Quaternion.identity);
		healthBarInstance.transform.SetParent (canvasHealthBar.transform, false);
		healthBarInstance.transform.position = new Vector3(100,0,0);
		healthSlider = healthBarInstance.transform.GetComponentInChildren<Slider> ();
		healthValues = 0;
	}
	
	// Update is called once per frame
	void Update () {
		positionHealthBar = Camera.main.WorldToScreenPoint(transform.position);
		positionHealthBar.y -= 15;
		if(healthBarInstance != null){
		healthBarInstance.transform.position = positionHealthBar;
		}
	}
	public void ChangeHealthvalue(float fullhealth, float health){
		if (healthSlider != null) {
			healthSlider.value = CauculateHealthBar (fullhealth, health);
		}
		if (health <= 0){
			Destroy (healthBarInstance);
		}
	}
	public float CauculateHealthBar(float fullHealth,float health){
		return (health / fullHealth);
	}
}
