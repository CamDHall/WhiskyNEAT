using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveDead {

	public static void Remove(GameObject target, GameObject attacker)
    {
		// Only run if the target isn't a hero
		CharacterData targetData = target.GetComponent<CharacterData>();
		CharacterData attackerData = attacker.GetComponent<CharacterData> ();
	
		Attacking attacking = attacker.GetComponent<Attacking> ();

		string t_name = "Characters/" + target.GetComponent<CharacterData> ().characterName;
		t_name = t_name.Replace (" ", "");
		Debug.Log (t_name);

		if (target.tag == "Enemy") {
			// Change deck if target is not a hero
			if (targetData.cType != CharacterType.Hero) {
				PlayerInfo.s_deck2.Remove (t_name);
				PlayerInfo.s_deck1.Add (t_name);
			}

			MapData.enemies.Remove (target);
			// Change each friend
			foreach (GameObject friend in MapData.friends) {
				Attacking friendAttacking = friend.GetComponent<Attacking> ();
				if (friendAttacking._enemiesInMeleeRange.Contains (target)) {
					friendAttacking._enemiesInMeleeRange.Remove (target);
				}

				if (friendAttacking._enemiesInRangedRange.Contains (target)) {
					friendAttacking._enemiesInRangedRange.Remove (target);
				}
			}
		} else {
			// Change deck if not target isn't a hero
			if (targetData.cType != CharacterType.Hero) {
				PlayerInfo.s_deck1.Remove (t_name);
				PlayerInfo.s_deck2.Add (t_name);
			}

			MapData.friends.Remove (target);
			foreach (GameObject enemy in MapData.enemies) {
				Attacking enenmyAttacking = enemy.GetComponent<Attacking> ();
				if (enenmyAttacking._friendsInMeleeRange.Contains (target)) {
					enenmyAttacking._friendsInMeleeRange.Remove (target);
				}

				if (enenmyAttacking._friendsInRangedRange.Contains (target)) {
					enenmyAttacking._friendsInRangedRange.Remove (target);
				}
			}
		}

        Targeting.DetermineTargets(attacker.tag, attackerData.rangedDistance, attackerData.meleeDistance, attacker);
    }
}
