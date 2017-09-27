using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : MonoBehaviour {
	public int points;
	public float speedMoves;
	public float damage;
	public float range;
	public float armor;
	public float health;

	public float timeBetweenAttacks;
	float timer;
	float distanceToPlayer;
	CommandsEnemies warrior;
	GameObject player;
	Player playerstatus;
	GameObject drop;
	public GameObject explosao;

	ControllerEnemyHealthBar healthBar;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		drop = (GameObject)Resources.Load ("Prefabs/Drops/DropLife", typeof(GameObject));
		playerstatus = player.GetComponent<Player> ();
		// speedMoves,health, damege, range, armor, player;
		health = health+(playerstatus.fullHealth*0.1f);
		damage = damage + Random.Range (playerstatus.armor * 0.2f, playerstatus.armor * 0.4f) + (int)Mathf.Log (playerstatus.lvl + 1) + 1;
		armor = armor+Random.Range(playerstatus.damage*0.1f,playerstatus.damage*0.2f)+(int)Mathf.Log(playerstatus.lvl+1)+1;
		warrior =new WarriorCommands(speedMoves, health, damage, range, armor,player.GetComponent<Player>());
		healthBar = GetComponent<ControllerEnemyHealthBar>();
		healthBar.ChangeHealthvalue (warrior.fullhealth, warrior.health);
		points += playerstatus.lvl;
		}
	void FixedUpdate (	) {
		timer += Time.deltaTime;
		distanceToPlayer = Vector3.Distance (new Vector3(player.transform.position.x,0),new Vector3( gameObject.transform.position.x,0));

		// If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
		if(timer >= timeBetweenAttacks){
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
				player.GetComponent<Player>().IncreasePoints(points);
				if (Random.Range(0,100) < 10)
					Instantiate (drop, gameObject.transform.position, Quaternion.identity);
				Instantiate (explosao, new Vector3(transform.position.x, transform.position.y+12, gameObject.transform.position.z), Quaternion.identity);
				Destroy (gameObject);
			} 

		}
	}
}
