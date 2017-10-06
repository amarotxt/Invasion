using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour {
	public EnemyData enemyData;

	int pointsInGame;
	float timer;
	float range;
	float distanceToPlayer;
	CommandsEnemies archer;
	GameObject player;
	Player playerstatus;
	GameObject drop;

	public GameObject bulets;
	ControllerEnemyHealthBar healthBar;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		range = Random.Range (40f, 70f);
		drop = (GameObject)Resources.Load ("Prefabs/Drops/DropLife", typeof(GameObject));
		playerstatus = player.GetComponent<Player> ();
		// speedMoves,health, damege, range, armor, player;
		archer =new ArcherCommands(enemyData.speedMoves,
			enemyData.health+(playerstatus.fullHealth*0.1f),
			0,
			range,
			enemyData.armor+(playerstatus.damage*0.1f),
			player.GetComponent<Player>());
		healthBar = GetComponent<ControllerEnemyHealthBar>();
		healthBar.ChangeHealthvalue (archer.fullhealth, archer.health);
		pointsInGame += enemyData.points+playerstatus.lvl;

		Instantiate (enemyData.nave,gameObject.transform);
	}

	void FixedUpdate () {
		if (distanceToPlayer <= range) {
			timer += Time.deltaTime;
		}
		distanceToPlayer = Vector3.Distance (new Vector3(player.transform.position.x,0),new Vector3( gameObject.transform.position.x,0));
		if(timer >= enemyData.timeBetweenAttacks){
			timer = 0f;
			if (bulets != null){
				Instantiate (bulets, gameObject.transform.position,Quaternion.identity);
			}
		}
		archer.Move (gameObject.transform, distanceToPlayer);
	}
	void OnTriggerEnter(Collider col){

		if (col.gameObject.CompareTag ("arma")) {
			archer.TakeDamege (player.GetComponent<Player> ().damage,transform);
			Destroy (col.gameObject);
			healthBar.ChangeHealthvalue (archer.fullhealth,archer.health);

			if (archer.health <= 0) {
				player.GetComponent<Player>().IncreasePoints(pointsInGame);
				if (Random.Range(0,100) < 10)
					Instantiate (drop, gameObject.transform.position, Quaternion.identity);
				Instantiate (enemyData.explosao, new Vector3(transform.position.x, transform.position.y+12,transform.position.z), Quaternion.identity);
				Destroy (gameObject);
			}
			} 

	}
}

