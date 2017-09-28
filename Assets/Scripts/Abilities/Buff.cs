using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : AbilitiesBase {

    // Increase melee damage
	public static void MeleeDamageBuff(CharacterData data, int amount)
    {
        data.meleeStrength += amount;
    }

    // Team buff
    public static void MeleeDamageBuffTeam(List<GameObject> characters, int amount)
    {
        foreach(GameObject character in characters)
        {
            character.GetComponent<CharacterData>().meleeStrength += amount;
        }
    }

    // If character has ranged attacks, buff it
    public static void RangedDamageBuff(CharacterData data, int amount)
    {
        if(data.rangedStrength > 0)
        {
            data.rangedStrength += amount;
        }
    }

    // Ranged List
    public static void RangedDamageBuffTeam(List<GameObject> characters, int amount)
    {
        foreach(GameObject character in characters)
        {
            if(character.GetComponent<CharacterData>().rangedStrength > 0)
            {
                character.GetComponent<CharacterData>().rangedStrength += amount;
            }
        }
    }
}
