using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColectDrops : MonoBehaviour {
	Player player;
	Vector3 mousePosition;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
	}
	
	// Update is called once per frame
	void Update () {
		mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		mousePosition.y = 0;
		gameObject.transform.position = mousePosition;
	}

	void OnTriggerEnter(Collider col){
		if (col.CompareTag("drop")){
			player.health += Random.Range (10, 10+(player.fullHealth*0.1f));
			if (player.fullHealth < player.health){
				player.health = player.fullHealth;
			}
			Destroy (col.gameObject);
		}
	}
}
