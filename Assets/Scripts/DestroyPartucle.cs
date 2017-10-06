using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPartucle : MonoBehaviour {

	private IEnumerator Start()
	{
		yield return new WaitForSeconds(GetComponentInChildren<ParticleSystem>().main.duration);
		Destroy(gameObject); 
	}
}
