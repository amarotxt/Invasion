using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvokerEnemy : MonoBehaviour {

	public float timeEnemy;
	public float timeEnemyInvoker;
	public float minRamdonTimerEnemy;


	public GameObject InvokeEnemy(GameObject enemy){
		Vector3 invokerPosition = new Vector3 (transform.position.x, transform.position.y, Random.Range(24f,-24f));
		GameObject instanceWarrior = Instantiate(enemy, invokerPosition, Quaternion.identity);
		timeEnemyInvoker = 0; 
		timeEnemy = Random.Range (minRamdonTimerEnemy,4f);
		return instanceWarrior;
	}
}
