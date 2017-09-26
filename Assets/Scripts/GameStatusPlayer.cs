using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStatusPlayer : MonoBehaviour {
	Player player;
	int lvlUp;
	int extraPoints;
	public Text totalPointsText;
	public Text lvlPlayerText;
	public Text helthPlayerText;
	public Text extraPointsText;
	public Text healthTotalText;
	public Text damageText;
	public Text armorText;
	public Text attackSpeedText;
	GameObject playSatusPainel;

	public Slider healthBar;
	public Slider lvlBar;

	// Use this for initialization
	void Start () {
		lvlUp = 0;
		playSatusPainel = GameObject.Find("PlayStatus");
		playSatusPainel.SetActive (false);
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player>();
	}
	void FixedUpdate(){
		extraPointsText.text = extraPoints.ToString();
		healthTotalText.text = player.fullHealth.ToString();
		damageText.text = player.damage.ToString();
		armorText.text = player.armor.ToString();
		attackSpeedText.text = player.timeBetweenAttacks.ToString("00.00");
		totalPointsText.text = player.totalPoints.ToString();
		lvlPlayerText.text = player.lvl.ToString();
		helthPlayerText.text = player.health.ToString ("00.00");
		if (player.lvl > lvlUp){
			playSatusPainel.SetActive (true);
			extraPoints += 5+(int)Mathf.Log(player.lvl);
			extraPointsText.text = extraPoints.ToString();
			lvlUp = player.lvl;
			Time.timeScale = 0;
		}
	
		healthBar.value = CauculateHealthBar();
		lvlBar.value = CauculateLvlBar ();

	}
	public void IncreaseHealth(){
		if (extraPoints != 0){
			player.IncreaseHealth ();
			healthTotalText.text = player.fullHealth.ToString();
			extraPoints -= 1;
			extraPointsText.text = extraPoints.ToString();

		}
	}
	public void IncreaseArmor(){
		if (extraPoints != 0) {
			player.IncreaseArmor ();
			armorText.text = player.armor.ToString();
			extraPoints -= 1;
			extraPointsText.text = extraPoints.ToString();
		}
	}
	public void IncreaseStrength(){
		if (extraPoints != 0) {
			player.IncreaseStrength ();
			damageText.text = player.damage.ToString();
			extraPoints -= 1;
			extraPointsText.text = extraPoints.ToString();
		}
	}
	public void IncreaseAttackSpeed(){
		if (extraPoints != 0) {
			if (player.timeBetweenAttacks < 0.1f) {
				extraPoints = extraPoints;
			} else {
				player.IncreaseAttackSpeed ();
				attackSpeedText.text = player.timeBetweenAttacks.ToString("00.00");
				extraPoints -= 1;
				extraPointsText.text = extraPoints.ToString();
			}
		}
	}
	public float CauculateHealthBar(){
		return (player.health / player.fullHealth);
	}
	public float CauculateLvlBar(){
		return ((float)player.points / (float)player.changelvl);
	}
	public void ContinueGame(){
		Time.timeScale = 1;
		playSatusPainel.SetActive (false);
	}
}
