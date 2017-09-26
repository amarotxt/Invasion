using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamagePopup : MonoBehaviour {
	public Animator animator;
	Text damageText;

	// Use this for initialization
	void OnEnable () {
		AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo (0);
		damageText = animator.GetComponent<Text> ();
		Destroy (gameObject, clipInfo[0].clip.length-0.1f);

	}
	
	// Update is called once per frame
	public void SetText (string text) {
		damageText.GetComponent<Text>().text  = text;
	}
}
