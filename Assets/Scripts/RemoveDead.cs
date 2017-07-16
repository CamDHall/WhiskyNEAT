using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveDead {

	public static void Remove(GameObject target, GameObject attacker)
    {
        CharacterData attackerData = attacker.GetComponent<CharacterData>();
        Attacking attacking = attacker.GetComponent<Attacking>();

        if(target.tag == "Enemy")
        {
            MapData.enemies.Remove(target);
            // Change each friend
            foreach (GameObject friend in MapData.friends)
            {
                Attacking friendAttacking = friend.GetComponent<Attacking>();
                if (friendAttacking._enemiesInMeleeRange.Contains(target))
                {
                    friendAttacking._enemiesInMeleeRange.Remove(target);
                }

                if (friendAttacking._enemiesInRangedRange.Contains(target))
                {
                    friendAttacking._enemiesInRangedRange.Remove(target);
                }
            }
        } else
        {
            MapData.friends.Remove(target);
            foreach (GameObject enemy in MapData.enemies)
            {
                Attacking enenmyAttacking = enemy.GetComponent<Attacking>();
                if (enenmyAttacking._friendsInMeleeRange.Contains(target))
                {
                    enenmyAttacking._friendsInMeleeRange.Remove(target);
                }

                if (enenmyAttacking._friendsInRangedRange.Contains(target))
                {
                    enenmyAttacking._friendsInRangedRange.Remove(target);
                }
            }
        }

        Targeting.DetermineTargets(attacker.tag, attackerData.rangedDistance, attackerData.meleeDistance, attacker);
    }
}
