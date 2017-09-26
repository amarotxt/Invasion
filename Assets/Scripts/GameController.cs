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
		warriorInvoker.timeEnemy = 4;
		warriorInvoker.minRamdonTimerEnemy = 3.5f;

		archerInvoker = gameObject.AddComponent<InvokerEnemy> ();
		archerInvoker.timeEnemy = 6;
		archerInvoker.minRamdonTimerEnemy = 4.5f;

		assassinInvoker = gameObject.AddComponent<InvokerEnemy> ();
		assassinInvoker.timeEnemy = 7;
		assassinInvoker.minRamdonTimerEnemy = 5.5f;

		tankInvoker = gameObject.AddComponent<InvokerEnemy> ();
		tankInvoker.timeEnemy = 12;
		tankInvoker.minRamdonTimerEnemy = 10.5f;

		cartInvoker = gameObject.AddComponent<InvokerEnemy> ();
		cartInvoker.timeEnemy = 15;
		cartInvoker.minRamdonTimerEnemy = 12.5f;

		enemies = new List<GameObject>();
		ControllerDamagePopup.Initialize ();		 
	}
	void Update () {
		CheckLvlMinTimeInvoker ();	


		if (maxEnemies > enemies.Count){
			if (player.lvl >= 0){
				warriorInvoker.timeEnemyInvoker += Time.deltaTime;
				if (warriorInvoker.timeEnemyInvoker >= warriorInvoker.timeEnemy){
					enemies.Add(warriorInvoker.InvokeEnemy (warrior));
				}
			}
			if (player.lvl >= 3){
				archerInvoker.timeEnemyInvoker += Time.deltaTime;				
				if (archerInvoker.timeEnemyInvoker >= archerInvoker.timeEnemy){
					enemies.Add(archerInvoker.InvokeEnemy (archer));
				}
			}
			if (player.lvl >= 5){
				assassinInvoker.timeEnemyInvoker += Time.deltaTime;
				if (assassinInvoker.timeEnemyInvoker >= assassinInvoker.timeEnemy){
					enemies.Add(assassinInvoker.InvokeEnemy (assassin));
				}
			}
			if (player.lvl >= 7){
				tankInvoker.timeEnemyInvoker += Time.deltaTime;
				if (tankInvoker.timeEnemyInvoker >= tankInvoker.timeEnemy){
					enemies.Add(tankInvoker.InvokeEnemy (tank));
				}
			}
			if (player.lvl >= 9){
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
				Debug.Log (player.lvl);
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





