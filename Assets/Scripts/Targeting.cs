using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeting {

    public static void DetermineTargets(string team, int rangedRange, int meleeRange, GameObject current)
    {
        Attacking attacking = current.GetComponent<Attacking>();
        // Check if this is a character or enemy
        if (team == "Friend")
        {
            attacking._enemiesInMeleeRange.Clear();
            attacking._enemiesInRangedRange.Clear();
            foreach (GameObject enemy in MapData.enemies)
            {
                MapData.enenmyInfo[enemy.transform.position] = GeneralMath.DistanceForm(enemy.transform.position, current.transform.position)
                     + enemy.transform.parent.position.y - 1;

                if (MapData.enenmyInfo[enemy.transform.position] <= rangedRange)
                {
                    attacking._enemiesInRangedRange.Add(enemy);
                }

                if (MapData.enenmyInfo[enemy.transform.position] <= meleeRange)
                {
                    attacking._enemiesInMeleeRange.Add(enemy);
                }
            }

            // Create totla list of targets
            foreach(GameObject target in attacking._enemiesInMeleeRange)
            {
                attacking._allTargets.Add(target);
            }

            foreach(GameObject target in attacking._enemiesInRangedRange)
            {
                attacking._allTargets.Add(target);
            }
        }
        else
        {
            attacking._friendsInMeleeRange.Clear();
            attacking._friendsInRangedRange.Clear();
            foreach (GameObject friend in MapData.friends)
            {
                MapData.friendsInfo[friend.transform.position] = GeneralMath.DistanceForm(friend.transform.position, current.transform.position) +
                   friend.transform.parent.position.y - 1;

                if (MapData.friendsInfo[friend.transform.position] <= meleeRange)
                {
                    attacking._friendsInMeleeRange.Add(friend);
                }

                if (MapData.friendsInfo[friend.transform.position] <= rangedRange)
                {
                   attacking._friendsInRangedRange.Add(friend);
                }
            }
            // Create list of total targets
            foreach(GameObject target in attacking._friendsInMeleeRange)
            {
                attacking._allTargets.Add(target);
            }

            foreach(GameObject target in attacking._friendsInRangedRange)
            {
                attacking._allTargets.Add(target);
            }
        }
    }
}
