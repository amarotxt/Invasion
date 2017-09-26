using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
	public float health;
	public float fullHealth;

	public float damage;
	public float armor;
	public int arm;
	float timer;
	public float timeBetweenAttacks;

	GameObject weapon;
	Vector3 mausePosition;
	Vector3 lookMausePosition;

	public int lvl;
	public int changelvl;
	public int points;
	public int totalPoints;

	void Start () {
		lvl = 0;
		arm = 1;
		points = 0;
		totalPoints = 0;
		changelvl = 100;
		weapon = (GameObject)Resources.Load("Prefabs/Armas/Arma"+arm, typeof(GameObject));
		timeBetweenAttacks += (weapon.GetComponent<Weapon> ().timeBetweenAttacksWeapon);

		damage += weapon.GetComponent<Weapon> ().damageWeapon;
		health += (weapon.GetComponent<Weapon> ().healthWeapon);
		fullHealth = health;
		armor += +(weapon.GetComponent<Weapon> ().armorWeapon);
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		timer += Time.deltaTime;
		lookMausePosition = Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x,Input.mousePosition.y));
//		lookMausePosition = Camera.main.ScreenToWorldPoint (new Vector3(Input.GetTouch(0).position.x,Input.GetTouch(0).position.y));
		lookMausePosition.y = gameObject.transform.position.y;
		gameObject.transform.LookAt (lookMausePosition);
		if (Input.GetMouseButton(0)){
			if(timer >= timeBetweenAttacks){
				Attack ();
				timer = 0f;
			}
		}
		if (points >= changelvl){
			lvl += 1;
			points = 0;
			changelvl += totalPoints/10;
		}

		if(health <= 0) {
			PlayerPrefs.SetInt ("pontuacao",totalPoints);  
			if (totalPoints > PlayerPrefs.GetInt("record")){
				PlayerPrefs.SetInt ("record", totalPoints);
			}
			SceneManager.LoadScene (0);
		}
	}
	public void TakeDamage(float damage){
		if (health > 0) {
			float calculateDamage = damage - Random.Range (armor / 2, armor);
			if (calculateDamage > 0) {
				ControllerDamagePopup.CreatingDamagePopupText (calculateDamage.ToString ("00.00"), transform);
				health -= calculateDamage;
			} else
				ControllerDamagePopup.CreatingDamagePopupText ("00.00", transform);
		} 
	}

	public void Attack(){
		mausePosition = Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x,Input.mousePosition.y));
//		mausePosition = Camera.main.ScreenToWorldPoint (new Vector3(Input.GetTouch(0).position.x,Input.GetTouch(0).position.y));
		mausePosition.y = gameObject.transform.position.y+1;
		GameObject clone = Instantiate (weapon, gameObject.transform.position,Quaternion.identity);
		clone.transform.LookAt (mausePosition);
	}

	public void IncreaseHealth(){
		health +=  (int)Mathf.Log(lvl)+5;
		fullHealth += (int)Mathf.Log(lvl)+5;
	}
	public void IncreaseArmor(){
		armor +=  (int)Mathf.Log(lvl)+1;
		}
	public void IncreaseStrength(){
		damage +=  (int)Mathf.Log(lvl)+1;
	}

	public void IncreaseAttackSpeed(){
		if (timeBetweenAttacks > 0.5f ){
			timeBetweenAttacks -= 0.1f;
		}else if (timeBetweenAttacks < 0.5f && timeBetweenAttacks > 0.4f){
			timeBetweenAttacks -= 0.05f;
		}else if (timeBetweenAttacks < 0.4f && timeBetweenAttacks > 0.3f){
			timeBetweenAttacks -= 0.01f;
		}else if (timeBetweenAttacks < 0.3f && timeBetweenAttacks > 0.1f){
			timeBetweenAttacks -= 0.005f;
		}

	}
	public void IncreasePoints(int points){
		this.points += points;
		totalPoints += points;
	}
}
