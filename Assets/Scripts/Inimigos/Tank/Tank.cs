using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour {
	public EnemyData enemyData;

	int pointsInGame;
	float timer;
	float health, damage, armor;
	float distanceToPlayer;
	CommandsEnemies warrior;
	GameObject player;
	Player playerstatus;
	GameObject drop;

	ControllerEnemyHealthBar healthBar;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		drop = (GameObject)Resources.Load ("Prefabs/Drops/DropLife", typeof(GameObject));
		playerstatus = player.GetComponent<Player> ();

		// speedMoves,health, damege, range, armor, player;
		health = enemyData.health+(playerstatus.fullHealth*0.3f+(int)Mathf.Log(playerstatus.lvl+1)+1);
		damage = enemyData.damage+(Random.Range(playerstatus.armor*0.1f, playerstatus.armor*0.35f)+((int)Mathf.Log(playerstatus.lvl+1)+1));
		armor = enemyData.armor+Random.Range(playerstatus.damage*0.25f,playerstatus.damage*0.4f)+(int)Mathf.Log(playerstatus.lvl+1)+1;
		warrior =new WarriorCommands(enemyData.speedMoves, health, damage, enemyData.range, armor,player.GetComponent<Player>());

		healthBar = GetComponent<ControllerEnemyHealthBar>();
		healthBar.ChangeHealthvalue (warrior.fullhealth, warrior.health);

		pointsInGame += enemyData.points+playerstatus.lvl;
		Instantiate (enemyData.nave,gameObject.transform);
	}

	void FixedUpdate (	) {
		if (distanceToPlayer <= enemyData.range) {
			timer += Time.deltaTime;
		}
		distanceToPlayer = Vector3.Distance (new Vector3(player.transform.position.x,0),new Vector3( gameObject.transform.position.x,0));

		// If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
	if(timer >= enemyData.timeBetweenAttacks){
			if (enemyData.range >= distanceToPlayer) {
			Instantiate (enemyData.explosaoDano, new Vector3(transform.position.x, transform.position.y+12, gameObject.transform.position.z), Quaternion.identity);
			}
			warrior.Attack (distanceToPlayer);
			timer = 0f;
		}
		warrior.Move (gameObject.transform, distanceToPlayer);

	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.CompareTag ("arma")) {

			warrior.TakeDamege (player.GetComponent<Player> ().damage, transform);
			Destroy (col.gameObject);
			healthBar.ChangeHealthvalue (warrior.fullhealth, warrior.health);
			if (warrior.health <= 0) {
				player.GetComponent<Player>().IncreasePoints(pointsInGame);
				if (Random.Range(0,100) < 10)
					Instantiate (drop, gameObject.transform.position, Quaternion.identity);
			Instantiate (enemyData.explosao,new Vector3(transform.position.x, transform.position.y+12,transform.position.z), Quaternion.identity);
				Destroy (gameObject);
			} 

		}
	}
}
