using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cart : MonoBehaviour {
	public EnemyData enemyData;

	int pointsInGame;
	float timer;
	float health, damage, armor;
	float distanceToPlayer;
	CommandsEnemies cart;
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
		health = enemyData.health+(playerstatus.fullHealth*0.1f);
		damage = enemyData.damage + (Random.Range(playerstatus.armor*0.1f, playerstatus.armor*0.5f)+((int)Mathf.Log(playerstatus.lvl+1)+1));
		armor = enemyData.armor+Random.Range(playerstatus.damage*0.1f,playerstatus.damage*0.2f)+(int)Mathf.Log(playerstatus.lvl+1)+1;
		cart =new WarriorCommands(enemyData.speedMoves, health, damage, enemyData.range, armor,player.GetComponent<Player>());

		healthBar = GetComponent<ControllerEnemyHealthBar>();
		healthBar.ChangeHealthvalue (cart.fullhealth, cart.health);

		pointsInGame += enemyData.points+playerstatus.lvl;
		Instantiate (enemyData.nave,gameObject.transform);
	}

	void FixedUpdate (	) {
		if (distanceToPlayer <= enemyData.range) {
			timer += Time.deltaTime;
		}
		distanceToPlayer = Vector3.Distance (new Vector3(player.transform.position.x,0),new Vector3( gameObject.transform.position.x,0));

		cart.Attack (distanceToPlayer);

		if (enemyData.range >= distanceToPlayer){
			Instantiate (enemyData.explosaoDano, new Vector3(transform.position.x, transform.position.y+12,transform.position.z), Quaternion.identity);
			Destroy (gameObject);
			cart.health = 0;
			healthBar.ChangeHealthvalue (cart.fullhealth, cart.health);
		}
		cart.Move (gameObject.transform, distanceToPlayer);

	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.CompareTag ("arma")) {
			cart.TakeDamege (player.GetComponent<Player> ().damage, transform);
			Destroy (col.gameObject);
			healthBar.ChangeHealthvalue (cart.fullhealth, cart.health);
			if (cart.health <= 0) {
				player.GetComponent<Player>().IncreasePoints(pointsInGame);
				if (Random.Range(0,100) < 10)
					Instantiate (drop, gameObject.transform.position, Quaternion.identity);
				Instantiate (enemyData.explosao, new Vector3(transform.position.x, transform.position.y+12,transform.position.z), Quaternion.identity);
				Destroy (gameObject);
			} 

		}
	}
}
