using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour {

    public MapData mapData;

    // Abilites
    public bool ability1_used = false, ability2_used = false, ability3_used = false;

    public float health, _meleeDefense, _rangedDefense, _spellDefense;
    public float moves;
    public int numAttacks, meleeStrength, rangedStrength;
    public int meleeRange, rangedRange;
    public float courage;
    public int mana;

    void Update () {
        if(health <= 0)
        {
            if (gameObject.tag == "Enemy")
            {
                mapData.enemies.Remove(gameObject);
                PhaseManager.numEnemies--;
                Destroy(gameObject);
            } else
            {
                mapData.friends.Remove(gameObject);
                PhaseManager.numFriendlies--;
                Destroy(gameObject);
            }
        }
	}
}
