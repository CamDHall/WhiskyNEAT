using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourageBoost : AbilitiesBase {

	public static void CourageBoostBasic(CharacterData character, int amount)
    {
        character.courage += amount;
    }

    public static void CourageBoostBasic(List<GameObject> characters, int amount)
    {
        foreach(GameObject character in characters)
        {
            character.GetComponent<CharacterData>().courage += amount;
        }
    }
}
