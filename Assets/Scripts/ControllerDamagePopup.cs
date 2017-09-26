using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerDamagePopup : MonoBehaviour {
	private static DamagePopup damagePopup;
	private static GameObject canvasDamage;

	// Use this for initialization
	public static void Initialize () {
		canvasDamage = GameObject.Find ("CanvasDamage");

		if(!damagePopup){
			damagePopup = Resources.Load<DamagePopup> ("Prefabs/Inimigos/CanvasUtilites/DamagePopupTextParent");
		}
	}
	
	// Update is called once per frame
	public static void CreatingDamagePopupText (string text, Transform location) {
		DamagePopup instance = Instantiate (damagePopup);
		Vector2 positionDamagePopup = Camera.main.WorldToScreenPoint (location.position);
		positionDamagePopup.x += 25;
		instance.transform.SetParent (canvasDamage.transform, false);
		instance.transform.position = positionDamagePopup;
		instance.SetText (text);
	}
}
