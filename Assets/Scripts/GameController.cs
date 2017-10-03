using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public GameObject warrior, tank, assassin, archer, cart;
	int countEnemies;
	List<GameObject> enemies;
	InvokerEnemy warriorInvoker, tankInvoker, assassinInvoker, archerInvoker, cartInvoker;
	int maxEnemies;
	int lvlPlayerChange; 


	Player player;
	// Use this for initialization
	void Start () {
//		timeWarrior = 1;
		maxEnemies = 4;
		lvlPlayerChange = 1;
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();

		warriorInvoker = gameObject.AddComponent<InvokerEnemy>();
		warriorInvoker.timeEnemy = 5;
		warriorInvoker.minRamdonTimerEnemy = 4.5f;

		archerInvoker = gameObject.AddComponent<InvokerEnemy> ();
		archerInvoker.timeEnemy = 6;
		archerInvoker.minRamdonTimerEnemy = 4.5f;

		assassinInvoker = gameObject.AddComponent<InvokerEnemy> ();
		assassinInvoker.timeEnemy = 7;
		assassinInvoker.minRamdonTimerEnemy = 5.5f;

		tankInvoker = gameObject.AddComponent<InvokerEnemy> ();
		tankInvoker.timeEnemy = 16;
		tankInvoker.minRamdonTimerEnemy = 14.5f;

		cartInvoker = gameObject.AddComponent<InvokerEnemy> ();
		cartInvoker.timeEnemy = 20;
		cartInvoker.minRamdonTimerEnemy = 18.5f;

		enemies = new List<GameObject>();
		ControllerDamagePopup.Initialize ();		 
	}

	void Update () {
		CheckLvlMinTimeInvoker ();	


		if (maxEnemies > enemies.Count){
			if (player.lvl >= 0){
				warriorInvoker.timeEnemyInvoker += Time.deltaTime;

				if (player.points < ((int)(player.changelvl - (player.changelvl * 0.2))) ) {
					
					if (warriorInvoker.timeEnemyInvoker >= warriorInvoker.timeEnemy) {
						

						enemies.Add (warriorInvoker.InvokeEnemy (warrior));
					}
				} else {
					if (warriorInvoker.timeEnemyInvoker >= warriorInvoker.timeEnemy) {
						int enimiesInvoker = (int)((player.changelvl * 0.2) * 0.1f);
						if (enimiesInvoker > 8){
							enimiesInvoker = Random.Range(8,10);
						}
						enemies.AddRange (warriorInvoker.InvokeEnemyWave (warrior, enimiesInvoker));
						}
				}
			}
			if (player.lvl >= 3){
				archerInvoker.timeEnemyInvoker += Time.deltaTime;				
				if (player.points < ((int)(player.changelvl - (player.changelvl * 0.3)))) {
					if (archerInvoker.timeEnemyInvoker >= archerInvoker.timeEnemy) {
						enemies.Add (archerInvoker.InvokeEnemy (archer));
					}
				} else {
					if (archerInvoker.timeEnemyInvoker >= archerInvoker.timeEnemy) {
						int enimiesInvoker = (int)((player.changelvl * 0.3) * 0.1f);
						if (enimiesInvoker > 7){
							enimiesInvoker = Random.Range(6,9);
						}
						enemies.AddRange (archerInvoker.InvokeEnemyWave (archer, enimiesInvoker));
					}
				}
			}
			if (player.lvl >= 6){
				if (player.points < ((int)(player.changelvl - (player.changelvl * 0.1)))) {
					assassinInvoker.timeEnemyInvoker += Time.deltaTime;
					if (assassinInvoker.timeEnemyInvoker >= assassinInvoker.timeEnemy) {
						enemies.Add (assassinInvoker.InvokeEnemy (assassin));
					}
				}else {
					if (assassinInvoker.timeEnemyInvoker >= assassinInvoker.timeEnemy) {
						int enimiesInvoker = (int)((player.changelvl * 0.1) * 0.1f);
						if (enimiesInvoker > 7){
							enimiesInvoker = Random.Range(4, 7);
						}
						enemies.AddRange (assassinInvoker.InvokeEnemyWave (assassin, enimiesInvoker));
					}
				}
			}
			if (player.lvl >= 8){
				if (player.points < ((int)(player.changelvl - (player.changelvl * 0.4)))) {
					tankInvoker.timeEnemyInvoker += Time.deltaTime;
					if (tankInvoker.timeEnemyInvoker >= tankInvoker.timeEnemy) {
						enemies.Add (tankInvoker.InvokeEnemy (tank));
					}
				} else {
					if (assassinInvoker.timeEnemyInvoker >= assassinInvoker.timeEnemy) {
						int  enimiesInvoker = (int)((player.changelvl * 0.1) * 0.1f);
						if (enimiesInvoker > 4){
							enimiesInvoker = Random.Range(3,5);
						}
						Debug.Log ("teste");
						enemies.AddRange (tankInvoker.InvokeEnemyWave (tank, enimiesInvoker));
					}
				}
			}
			if (player.lvl >= 12){
				cartInvoker.timeEnemyInvoker += Time.deltaTime;
				if (cartInvoker.timeEnemyInvoker >= cartInvoker.timeEnemy){
					enemies.Add(cartInvoker.InvokeEnemy (cart));
				}
			}
		}
		for (int i = 0; i < enemies.Count; i++) {
			if (enemies[i] == null) {
				enemies.RemoveAt (i);
			}
		}
	}
	void CheckLvlMinTimeInvoker(){
		if (lvlPlayerChange == player.lvl) {
			lvlPlayerChange += 1;
			maxEnemies += (int)Mathf.Log(player.lvl);
			SetMinTimerInvoker (warriorInvoker);
			if (player.lvl > 3){
				SetMinTimerInvoker (archerInvoker);				
			}
			if (player.lvl > 6){
				SetMinTimerInvoker (assassinInvoker);				
			}
			if (player.lvl > 9){
				SetMinTimerInvoker (tankInvoker);				
			}
			if (player.lvl > 12){
				SetMinTimerInvoker (cartInvoker);				
			}
		}

	}
		
	void SetMinTimerInvoker (InvokerEnemy enemy){
		if (enemy.minRamdonTimerEnemy > 2.5f)
			enemy.minRamdonTimerEnemy -= 0.1f;
		else if (enemy.minRamdonTimerEnemy < 2.5f && enemy.minRamdonTimerEnemy >= 1.5f)
			enemy.minRamdonTimerEnemy -= 0.05f;

	}
}





