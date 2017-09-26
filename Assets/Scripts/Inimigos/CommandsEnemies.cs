using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CommandsEnemies{
	protected float speedMoves;
	public float damage;
	protected float range;
	protected float armor;
	protected Player player;
	public float health;
	public float fullhealth;

	public virtual void Move(Transform enemy, float range){
		if (this.range < range){
			enemy.transform.Translate (speedMoves*Time.deltaTime*Vector3.left);
		}
	}
	public virtual void Attack(float distance){}

	public virtual void TakeDamege(float damage, Transform location){
		float calculateDamage = damage - Random.Range (armor / 2f, armor);
		if (calculateDamage > 0) {
			ControllerDamagePopup.CreatingDamagePopupText (calculateDamage.ToString ("0.00"), location);
			health -= calculateDamage;
		} else {
			ControllerDamagePopup.CreatingDamagePopupText ("00.00", location);

		}

	}

}

public class WarriorCommands: CommandsEnemies{
	public WarriorCommands(float speedMoves,float health, float damage, float range, float armor, Player player){
		this.speedMoves = speedMoves;
		this.health = health;
		fullhealth = health;
		this.damage = damage;
		this.range = range;
		this.armor = armor;
		this.player = player;
		}
	public override void Attack(float distance){
		if (this.range >= distance) {
			this.player.TakeDamage (this.damage);
		}
	}
}

public class ArcherCommands: CommandsEnemies{
	public ArcherCommands(float speedMoves, float health, float damage, float range, float armor, Player player){
		this.speedMoves = speedMoves;
		this.health = health;
		fullhealth = health;
		this.damage = damage;
		this.range = range;
		this.armor = armor;
		this.player = player;
	}
}

public class BuletsCommands: CommandsEnemies{
	public BuletsCommands(float speedMoves, float health, float damage, float range, float armor, Player player){
		this.speedMoves = speedMoves;
		this.health = health;
		this.damage = damage;
		this.range = range;
		this.armor = armor;
		this.player = player;
	}

//			enimy.position =Vector3.MoveTowards (enimy.position, player.transform.position, speedMoves);
//			Vector3 direction = player.transform.position - enimy.position;
//			enimy.transform.Translate (speedMoves*Time.deltaTime*Vector3.Normalize(direction));
	public override void Move(Transform enemy, float range){
		if (this.range < range){
			enemy.transform.Translate (speedMoves*Time.deltaTime*Vector3.forward);

		}
	}	
	public override void Attack(float distance){
		this.player.TakeDamage (damage);
	}
}
public class AssassinoCommands: CommandsEnemies{
	bool up = true;
	bool down = false;
	public AssassinoCommands(float speedMoves, float health, float damage, float range, float armor, Player player){
		this.speedMoves = speedMoves;
		this.health = health;
		fullhealth = health;
		this.damage = damage;
		this.range = range;
		this.armor = armor;
		this.player = player;
	}

	public override void Move(Transform enemy, float range){
		if (this.range < range){
			Vector3 goUp = new Vector3 (-1,0,2f);
			Vector3 goDown = new Vector3 (-1,0,-2f);

			if (enemy.transform.position.z > 24 ){
				up = false;
				down = true;
			}if (enemy.transform.position.z < -24) {
				up = true;
				down = false;
			}

			if (up && !down){
				enemy.transform.Translate (speedMoves * Time.deltaTime * goUp);
			}
			if (!up && down){
				enemy.transform.Translate (speedMoves * Time.deltaTime * goDown);
			}
		}

	}	
	public override void Attack(float distance){
		if (this.range >= distance) {
			this.player.TakeDamage (damage);
		}	
	}
}