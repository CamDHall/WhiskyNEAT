using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : AbilitiesBase {

    // Always use this to heal, even for multi-turn healing
	public static void BasicHeal(CharacterData data, int amount)
    {
        data.health += amount;
    }

    public static void SlowHealLV1(CharacterData data)
    {
        BasicHeal(data, 4);
    }

    public static void AOEHealLV1(CharacterTeam team, GameObject user)
    {
        if (team == CharacterTeam.Enemy)
        {
            foreach (GameObject character in MapData.enemies)
            {
                if(Vector3.Distance(user.transform.position, character.transform.position) <= 3)
                {
                    BasicHeal(character.GetComponent<CharacterData>(), 2);
                }
            }
        } else
        {
            foreach(GameObject character in MapData.friends)
            {
                if(Vector3.Distance(user.transform.position, character.transform.position) <= 3)
                {
                    BasicHeal(character.GetComponent<CharacterData>(), 2);
                }
            }
        }
    }

}
